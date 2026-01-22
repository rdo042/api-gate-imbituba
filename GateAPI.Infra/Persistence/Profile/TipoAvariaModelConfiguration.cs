using GateAPI.Infra.Models.Configuracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GateAPI.Infra.Persistence.Profile
{
    public class TipoAvariaModelConfiguration : IEntityTypeConfiguration<TipoAvariaModel>
    {
        public void Configure(EntityTypeBuilder<TipoAvariaModel> builder)
        {
            builder.ToTable("tipo_avaria");
        }
    }
}
