using GateAPI.Application.Common.Models;
using GateAPI.Application.UseCases.Configuracao.MotoristaUC.Criar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.MotoristaUC.Criar
{
    public class CriarMotoristaCommandHandler : IRequestHandler<CriarMotoristaCommand, Result<Motorista>>
    {
        public readonly IMotoristaRepository _repository;

        public CriarMotoristaCommandHandler(IMotoristaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Motorista>> Handle(CriarMotoristaCommand request, CancellationToken cancellationToken)
        {
            var byCpf = await _repository.GetByDocumentoAsync(request.CPF, cancellationToken);
            if(byCpf != null)
                return Result<Motorista>.Failure("Já existe um motorista cadastrado com este CPF.");

            var byCNH = await _repository.GetByDocumentoAsync(request.CPF, cancellationToken);
            if(byCNH != null)
                return Result<Motorista>.Failure("Já existe um motorista cadastrado com esta CNH.");

            var motorista = Motorista.Create(
                            request.Nome,
                            request.DataNascimento,
                            request.RG,
                            request.CPF,
                            request.CNH,
                            request.ValidadeCnh,
                            request.Telefone,
                            request.Foto,
                            request.Status
                        );

            var result = await _repository.AddAsync(motorista, cancellationToken);
            return Result<Motorista>.Success(result);
        }
    }
}