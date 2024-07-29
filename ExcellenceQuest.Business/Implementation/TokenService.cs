namespace ExcellenceQuest.Business.Implementation
{
    using ExcellenceQuest.Business.Contracts;
    using Microsoft.IdentityModel.Protocols.OpenIdConnect;
    using Microsoft.IdentityModel.Protocols;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Configuration;
    using Microsoft.Extensions.Configuration;
    using System.Net.Http;
    using Microsoft.AspNetCore.Http;
    using ExcellenceQuest.Repository.Models;
    using ExcellenceQuest.Models.Token;

    public class TokenService : ITokenService
    {
        private const string InvalidTokenMessage = "Invalid Token";
        private const string EmptyTokenMessage = "Token is empty";
        private const string NoClaimsMessage = "No claims found";
        public IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        public TokenService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri($"{_configuration.GetValue<string>("SSOSettings:Url")}");
        }

        public async Task<bool> CheckMicrosoftTokenAsync(TokenRequestModel request)
        {
            string stsDiscoveryEndpoint = "https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration";

            var configManager = new ConfigurationManager<OpenIdConnectConfiguration>(
       stsDiscoveryEndpoint,
       new OpenIdConnectConfigurationRetriever());

            OpenIdConnectConfiguration config = configManager.GetConfigurationAsync().Result;

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKeys = config.SigningKeys,
                ValidateLifetime = false
            };

            JwtSecurityTokenHandler tokendHandler = new JwtSecurityTokenHandler();

            SecurityToken jwt;

            var result = tokendHandler.ValidateToken(request.Id_token, validationParameters, out jwt);
            var isAuthenticated = (result.Identity.IsAuthenticated);
            return isAuthenticated;

        }

        public string GenerateToken(TokenResponseModel response)
        {
            var claimsData = new List<Claim>
            {
                new Claim("email", response.User.UserName),
                new Claim("Id", response?.Employee?.Id.ToString()),
                new Claim("client", response?.ClientName),
                new Claim("first_name", response?.Employee?.Name),
                 new Claim("role", response?.UserRole?.Role.Name)
            };
            if (response?.EmpCareerUserId != null)
            {
                claimsData.Add(new Claim("UserId", response?.EmpCareerUserId.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jkhdlkfjsljdkjfandflkahjkdfncasdfhkjashfksdfhsajkfhas"));
            var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                issuer: _configuration["TokenValidationParameters:IssuerAndAudience"],
                audience: _configuration["TokenValidationParameters:IssuerAndAudience"],
                expires: DateTime.Now.AddMonths(2),
                claims: claimsData,
                signingCredentials: signInCred
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
