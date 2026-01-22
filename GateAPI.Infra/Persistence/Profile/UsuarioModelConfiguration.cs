using GateAPI.Infra.Models.Configuracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GateAPI.Infra.Persistence.Profile
{
    public class UsuarioModelConfiguration : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
