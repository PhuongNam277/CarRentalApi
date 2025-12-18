using System.ComponentModel.DataAnnotations;

namespace NewCarRental.Api.Contracts
{
    public class RegisterRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = "12345678";

        public int IsBlocked { get; set; } = 0;
        public int RoleId { get; set; } = 2;
    }
}
