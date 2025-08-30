using Identity.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string UserName { get; private set; }
        public int Age { get; private set; }
        public bool IsActive { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public int RoleId { get; private set; }
        //Navigation
        [ForeignKey("RoleId")]
        public virtual Role ? Role {get;set;}

        private User() { } // EF Core

        public User(string firstName, string lastName, string userName, int age, string email, string password, int roleId)
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
        }

        public void ChangePassword(string newPassword)
        {
            Password = newPassword;
        }

        public void UpdateUser(string firstName, string lastName, int age, string email, int roleId)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Email = email;
            RoleId = roleId;
        }

        public void DeleteUser()
        {
            IsActive = false;
        }

    }
}
