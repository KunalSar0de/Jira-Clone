using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Jira.Models;
using Jira.Request;
using Jira.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Jira.Services.Impl
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IIdEncoderDecoder _idEncoderDecoder;

        public JwtTokenService(
            IConfiguration configuration, 
            IIdEncoderDecoder idEncoderDecoder
        )
        {
            _configuration = configuration;
            _idEncoderDecoder = idEncoderDecoder;
        }

        public JwtTokenCreationResponse CreateJwtToken(LoginRequest loginRequest, User user)
        {
            string tokenIssuer = _configuration["Token:Issuer"];
            string secretSigningKey = _configuration["Token:SecretKey"];
            string audience = _configuration["Token:Audiance"];
            int tokenValidity = Convert.ToInt32(_configuration["Token:TokenValidity"]);


            var claims = new List<Claim>(){
                new ("id", user.Id.ToString())
            };


            var token = new JwtSecurityToken(
                issuer: tokenIssuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(tokenValidity),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretSigningKey)), SecurityAlgorithms.HmacSha256)

            );

            return new JwtTokenCreationResponse(){
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId =  user.Id
            } ;
        }

        public int? ValidateJwtToken(string jwtToken)
        {
            if (jwtToken == null)
                return null;

            string tokenIssuer = _configuration["Token:Issuer"];
            string secretSigningKey = _configuration["Token:SecretKey"];
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretSigningKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                }, out SecurityToken validatedToken);
            
                var jwt = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwt.Claims.First(c => c.Type == "id").Value);
                return userId;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
    }
}