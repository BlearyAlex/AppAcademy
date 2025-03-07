using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Features.Students.Queries.GetAllStudents;
using AppAcademy.Application.Features.Students.Queries.GetGanttData;
using AppAcademy.Application.Features.Students.Queries.GetStudent;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class StudentRepository : AsyncRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<GetAllStudentsVm>> GetAllStudentsWithCareers()
        {
            try
            {
                var students = await _dbContext.Students
                    .Include(s => s.Career)
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
                        EstadoEstudiante = s.EstadoEstudiante.ToString(),
                        Career = new GetAllCareerVm
                        {
                            CareerId = s.Career.CareerId,
                            Nombre = s.Career.Nombre,
                            Color = s.Career.Color
                        }
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
                         ? GetTotalMonthsInCareer(student.CareerId.Value)
                         : 0; // Si CareerId es null, puedes asignar 0 o un valor por defecto
                var payments = GetPaymentsInMonths(student.Payments, student.Career.AcademicCycles);

                studentCards.Add(new GetStudentCardVm
                {
                    StudentId = student.StudentId,
                    Nombre = student.Nombre,
                    Apellido = student.Apellido,
                    Foto = student.ImageUrl,
                    CarreraNombre = student.Career.Nombre,
                    DuracionCarrera = monthsInCareer,
                    Pagos = payments
                });
            }

            return studentCards;
        }

        private int GetTotalMonthsInCareer(int careerId)
        {
            var academicCycles = _dbContext.AcademicCycles
                .Where(c => c.CareerId == careerId)
                .OrderBy(c => c.FechaInicio)
                .ToList();

            if (academicCycles.Count == 0)
            {
                return 0;
            }

            var fechaInicio = academicCycles.First().FechaInicio;
            var fechaFin = academicCycles.Last().FechaFin;

            var duration = (fechaFin.Year - fechaInicio.Year) * 12 + (fechaFin.Month - fechaInicio.Month);
            return duration;
        }

        private List<PaymentMonthDto> GetPaymentsInMonths(List<Payment> payments, List<AcademicCycle> academicCycles)
        {
            var months = new List<PaymentMonthDto>();

            foreach (var cycle in academicCycles)
            {
                // Empezamos desde la fecha de inicio del ciclo académico
                var currentMonth = cycle.FechaInicio.Month;
                var currentYear = cycle.FechaInicio.Year;

                // Calculamos la cantidad de meses entre FechaInicio y FechaFin
                var endMonth = cycle.FechaFin.Month;
                var endYear = cycle.FechaFin.Year;

                // Iteramos mes a mes
                while (currentYear < endYear || (currentYear == endYear && currentMonth <= endMonth))
                {
                    var paymentMonth = new PaymentMonthDto
                    {
                        Mes = currentMonth,
                        Pagado = payments.Any(p => (int)p.MesPagado == currentMonth && p.AnioPagado == currentYear)
                    };

                    months.Add(paymentMonth);

                    // Incrementamos el mes, y si pasamos diciembre, cambiamos de año
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

    }
}
