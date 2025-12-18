using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NewCarRental.Api.Contracts;
using NewCarRental.Application.Authentication.Commands.Login;
using NewCarRental.Application.Authentication.Commands.Logout;
using NewCarRental.Application.Authentication.Commands.RefreshToken;
using NewCarRental.Application.Authentication.Commands.Register;

namespace NewCarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginCommand(request.Email, request.Password);
            try
            {
                var token = await _mediator.Send(query);
                return Ok(new { Token = token, Message = "Login successfully!" });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Contracts.RegisterRequest request)
        {
            var command = new RegisterCommand(
                FullName: request.FullName,
                Username: request.Username,
                PasswordHash: request.PasswordHash,
                Email: request.Email,
                IsBlocked:request.IsBlocked,
                RoleId: request.RoleId
            );

            try
            {
                var userId = await _mediator.Send(command);
                return Ok(new { UserId = userId, Message = "Đăng ký thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new {Error = ex.Message});
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var command = new RefreshTokenCommand(request.RefreshToken);

            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            var command = new LogoutCommand(request.RefreshToken);
            await _mediator.Send(command);
            return Ok(new {Message = "Logout successfully!"});
        }

    }
}
