using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Infra.Mappers.Configuracao
{
    public class MotoristaMapper : IMapper<Motorista, MotoristaModel>
    {
        public Motorista ToDomain(MotoristaModel model)
        {
            var entity = Motorista.Load(id: model.Id,
                                nome: model.Nome,
                                dataNascimento: model.DataNascimento,
                                rg: model.RG,
                                cpf: model.CPF,
                                cnh: model.CNH,
                                validadeCNH: model.ValidadeCNH,
                                telefone: model.Telefone,
                                foto: model.Foto,
                                status: model.Status);
            return entity;
        }

        public MotoristaModel ToModel(Motorista entity)
        {
            var model = new MotoristaModel();

            model.Id = entity.Id;
            model.Telefone = entity.Telefone??"";
            model.Nome = entity.Nome;
            model.DataNascimento = entity.DataNascimento;
            model.CPF = entity.CPF.Value;
            model.RG = entity.RG.Value;
            model.CNH = entity.CNH.Value;
            model.ValidadeCNH = entity.ValidadeCNH;
            model.Foto = entity.Foto??"";
            model.Status = entity.Status;

            return model;
        }
    }
}