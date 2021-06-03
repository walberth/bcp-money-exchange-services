namespace MoneyExchange.Service.Api.Core
{
    using System;
    using System.Text;
    using Transversal.Common;
    using System.Security.Claims;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;

    internal static class TokenGenerator
    {
        public static Token BuildToken(TokenParams tokenParams)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(tokenParams.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, tokenParams.Username)
            });

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: tokenParams.Audience,
                issuer: tokenParams.Issuer,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(Environment.GetEnvironmentVariable("JWT_EXPIRATION"))),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);

            return new Token
            {
                TokenValue = jwtTokenString,
                ExpirationTime = DateTime.UtcNow.AddMinutes(Convert.ToInt32(Environment.GetEnvironmentVariable("JWT_EXPIRATION")))
            };
        }
    }
}
