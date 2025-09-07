using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BagType.Domain.Entities
{
    using Common.Models;

    public class BagType : AuditableEntity
    {
    public Guid Id { get; set; }          // Primary Key
        public string Name { get; set; } = string.Empty;
        public decimal Weight { get; set; }  // Weight per bag type
    // ...existing code...
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Audit helpers to set protected properties from the aggregate itself
        public void MarkCreated(string createdBy)
        {
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
            CreatedBy = createdBy;
            CreatedAt = CreatedDate;
        }

        public void MarkUpdated(string updatedBy)
        {
            UpdatedDate = DateTime.UtcNow;
            UpdatedBy = updatedBy;
            UpdatedAt = UpdatedDate;
        }
    }
}