using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AttendanceSystem.Utility
{
    public static class JwtHelper
    {
        public  static string SecretKey { get; set; } = string.Empty;
        public static byte[] Key {get;set;}
        public static string strAudince { get; set; } = string.Empty;
        public static string strIssuer { get; set; } = string.Empty;


        public static string GenerateJwtToken(string username, int userId, string password)
        {
            var claims = new[]
            {
                new Claim("Username", username),
                new Claim("UserId", userId.ToString()),
                new Claim("Password", password)  // Storing password (Only for development; not recommended in production)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature),
                Audience = strAudince,
                Issuer = strIssuer,

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static bool ValidToken(string paramTopken)
        {
            JwtSecurityTokenHandler _objJwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey _objSymmetricSecurityKey = new SymmetricSecurityKey(Key);
            try
            {
                _objJwtSecurityTokenHandler.ValidateToken(paramTopken, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = strIssuer,
                    ValidAudience = strAudince,
                    IssuerSigningKey = _objSymmetricSecurityKey,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken _objSecurityToken);
                JwtSecurityToken _objJwtSecurityToken = (JwtSecurityToken)_objSecurityToken;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}



#region old code
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.IdentityModel.Tokens;

//namespace RoleBasedAccessAPI.Utility
//{
//    public static class JwtHelper
//    {
//        //private static readonly string SecretKey = "your-super-secret-key";
//        //private static readonly string SecretKey = "MTSU2025!SuperSecureJWTKey2025!";
//        private static readonly string SecretKey = "MTSU2025!SuperSecureJWTKey2025!ExtraSecure";


//        //private static readonly byte[] Key = Encoding.UTF8.GetBytes(SecretKey);
//        private static readonly byte[] Key = Encoding.ASCII.GetBytes(SecretKey);


//        public static string GenerateJwtToken(string Username, string Password)
//        {
//            var claims = new[]
//            {
//                new Claim("Username", Username),
//                new Claim("Password", Password)
//            };

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(claims),
//                Expires = DateTime.UtcNow.AddHours(2),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
//            };

//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            return tokenHandler.WriteToken(token);
//        }
//    }
//}
#endregion