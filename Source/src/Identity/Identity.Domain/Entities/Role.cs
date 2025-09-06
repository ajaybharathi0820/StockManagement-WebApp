using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.Entities
{
    public class Role
    {
        [Key]
    public Guid Id { get; set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }

        public virtual ICollection<User> ? Users {get;set;}

        private Role() { } // EF Core

        public Role(string name)
        {
            Name = name;
            IsActive = true;
        }

        public void UpdateRole(string name)
        {
            Name = name;
        }
        public void DeleteRole()
        {
            IsActive = false;
        }
    }
}
