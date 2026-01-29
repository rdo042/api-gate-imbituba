using GateAPI.Infra.Models.Configuracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GateAPI.Infra.Persistence.Profile
{
    public class TaskFlowTasksModelConfiguration : IEntityTypeConfiguration<TaskFlowTasksModel>
    {
        public void Configure(EntityTypeBuilder<TaskFlowTasksModel> builder)
        {
            builder.Property(x => x.Status).HasConversion<string>();
        }
    }
}
