using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads;
using BaseInsightDotNet.Business.Payloads.Converters;
using BaseInsightDotNet.Business.Payloads.RequestModels.UserRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataUser;
using BaseInsightDotNet.Commons.Extensions;
using BaseInsightDotNet.Commons.UtilitiesGlobal;
using BaseInsightDotNet.Core.Entities;
using BaseInsightDotNet.DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.ImplementServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly UserConverter _converter;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IRepository<ApplicationUserRole> _userRoleRepository;
        private readonly IRepository<ApplicationRole> _roleRepository;
        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration, UserConverter converter, IRepository<RefreshToken> refreshTokenRepository, IRepository<ApplicationUserRole> userRoleRepository, IRepository<ApplicationRole> roleRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _converter = converter;
            _refreshTokenRepository = refreshTokenRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        public async Task AssignRoleToUserAsync(List<string> roles, ApplicationUser user)
        {
            var assignedRole = new List<string>();
            foreach (var role in roles)
            {
                if (await _roleManager.RoleExistsAsync(role))
                {
                    if (!await _userManager.IsInRoleAsync(user, role))
                    {
                        await _userManager.AddToRoleAsync(user, role);
                        assignedRole.Add(role);
                    }
                }
            }
        }

        public async Task<ResponseObject<DataResponseUser>> RegisterUser(Request_Register registerUser)
        {
            try
            {
                var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
                if (userExist != null)
                {
                    return new ResponseObject<DataResponseUser> { Status = StatusCodes.Status400BadRequest, Data = null, Message = "Người dùng đã tồn tại!" };
                }
                var userNameExist = await _userManager.FindByNameAsync(registerUser.Username);
                if (userNameExist != null)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Data = null,
                        Message = "Tên tài khoản đã tồn tại trên hệ thống",
                        Status = StatusCodes.Status400BadRequest
                    };
                }
                ApplicationUser user = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = registerUser.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = registerUser.Username,
                    AvatarUrl = "dfđf",
                    DateOfBirth = registerUser.DateOfBirth,
                    FullName = registerUser.FullName,
                    Gender = registerUser.Gender,
                    PhoneNumber = registerUser.PhoneNumber,
                    TwoFactorEnabled = false,
                    DepartmentId = registerUser.DepartmentId,
                    PositionId = registerUser.PositionId,
                };
                IdentityResult result = null;
                if (registerUser.Password.IsNullOrEmpty())
                {
                    result = await _userManager.CreateAsync(user);
                }
                else
                {
                    result = await _userManager.CreateAsync(user, registerUser.Password);
                }
                if (result.Succeeded)
                {
                    //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                     var resultAddRole =  await _userManager.AddToRolesAsync(user, new List<string> { "User" });
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status200OK,
                        Data = _converter.EntityToDTO(user),
                        Message = "Đăng ký thành công"
                    };
                }
                else
                {
                    return new ResponseObject<DataResponseUser> { Status = StatusCodes.Status400BadRequest, Data = null, Message = "Đăng ký người dùng thất bại" };
                }
            }catch (Exception ex)
            {
                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Data = null,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseObject<DataResponseLogin>> GetJwtTokenAsync(ApplicationUser user)
        {
            var permissions = await _userRoleRepository.GetAllAsync(x => x.UserId == user.Id);
            var roles = await _roleRepository.GetAllAsync();

            var authClaims = new List<Claim>
    {
        new Claim("Id", user.Id.ToString()),
        new Claim("UserName", user.UserName),
        new Claim("Email", user.Email),
        new Claim("FullName", user.FullName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };

            foreach (var permission in permissions)
            {
                foreach (var role in roles)
                {
                    if (role.Id == permission.RoleId)
                    {
                        authClaims.Add(new Claim("Permission", role.Name));
                    }
                }
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtToken = GetToken(authClaims);
            var refreshToken = GenerateRefreshToken();
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidity"], out int refreshTokenValidity);

            RefreshToken rf = new RefreshToken
            {
                UserId = user.Id,
                ExpiryTime = DateTime.UtcNow.AddHours(refreshTokenValidity),
                Token = refreshToken
            };

            rf = await _refreshTokenRepository.CreateAsync(rf);

            return new ResponseObject<DataResponseLogin>
            {
                Status = StatusCodes.Status200OK,
                Message = "Token created successfully",
                Data = new DataResponseLogin
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    RefreshToken = refreshToken
                }
            };
        }

        public async Task<ResponseObject<DataResponseLogin>> Login(Request_Login request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                {
                    return new ResponseObject<DataResponseLogin>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Tài khoản không chính xác",
                        Data = null
                    };
                }

                var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!checkPassword)
                {
                    return new ResponseObject<DataResponseLogin>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mật khẩu không chính xác",
                        Data = null
                    };
                }

                var jwtTokenResponse = await GetJwtTokenAsync(user);

                return new ResponseObject<DataResponseLogin>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Đăng nhập thành công",
                    Data = new DataResponseLogin
                    {
                        AccessToken = jwtTokenResponse.Data.AccessToken,
                        RefreshToken = jwtTokenResponse.Data.RefreshToken,
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseLogin>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }

        }

        public async Task<ResponseObject<DataResponseLoginOTP>> GetOtpByLoginAsync(Request_Login loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, true);
                if (user.TwoFactorEnabled)
                {
                    var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                    return new ResponseObject<DataResponseLoginOTP>
                    {
                        Data = new DataResponseLoginOTP()
                        {
                            Token = token,
                            IsTwoFactorEnable = user.TwoFactorEnabled
                        },
                        Status = StatusCodes.Status200OK,
                        Message = $"OTP đã được gửi về email {user.Email}"
                    };

                }
                else
                {
                    return new ResponseObject<DataResponseLoginOTP>
                    {
                        Data = new DataResponseLoginOTP()
                        {
                            Token = string.Empty,
                            IsTwoFactorEnable = user.TwoFactorEnabled
                        },
                        Status = 200,
                        Message = $"2FA chưa được kích hoạt"
                    };
                }
            }
            else
            {
                return new ResponseObject<DataResponseLoginOTP>
                {
                    Data = null,
                    Status = StatusCodes.Status404NotFound,
                    Message = $"Người dùng không tồn tại"
                };
            }
        }

        public async Task<ResponseObject<DataResponseLogin>> LoginUserWithJWTokenAsync(string otp, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var signIn = await _signInManager.TwoFactorSignInAsync("Email", otp, false, false);

            /*
             "Email": Chỉ định phương thức xác thực 2 yếu tố được gửi qua email.

              otp: đây là cái otp mà cái thằng người dùng nhập vào.

              false1: tức là khi trình duyệt đóng thì phiên đăng nhập sẽ hết hạn.

              false nếu đặt thành true thì hệ thống sẽ ghi nhớ phiên đăng nhập và lần sau không cần phải xác minh 2 bước nữa
             */
            if (signIn.Succeeded)
            {
                if (user != null)
                {
                    return await GetJwtTokenAsync(user);
                }
            }
            return new ResponseObject<DataResponseLogin>
            {

                Data = null,
                Status = StatusCodes.Status400BadRequest,
                Message = $"Mã OTP không hợp lệ"
            };
        }

        public async Task<ResponseObject<DataResponseLogin>> RenewAccessTokenAsync(Request_RenewAccessToken token)
        {
            var principal = GetClaimsPrincipal(token.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            var refreshToken = await _refreshTokenRepository.GetAsync(record => record.UserId.Equals(user.Id));
            if (refreshToken != null && refreshToken.ExpiryTime <= DateTime.Now)
            {
                return new ResponseObject<DataResponseLogin>
                {

                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = $"Token không hợp lệ hoặc hết hạn"
                };
            }
            var response = await GetJwtTokenAsync(user);
            return response;
        }

        #region PrivateMethods
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInHours"], out int tokenValidityInMinutes);
            var expirationTimeUtc = DateTime.UtcNow.AddHours(tokenValidityInMinutes);
            var localTimeZone = TimeZoneInfo.Local;
            var expirationTimeInLocalTimeZone = TimeZoneInfo.ConvertTimeFromUtc(expirationTimeUtc, localTimeZone);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: expirationTimeInLocalTimeZone,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new Byte[64];
            var range = RandomNumberGenerator.Create();
            range.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal GetClaimsPrincipal(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

            return principal;

        }



        #endregion
    }
}
