using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace signal_core
{
    public static class JwtSecurityKey  
    {  
        private const string rahasia="rahasi123sayajugabisabikinartikel";

        public const string Issuer = "SmartCity.Security.Bearer";

        public const string Audience = "SmartCity.Security.Bearer";

        public static SymmetricSecurityKey Create(){
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(rahasia));
        } 
        public static SymmetricSecurityKey Create(string secret)  
        {  
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));  
        }  
    }  
}
