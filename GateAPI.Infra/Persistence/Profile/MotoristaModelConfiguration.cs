using GateAPI.Infra.Models.Configuracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GateAPI.Infra.Persistence.Profile
{
    public class MotoristaModelConfiguration : IEntityTypeConfiguration<MotoristaModel>
    {
        public void Configure(EntityTypeBuilder<MotoristaModel> builder)
        {
            builder.ToTable("motoristas");
        }
    }
}
