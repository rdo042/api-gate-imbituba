using GateAPI.Infra.Models.Configuracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GateAPI.Infra.Persistence.Profile
{
    public class TraducaoModelConfiguration : IEntityTypeConfiguration<TraducaoModel>
    {
        public void Configure(EntityTypeBuilder<TraducaoModel> builder)
        {
            builder.HasIndex(x => x.IdIdioma);

            builder.HasIndex(x => new { x.IdIdioma, x.Chave }).IsUnique();
        }
    }
}
