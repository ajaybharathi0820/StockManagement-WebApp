namespace Identity.Application.Users.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public int Age { get; set; }
        public string Email { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}

