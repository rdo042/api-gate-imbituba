using GateAPI.Infra.Models.Configuracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GateAPI.Infra.Persistence.Profile
{
    public class TipoLacreModelConfiguration : IEntityTypeConfiguration<TipoLacreModel>
    {
        public void Configure(EntityTypeBuilder<TipoLacreModel> builder)
        {
            builder.Property(x => x.Status).HasConversion<string>();
        }
    }
}
