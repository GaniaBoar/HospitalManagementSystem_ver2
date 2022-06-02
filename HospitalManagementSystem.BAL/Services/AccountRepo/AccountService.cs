using Hospital.BAL.Configurations;
using HospitalManagementSystem.Common.Entities;
using HospitalManagementSystem.Common.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.AccountRepo
{
    public class AccountService : IAccountService, IDisposable
    {
        #region Private Members
        private const string _smsSessionKey = "_@SMS.OTP";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary =>
            _usersRefreshTokens.ToImmutableDictionary();

        private readonly ConcurrentDictionary<string, RefreshToken> _usersRefreshTokens;
        private readonly JWTSettings _jwtSettings;
        private readonly byte[] _secret;
        private readonly ILogger<AccountService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly AppDbContext _db;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        #endregion

        public AccountService(
            IHttpContextAccessor httpContextAccessor,
            ILogger<AccountService> logger,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext context,
            JWTSettings jWTSettings)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _jwtSettings = jWTSettings;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = context;
            _usersRefreshTokens = new ConcurrentDictionary<string, RefreshToken>();
            _secret = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
        }

        #region Public Methods

        public void RemoveExpiredRefreshTokens(DateTime now, CancellationToken ct = default)
        {
            var expiredTokens = _usersRefreshTokens.
          Where(x => x.Value.ExpireAt < now).ToList();
            foreach (var expiredToken in expiredTokens)
            {
                _usersRefreshTokens.TryRemove(expiredToken.Key, out _);
            }
        }

       
        public void RemoveRefreshToken(string userId, CancellationToken ct = default)
        {
            var refreshTokens = _usersRefreshTokens.
           Where(x => x.Value.User.Id == userId).ToList();
            foreach (var refreshToken in refreshTokens)
            {
                _usersRefreshTokens.TryRemove(refreshToken.Key, out _);
            }
        }

        
        public JwtAuthResult GenerateTokens(string userId, Claim[] claims, DateTime now)
        {
           
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                shouldAddAudienceClaim ? _jwtSettings.Audience : string.Empty,
                claims,
                expires: now.AddDays(_jwtSettings.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = new RefreshToken
            {
                UserId = userId,
                TokenString = GenerateRefreshTokenString(),
                ExpireAt = now.AddDays(_jwtSettings.RefreshTokenExpiration)
            };
            var _tokenString = refreshToken.TokenString;
            _usersRefreshTokens.AddOrUpdate(_tokenString, refreshToken, (_tokenString, refreshToken) => refreshToken);

            return new JwtAuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

        }

      
        public JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now)
        {
            var (principal, jwtToken) = DecodeJwtToken(accessToken);
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException("Invalid token");
            }

            var phoneNumber = principal.Identity?.Name;
            if (!_usersRefreshTokens.TryGetValue(refreshToken, out var existingRefreshToken))
            {
                throw new SecurityTokenException("Invalid token");
            }
            if (existingRefreshToken.UserId != phoneNumber || existingRefreshToken.ExpireAt < now)
            {
                throw new SecurityTokenException("Invalid token");
            }

            return GenerateTokens(phoneNumber, principal.Claims.ToArray(), now); // need to recover the original claims

        }

        
        public (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new SecurityTokenException("Invalid token");
            }
            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _jwtSettings.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(_secret),
                        ValidAudience = _jwtSettings.Audience,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    },
                    out var validatedToken);
            return (principal, validatedToken as JwtSecurityToken);

        }



        public async Task<object> LoginAsync(SignIn values, CancellationToken ct = default)
        {
            if (!string.IsNullOrEmpty(values.Email))
            {
                var user = await _userManager.FindByEmailAsync(values.Email.Trim());
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                var isValidLogin = await _userManager.CheckPasswordAsync(user, values.Password);

                if (isValidLogin)
                {
                   
                    user.UserRoles = (List<string>)await _userManager.GetRolesAsync(user);
                    return user;
                }
            }
            return new
            {
                error = true,
                message = "Invalid credentials, try again."
            };
        }

        public void Logout(string id)
        {
            RemoveRefreshToken(id); 
            _logger.LogInformation($"User [{id}] logged out the system.");
        }


        public async Task<ServiceResult> SignUpAsync(SignUp model, CancellationToken ct = default)
        {
            List<string> _errors = new List<string>();

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName=model.FirstName,
                LastName=model.LastName,
            };

            var result = (model.Password != null && model.Email != null)
                ? await _userManager.CreateAsync(user, model.Password)
                : await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);

                await _db.SaveChangesAsync();

                return new ServiceResult("User Created");
            }
            foreach (var item in result.Errors)
            {
                _errors.Add($"{item.Code}: {item.Description}");
            }
            
            _logger.LogError($"Error occurred creating user => {user}");
            return new ServiceResult("Error creating user")
            {
                Errors = _errors
            };
        }

        public async Task<ServiceResult> UpdateUser(SignUp userInput, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(userInput.Id);

            if (user == null)
                return new ServiceResult("User not found")
                {
                    Errors = new List<string>()
                    {
                        $"User could not be found with {userInput.Id}"
                    }
                };

            user.UserName = userInput.Email;
            user.Email = userInput.Email;
            user.FirstName = userInput.FirstName;
            user.LastName = userInput.LastName;

            
            if (userInput.Role != null)
            {
                var _roles = await _userManager.GetRolesAsync(user);
                if (_roles.Count > 0)
                    await _userManager.RemoveFromRolesAsync(user, _roles);

                
                await _userManager.AddToRoleAsync(user, userInput.Role);
            }
            
            var result = await _userManager.UpdateAsync(user);
            return new ServiceResult(result.Succeeded ? "Saved" : result.Errors.GetEnumerator().Current.Description)
            {
                Errors = result.Succeeded ? null : new List<string>
                {
                    result.Errors.GetEnumerator().Current.Description
                }
            };

        }

        public async Task<ServiceResult> CreateRole(RolesInputModel model, CancellationToken ct = default)
        {
            if (await _roleManager.RoleExistsAsync(model.Name.ToLower().Trim()))
            {
                return new ServiceResult("Role already exists")
                {
                    Errors = new List<string>()
                        {
                            "Role already exists!"
                        }
                };
            }
            var role = new IdentityRole { Name = model.Name };
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
                return new ServiceResult(result.Errors.SingleOrDefault().Description)
                {
                    Errors = result.Errors.Select(a => a.Description).ToList()
                };

            await _db.SaveChangesAsync();
            return new ServiceResult("Role Created");
        }

        public async Task<ServiceResult> UpdateRole(RolesInputModel model, CancellationToken ct = default)
        {
            var role = await _roleManager.Roles.SingleOrDefaultAsync(a => a.Name == model.Name.Trim());

            if (role == null)
            {
                return new ServiceResult("Role doesn't exists!")
                {
                    Errors = new List<string>()
                        {
                            "Role doesn't exists!"
                        }
                };
            }

            role.Name = model.Name;

            await _db.SaveChangesAsync();
            return new ServiceResult("Saved");

        }

        public async Task<ServiceResult> GetRoles(CancellationToken ct = default)
        {
            var roles = await _db.Roles.ToListAsync();
            return new ServiceResult
            {
                Data = roles
            };
        }

        public async Task<ServiceResult> GetRoles(string role, CancellationToken ct = default)
        {
            var roles = await _db.Roles.SingleOrDefaultAsync(a => a.Name == role);
            if (roles == null)
            {
                throw new ArgumentNullException(nameof(roles));
            }
           

            return new ServiceResult(roles == null ? "No Role found" : null)
            {
                Errors = roles == null ? new List<string>
                    {
                        $"No Role found with name => {role}"
                    } : null,
                Data = roles ?? null
            };

        }

        public async Task ResetPassword(PasswordInputModal passwordInput) // TODO: change later - check old password.
        {
            var user = await _userManager.FindByIdAsync(passwordInput.UserId);
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, passwordInput.Password);

        }

        
        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }



        public void Dispose()
        {
            _db.Dispose();
        }

        
        public async Task<ServiceResult> GetUsers(CancellationToken ct = default)
        {
            var users = await _db.Users.ToListAsync();
            return new ServiceResult
            {
                Data = users
            };
        }




        #endregion
    }
}
