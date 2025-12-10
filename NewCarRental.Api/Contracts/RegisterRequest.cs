using System.ComponentModel.DataAnnotations;

namespace NewCarRental.Api.Contracts
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "FullName không được để trống")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage ="Username không được để trống")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage ="Định dạng email không hợp lệ")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [MinLength(8, ErrorMessage ="Mật khẩu phải có ít nhất 8 ký tự")]
        public string PasswordHash { get; set; } = "12345678";

        public int IsBlocked { get; set; } = 0;
        public int RoleId { get; set; } = 2;
    }
}
