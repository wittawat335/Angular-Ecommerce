using AutoMapper;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Models;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.RepositoryContracts;
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
        private readonly IGenericRepository<Position> _positionRepository;
        private readonly IGenericRepository<UserPosition> _upRepository;
        private readonly ICommonService _common;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthenService(IGenericRepository<User> repository, IGenericRepository<UserPosition> upRepository,
            IGenericRepository<Position> positionRepository,
            ICommonService common,
            IMapper mapper,
            IOptions<JwtSettings> options)
        {
            _repository = repository;
            _upRepository = upRepository;
            _positionRepository = positionRepository;
            _common = common;
            _mapper = mapper;
            _jwtSettings = options.Value;
        }

        public async Task<Response<string>> AddPosition(PositionRequest request)
        {
            var response = new Response<string>();
            try
            {
                var position = await _positionRepository.GetAsync(x => x.PositionName == request.PositionName);
                if (position != null)
                {
                    response.IsSuccess = Constants.Status.False;
                    response.Message = Constants.StatusMessage.DuplicatePosition;
                }
                else
                {
                    var positionResult = await _positionRepository.InsertAsync(_mapper.Map<Position>(request));
                    if (positionResult != null)
                    {
                        response.IsSuccess = Constants.Status.True;
                        response.Message = Constants.StatusMessage.AddSuccessfully;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = Constants.Status.False;
            }
            return response;
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
                var roles = await _upRepository.GetListAsync(x => x.UserId == user.UserId && x.Status == Constants.Status.Active);
                var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, _common.GetPositionName(x.PositionId.ToString())));
                claims.AddRange(roleClaims);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);

                loginResponse.token = new JwtSecurityTokenHandler().WriteToken(token);
                loginResponse.userId = user.UserId.ToString();
                loginResponse.fullName = user.FullName;
                loginResponse.position = user.Position.PositionName;
                loginResponse.email = user.Email;

                response.Message = Constants.StatusMessage.LoginSuccess;
                response.IsSuccess = Constants.Status.True;
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
                if (user != null && user.Status == Constants.Status.Active)
                {
                    request.Password = _common.Encrypt(request.Password);
                    if (user.Password == request.Password)
                        response = await GenerateToken(user);
                    else
                        response.Message = Constants.StatusMessage.InvaildPassword;
                }
                else if (user != null && user.Status == Constants.Status.Inactive)
                    response.Message = Constants.StatusMessage.InActive;
                else
                    response.Message = Constants.StatusMessage.NotFoundUser;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response<string>> Register(RegisterRequest request)
        {
            var response = new Response<string>();
            try
            {
                var userExists = await _repository.GetAsync(x => x.Username == request.Username);
                if (userExists != null)
                {
                    response.IsSuccess = Constants.Status.False;
                    response.Message = Constants.StatusMessage.DuplicateUser;
                }
                else
                {
                    request.Password = _common.Encrypt(request.Password);
                    var user = await _repository.InsertAsync(_mapper.Map<User>(request)); // Insert Table User
                    if (user != null)
                    {
                        var result = await _upRepository.InsertAsync(_mapper.Map<UserPosition>(user)); // Insert Table UserPosition
                        if (result != null)
                        {
                            response.IsSuccess = Constants.Status.True;
                            response.Message = Constants.StatusMessage.RegisterSuccess;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = Constants.Status.False;
            }

            return response;
        }
    }
}
