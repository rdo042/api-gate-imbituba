namespace GateAPI.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        protected void SetId(Guid id) => Id = id;

        protected void SetAudit(
            DateTime createdAt, string createdBy,
            DateTime? updatedAt, string? updatedBy,
            DateTime? deletedAt, string? deletedBy
            )
        {
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
            DeletedAt = deletedAt;
            DeletedBy = deletedBy;
        }
    }
}
