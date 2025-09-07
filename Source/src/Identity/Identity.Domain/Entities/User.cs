using Identity.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.Entities
{
    using Common.Models;

    public class User : AuditableEntity
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string UserName { get; private set; }
        public int Age { get; private set; }
    // ...existing code...
        public string Email { get; private set; }
        public string Password { get; private set; }

        public Guid RoleId { get; private set; }
        //Navigation
        [ForeignKey("RoleId")]
        public virtual Role ? Role {get;set;}

        private User() { } // EF Core

        public User(string firstName, string lastName, string userName, int age, string email, string password, Guid roleId)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Age = age;
            Email = email;
            Password = password;
            RoleId = roleId;
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
            CreatedBy = "System";
        }

        public void ChangePassword(string newPassword)
        {
            Password = newPassword;
        }

        public void UpdateUser(string firstName, string lastName, int age, string email, Guid roleId)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Email = email;
            RoleId = roleId;
        }

        // Audit helpers
        public void MarkCreated(string createdBy)
        {
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
            CreatedBy = createdBy;
        }

        public void MarkUpdated(string updatedBy)
        {
            UpdatedDate = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }

        public void DeleteUser()
        {
            IsActive = false;
        }

    }
}
