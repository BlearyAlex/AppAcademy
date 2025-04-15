using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Features.Careers.Commands.CreateCareer;
using AppAcademy.Application.Features.Careers.Commands.DeleteCareer;
using AppAcademy.Application.Features.Careers.Commands.UpdateCareer;
using AppAcademy.Application.Features.Careers.Queries.GetAllCareers;
using AppAcademy.Application.Features.Careers.Queries.GetAllCareersFilter;
using AppAcademy.Application.Features.Careers.Queries.GetCareer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlAcademias
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User, Academia")]
    public class CareerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICareerRepository _careerRepository;

        public CareerController(IMediator mediator, ICareerRepository careerRepository)
        {
            _mediator = mediator;
            _careerRepository = careerRepository;
        }


        #region Create
        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] CreateCareerCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return Ok(new { message = "Carrera creada exitosamente.", careerId = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region Update
        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] UpdateCareerCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region Delete
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (await _careerRepository.CareerTienePagosActivos(id))
                {
                    return Conflict(new { message = "No se puede eliminar la carrera porque tiene pagos asociados y activos." });
                }

                var command = new DeleteCareerCommand
                {
                    CareerId = id
                };

                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Carrera con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetAllCareersVm>>> GetAll()
        {
            try
            {
                var query = new GetAllCareerListQuery();
                var careers = await _mediator.Send(query);

                if (careers == null || !careers.Any())
                {
                    return NotFound("No se encontraron carreras.");
                }

                return Ok(careers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetAllFilter
        [HttpGet("GetAllFilter")]
        public async Task<ActionResult<IEnumerable<GetAllCareerFilterVm>>> GetAllFilter()
        {
            try
            {
                var query = new GetAllCareerFilterListQuery();
                var careers = await _mediator.Send(query);

                if (careers == null || !careers.Any())
                {
                    return NotFound("No se encontraron carreras.");
                }

                return Ok(careers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion

        #region GetById
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<GetCareerVm>> GetById(int id)
        {
            try
            {
                var command = new GetCareerQuery(id);

                var career = await _mediator.Send(command);

                if (career == null)
                {
                    return NotFound();
                }

                return Ok(career);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Carrera con ID {id} no encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        #endregion
    }
}
