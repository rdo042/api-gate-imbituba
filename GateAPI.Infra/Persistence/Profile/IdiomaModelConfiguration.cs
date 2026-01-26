using GateAPI.Infra.Models.Configuracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GateAPI.Infra.Persistence.Profile
{
    public class IdiomaModelConfiguration : IEntityTypeConfiguration<IdiomaModel>
    {
        public void Configure(EntityTypeBuilder<IdiomaModel> builder)
        {
            builder.Property(x => x.Status).HasConversion<string>();
            builder.Property(x => x.Canal).HasConversion<int>();
        }
    }
}
