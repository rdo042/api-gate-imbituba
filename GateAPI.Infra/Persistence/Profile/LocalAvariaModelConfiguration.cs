using GateAPI.Infra.Models.Configuracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GateAPI.Infra.Persistence.Profile
{
    public class LocalAvariaModelConfiguration : IEntityTypeConfiguration<LocalAvariaModel>
    {
        public void Configure(EntityTypeBuilder<LocalAvariaModel> builder)
        {
            builder.Property(x => x.Status).HasConversion<string>();
        }
    }
}
