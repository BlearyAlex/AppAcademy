using AppAcademy.Application.Features.Careers.Commands.CreateCareer;
using AppAcademy.Application.Features.Careers.Commands.DeleteCareer;
using AppAcademy.Application.Features.Careers.Commands.UpdateCareer;
using AppAcademy.Application.Features.Careers.Queries.GetAllCareers;
using AppAcademy.Application.Features.Careers.Queries.GetCareer;
using AppAcademy.Application.Features.Categorias.Queries.GetAllCategoria;
using AppAcademy.Application.Features.Students.Commands.DeleteStudent;
using AppAcademy.Application.Features.Students.Commands.UpdateStudent;
using AppAcademy.Application.Features.Students.Queries.GetAllStudents;
using AppAcademy.Application.Features.Students.Queries.GetStudent;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ControlAcademias
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CareerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CareerController(IMediator mediator)
        {
            _mediator = mediator;
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
