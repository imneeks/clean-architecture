using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Swagger
{
    internal class SwaggerConfig
    {
        public required string ApiName { get; set; }
        public required string Version { get; set; }
        public required string Description { get; set; }

        public List<string> Styles { get; set; }
        public List<string> Scripts { get; set; }
    }
}
