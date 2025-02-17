using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Marcas.Command.CreateMarca
{
    public class CreateMarcaCommandHandler : IRequestHandler<CreateMarcaCommand, string>
    {
        private readonly IMarcaRepository _repository;
        private readonly IMapper _mapper;

        public CreateMarcaCommandHandler(IMarcaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateMarcaCommand request, CancellationToken cancellationToken)
        {
            var marca = new Marca
            {
                Nombre = request.Nombre,
                Color = request.Color,
                Description = request.Description,
                FechaRegistro = DateTime.Now,
            };

            var newMarca = await _repository.AddAsync(marca);

            return newMarca.MarcaId;
        }
    }
}
