namespace Common.Models
{
    public abstract class AuditableEntity
    {
        public DateTime CreatedDate { get; protected set; }
        public string CreatedBy { get; protected set; }
        public DateTime? UpdatedDate { get; protected set; }
        public string? UpdatedBy { get; protected set; }
        public bool IsActive { get; set; } = true;
    }
}
