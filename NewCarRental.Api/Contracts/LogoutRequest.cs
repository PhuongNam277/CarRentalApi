namespace NewCarRental.Api.Contracts
{
    public class LogoutRequest
    {
        public string RefreshToken { get; set; } = null!;
    }
}
