using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Authentication.Configuration
{
    public class JwtConfig
    {
        public JwtConfig()
        {

        }
        public required string Key { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public int Expires { get; set; }
    }
}
