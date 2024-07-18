using Core.Extensions;
using Entities.Concrete;
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

namespace Core.Utilities.Security.JWT
{
    public class TokenHandler : ITokenHandler
    {
       public IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateToken(User user, List<OperationClaim> operationClaims,long customerId, string customerName)
        {
            Token token = new Token();
            //Secyrity Key simetriği alınıyor.
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //Şifrelenmiş kimlik oluşturma
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //Token ayarları
            token.Expiration=DateTime.Now.AddMinutes(60);//token süresi 60 dk sonra bitsin
            JwtSecurityToken securityToken= new JwtSecurityToken(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires:token.Expiration,
                claims:SetClaims(user,operationClaims,customerId,customerName),
                notBefore:DateTime.Now,
                signingCredentials:signingCredentials
                );

            //Token oluşturucu sınıfından örnek alma
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //Token üretme
            token.AccessToken=jwtSecurityTokenHandler.WriteToken(securityToken);

            //Refresh token üretme
            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()//Refresh token oluşturur
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random=RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        private IEnumerable<Claim> SetClaims(User user,List<OperationClaim> operationClaims, long customerId, string customerName)
        {
            var claims= new List<Claim>();
            claims.AddName(user.Name);
            claims.AddCustomer(customerId.ToString());
            claims.AddCustomerName(customerName.ToString());
            claims.AddRoles(operationClaims.Select(p=>p.Name).ToArray());            
            return claims;
        }
    }
}
