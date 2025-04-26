namespace AuthenticationService.Models
{
    public class AuthUser
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Primary key (GUID)

        public required string Username { get; set; }

        public required string PasswordHash { get; set; }

        public required string Salt { get; set; }

        public required string Email { get; set; }

        public required string Phone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
