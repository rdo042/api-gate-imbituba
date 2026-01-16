using System.ComponentModel.DataAnnotations;

namespace GateAPI.Infra.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLength(36)]
        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        [StringLength(36)]
        public string? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }
        [StringLength(36)]
        public string? DeletedBy { get; set; }
    }
}
