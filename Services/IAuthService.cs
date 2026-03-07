using CreditCardAPI.DTOs;

namespace CreditCardAPI
{
    public interface IAuthService
    {
        public Task<string>  Register(RegisterDto dto);

        Task<string> Login(LoginDto dto);
    }
}