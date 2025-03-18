using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using AppAcademy.Application.Features.Students.Commands.CreateStudent;
using AppAcademy.Application.Features.Students.Commands.DeleteStudent;
using AppAcademy.Application.Features.Students.Queries.GetAllStudents;
using AppAcademy.Application.Features.Students.Queries.GetGanttData;
using AppAcademy.Application.Features.Students.Queries.GetStudent;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Infrastucture.Historicos;
using AppAcademy.Infrastucture.Identity;
using AppAcademy.Infrastucture.Persistence;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class StudentRepository : AsyncRepository<Student>, IStudentRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<StudentRepository> _logger;

        public StudentRepository(AppAcademyDbContext dbContext, UserManager<AppUser> userManager, ILogger<StudentRepository> logger) : base(dbContext)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<List<GetAllStudentsVm>> GetAllStudentsWithCareers()
        {
            try
            {
                var students = await _dbContext.Students
                    .Include(s => s.Career)
                    .Include(s => s.AcademicCycle)
                    .Select(s => new GetAllStudentsVm
                    {
                        StudentId = s.StudentId,
                        Nombre = s.Nombre,
                        Apellido = s.Apellido,
                        Telefono = s.Telefono,
                        Email = s.Email,
                        Direccion = s.Direccion,
                        ImageUrl = s.ImageUrl,
                        FechaIngreso = s.FechaIngreso,
                        AcademicCycleId = s.AcademicCycleId ?? 0,
                        EstadoEstudiante = s.EstadoEstudiante.ToString(),
                        Career = s.Career != null 
                            ? new GetAllCareerVm
                            {
                                CareerId = s.Career.CareerId,
                                Nombre = s.Career.Nombre,
                                Color = s.Career.Color
                            }: null,
                    }).ToListAsync();

                return students;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<GetStudentVm> GetStudentsByIdWithCareer(int studentId)
        {
            var student = await _dbContext.Students
                .Include(s => s.Career)
                .Where(s => s.StudentId == studentId)
                .Select(s => new GetStudentVm
                {
                    StudentId = s.StudentId,
                    Nombre = s.Nombre,
                    Apellido = s.Apellido,
                    Telefono = s.Telefono,
                    Email = s.Email,
                    Direccion = s.Direccion,
                    ImageUrl = s.ImageUrl,
                    FechaIngreso = s.FechaIngreso,
                    AcademicCycleId = s.AcademicCycleId ?? 0,
                    EstadoEstudiante = s.EstadoEstudiante,
                    Career = new GetCareerById
                    {
                        CareerId = s.Career.CareerId,
                        Nombre = s.Career.Nombre,
                        Color = s.Career.Color
                    }
                }).FirstOrDefaultAsync();

            return student;
        }

        public async Task<List<GetStudentCardVm>> GetStudentCard()
        {
            var students = await _dbContext.Students
                 .Include(s => s.Career)
                 .Include(s => s.Payments)
                 .Include(s => s.Career.AcademicCycles)
                 .ToListAsync();

            var studentCards = new List<GetStudentCardVm>();

            foreach (var student in students)
            {
                var monthsInCareer = student.CareerId.HasValue
                         ? GetTotalMonthsInCareer(student.CareerId.Value, student.AcademicCycleId)
                         : 0; // Si CareerId es null, puedes asignar 0 o un valor por defecto
                var payments = GetPaymentsInMonths(student.Payments, student.Career.AcademicCycles, student.AcademicCycleId);

                // Aquí obtenemos el primer año de los pagos o el ciclo académico (opcional)
                var paymentYears = payments.Select(p => p.Anio).Distinct().ToList();
                var paymentYear = paymentYears.Any() ? paymentYears.First() : DateTime.Now.Year;  // Tomar el primer año de los pagos


                studentCards.Add(new GetStudentCardVm
                {
                    StudentId = student.StudentId,
                    Nombre = student.Nombre,
                    Apellido = student.Apellido,
                    Foto = student.ImageUrl,
                    CarreraNombre = student.Career.Nombre,
                    DuracionCarrera = monthsInCareer,
                    Pagos = payments,
                    AnioPago = paymentYear
                });
            }

            return studentCards;
        }

        public async Task<int> CreateStudent(CreateStudentCommand student, string userName)
        {
            if (student.ImageFile != null && student.ImageFile.Length > 0)
            {
                try
                {
                    // Guardar la imagen y obtener la url
                    var imageUrl = await SaveImageAndGetUrl(student.ImageFile);
                    student.ImageUrl = imageUrl; // Almacenar la Url de la imagen en el modelo 
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error al procesar la imagen: {ex.Message}");
                    throw new ApplicationException("No se pudo guardar la imagen del estudiante");
                }
            }

            var newStudent = new Student
            {
                Nombre = student.Nombre,
                Apellido = student.Apellido,
                Telefono = student.Telefono,
                Email = student.Email,
                Direccion = student.Direccion,
                ImageUrl = student.ImageUrl,
                FechaIngreso = DateTime.Now,
                EstadoEstudiante = student.EstadoEstudiante,
                CareerId = student.CareerId,
                AcademicCycleId = student.AcademicCycleId,
            };

            await AddAsync(newStudent);

            var usuario = await _userManager.FindByNameAsync(userName);
            if (usuario == null) throw new Exception("Usuario no encontrado");

            var bitacora = new Bitacora
            {
                UsuarioId = usuario.Id,
                Fecha = DateTime.UtcNow,
                Descripcion = $"Se registró un nuevo estudiante: {newStudent.Nombre} {newStudent.Apellido}",
                ReferenciaId = newStudent.StudentId.ToString(),
                TipoReferencia = "Student"
            };

            _dbContext.Bitacora.Add(bitacora);
            await _dbContext.SaveChangesAsync();

            return newStudent.StudentId;
        }

        public async Task<bool> DeleteStudent(DeleteStudentCommand student, string userName)
        {
            var findStudent = await _dbContext.Students.FindAsync(student.StudentId);

            if (findStudent == null)
            {
                _logger.LogError($"{student.StudentId} student no existe en el sistema");
                throw new NotFoundException(nameof(findStudent), student.StudentId);
            }

            // Si hay una imagen asociada, eliminarla del servidor
            if (!string.IsNullOrEmpty(findStudent.ImageUrl))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "students", "images", Path.GetFileName(findStudent.ImageUrl.TrimStart('/')));

                if (File.Exists(imagePath))
                {
                    try
                    {
                        File.Delete(imagePath);
                        _logger.LogInformation($"Imagen eliminada del servidor: {imagePath}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error al eliminar la imagen: {ex.Message}");
                        // Puedes optar por continuar o lanzar un error dependiendo de si quieres que falle la eliminación del producto si no se puede eliminar la imagen.
                    }
                }
                else
                {
                    _logger.LogWarning($"No se encontró la imagen en el servidor para eliminar: {imagePath}");
                }
            }

            _dbContext.Remove(findStudent);

            var usuario = await _userManager.FindByNameAsync(userName);
            if (usuario == null) throw new Exception("Usuario no encontrado");

            var bitacora = new Bitacora
            {
                UsuarioId = usuario.Id,
                Fecha = DateTime.UtcNow,
                Descripcion = $"Se elimino un estudiante: {findStudent.Nombre} {findStudent.Apellido}",
                ReferenciaId = findStudent.StudentId.ToString(),
                TipoReferencia = "Student"
            };

            _dbContext.Bitacora.Add(bitacora);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        #region Metodos Privados
        private List<PaymentMonthDto> GetPaymentsInMonths(List<Payment> payments, List<AcademicCycle> academicCycles, int? academicCycleId)
        {
            var months = new List<PaymentMonthDto>();

            // Filtrar ciclos académicos por el ciclo académico del estudiante
            var filteredCycles = academicCycles.Where(c => c.AcademicCycleId == academicCycleId).ToList();

            foreach (var cycle in filteredCycles)
            {
                var currentMonth = cycle.FechaInicio.Month;
                var currentYear = cycle.FechaInicio.Year;

                var endMonth = cycle.FechaFin.Month;
                var endYear = cycle.FechaFin.Year;

                while (currentYear < endYear || (currentYear == endYear && currentMonth <= endMonth))
                {
                    var payment = payments.FirstOrDefault(p => (int)p.MesPagado == currentMonth && p.AnioPagado == currentYear);

                    var paymentMonth = new PaymentMonthDto
                    {
                        Mes = GetMonthName(currentMonth),
                        Pagado = payment != null ? payment.EstadoVenta.ToString() : "Sin Pago",
                        Anio = currentYear
                    };

                    months.Add(paymentMonth);

                    currentMonth++;
                    if (currentMonth > 12)
                    {
                        currentMonth = 1;
                        currentYear++;
                    }
                }
            }

            return months;
        }

        private int GetTotalMonthsInCareer(int careerId, int? academicCycleId)
        {
            var academicCycles = _dbContext.AcademicCycles
                .Where(c => c.CareerId == careerId)
                .OrderBy(c => c.FechaInicio)
                .ToList();

            // Si el estudiante tiene un ciclo académico asignado, solo tomamos ese
            if (academicCycleId.HasValue)
            {
                var academicCycle = academicCycles.FirstOrDefault(c => c.AcademicCycleId == academicCycleId.Value);
                if (academicCycle != null)
                {
                    var duration = (academicCycle.FechaFin.Year - academicCycle.FechaInicio.Year) * 12 + (academicCycle.FechaFin.Month - academicCycle.FechaInicio.Month);
                    return duration;
                }
            }

            // Si no hay ciclo académico asignado, puedes calcular usando todos los ciclos
            if (academicCycles.Count == 0)
            {
                return 0;
            }

            var fechaInicio = academicCycles.First().FechaInicio;
            var fechaFin = academicCycles.Last().FechaFin;

            var totalDuration = (fechaFin.Year - fechaInicio.Year) * 12 + (fechaFin.Month - fechaInicio.Month);
            return totalDuration;
        }

        private string GetMonthName(int month)
        {
            var monthNames = new[]
            {
        "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
        "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
    };

            return month >= 1 && month <= 12 ? monthNames[month - 1] : "Desconocido";
        }

        private async Task<string> SaveImageAndGetUrl(IFormFile imageFile)
        {
            try
            {
                // Generar un nombre único para la imagen
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";

                // Crear directorio si no existe
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filePath = Path.Combine(directoryPath, fileName);

                // Guardar la imagen en el servidor
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                // Devolver la URL de la imagen
                return $"/images/{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al guardar la imagen: {ex.Message}");
                throw new ApplicationException("Error al guardar la imagen del producto");
            }
        }
        #endregion
    }
}
