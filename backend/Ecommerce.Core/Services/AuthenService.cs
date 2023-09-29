using AutoMapper;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Models;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Core.Services
{
    public class AuthenService : IAuthenService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IGenericRepository<UserPosition> _upRepository;
        private readonly ICommonService _common;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthenService(
            IGenericRepository<User> repository,
            IGenericRepository<UserPosition> upRepository,
            ICommonService common,
            IMapper mapper,
            IOptions<JwtSettings> options)
        {
            _repository = repository;
            _upRepository = upRepository;
            _common = common;
            _mapper = mapper;
            _jwtSettings = options.Value;
        }

        public async Task<Response<LoginResponse>> GenerateToken(User user)
        {
            var response = new Response<LoginResponse>();
            var loginResponse = new LoginResponse();
            try
            {
                var claims = new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Sub, _jwtSettings.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString())
                    };
                var roles = await _upRepository.GetListAsync(x => x.UserId == user.UserId && x.Status == "A");
                var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, _common.GetPositionName(x.PositionId.ToString())));
                claims.AddRange(roleClaims);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                   _jwtSettings.Issuer,
                   _jwtSettings.Audience,
                   claims,
                   expires: DateTime.UtcNow.AddMinutes(10),
                   signingCredentials: signIn);

                loginResponse.token = new JwtSecurityTokenHandler().WriteToken(token);
                loginResponse.userId = user.UserId.ToString();
                loginResponse.fullName = user.FullName;
                loginResponse.position = user.Position.PositionName;
                loginResponse.email = user.Email;

                response.Message = "Login Success";
                response.IsSuccess = true;
                response.Value = loginResponse;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<LoginResponse>> Login(LoginRequest request)
        {
            var response = new Response<LoginResponse>();
            try
            {
                var user = await _repository.GetAsync(x => x.Username == request.Username);
                if (user != null && user.Status == "A")
                {
                    request.Password = _common.Encrypt(request.Password);
                    if (user.Password == request.Password)
                        response = await GenerateToken(user);
                    else
                        response.Message = "invaild password";
                }
                else if (user != null && user.Status == "I")
                    response.Message = "user is InActive";
                else
                    response.Message = "not found user";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<bool>> Register(RegisterRequest request)
        {
            var response = new Response<bool>();
            try
            {
                var userExists = await _repository.GetAsync(x => x.Username == request.Username);
                if (userExists != null)
                {
                    response.IsSuccess = false;
                    response.Message = "user already exists";
                }
                else
                {
                    request.Password = _common.Encrypt(request.Password);
                    //request.PositionId = "08FE48BE-AC81-4983-9A0A-4EEB2972C947";
                    var user = await _repository.InsertAsync(_mapper.Map<User>(request)); // Insert Table User
                    if (user != null)
                    {
                        var result = await _upRepository.InsertAsync(_mapper.Map<UserPosition>(user));
                        if (result != null)
                        {
                            response.IsSuccess = true;
                            response.Message = "register successfully";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return response;
        }
    }
}
