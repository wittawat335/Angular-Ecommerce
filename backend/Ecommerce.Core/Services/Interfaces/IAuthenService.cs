using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface IAuthenService
    {
        Task<ResponseApi<LoginResponse>> Login(LoginRequest request);
        Task<ResponseApi<LoginResponse>> Register(LoginRequest request);
        Task<ResponseApi<LoginResponse>> GenerateToken(User user);
    }
}
