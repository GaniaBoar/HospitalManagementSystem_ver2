using HospitalManagementSystem.Common.Entities;
using HospitalManagementSystem.Common.Modal;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.AccountRepo
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly byte[] _secret;

        private readonly ILogger<AccountService> _logger;

        private readonly ConcurrentDictionary<string, RefreshToken> _usersRefreshTokens;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<IdentityResult> SignUpAsync(SignUp signUp)
        {
            var user = new ApplicationUser()
            {
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
                Email = signUp.Email,
                UserName = signUp.Email
            };
            return await _userManager.CreateAsync(user, signUp.Password);
        }

        public async Task<string> LoginAsync(SignIn signIn)
        {
            var result = await _signInManager.PasswordSignInAsync(signIn.Email, signIn.Password, false, false);

            if (!result.Succeeded)
            {
                return null;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, signIn.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public JwtAuthentication GenerateTokens(string userId, Claim[] claims, DateTime now)
        {
            // get claims and bind with user
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                shouldAddAudienceClaim ? _jwtSettings.Audience : string.Empty,
                claims,
                expires: now.AddDays(_jwtSettings.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));

            // generate access token (JWT) containing all claims for user.
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = new RefreshToken
            {
                UserId = userId,
                TokenString = GenerateRefreshTokenString(),
                ExpireAt = now.AddDays(_jwtSettings.RefreshTokenExpiration)
            };
            var _tokenString = refreshToken.TokenString;
            _usersRefreshTokens.AddOrUpdate(_tokenString, refreshToken, (_tokenString, refreshToken) => refreshToken);

            return new JwtAuthentication
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
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

        public void Logout(string id)
        {
              
         
                RemoveRefreshToken(id); // can be more specific to ip, user agent, device name, etc.
                _logger.LogInformation($"User [{id}] logged out the system.");
            
        }
        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
        
}
