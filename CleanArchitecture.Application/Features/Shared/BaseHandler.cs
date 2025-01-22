using CleanArchitecture.Application.Resources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Shared
{
    public class BaseHandler 
    {
        public BaseHandler()
        {

        }   
        
        public string NotFound(string entityName)
        {
            return string.Format("NotFound".TransformLanguage(), entityName);
        }
    }
}
