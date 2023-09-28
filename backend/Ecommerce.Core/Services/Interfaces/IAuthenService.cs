﻿using Ecommerce.Core.DTOs;
using Ecommerce.Core.Models;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface IAuthenService
    {
        Task<Response<LoginResponse>> Login(LoginRequest request);
        Task<Response<bool>> Register(RegisterRequest request);
        Task<Response<LoginResponse>> GenerateToken(User user);
    }
}
