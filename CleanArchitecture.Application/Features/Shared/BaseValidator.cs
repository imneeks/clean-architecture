using FluentValidation;
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
    public class BaseValidator<T> : AbstractValidator<T> where T : class
    {        
        protected string GetRequiredMessage(string propertyName)
        {
            return GetFormattedMessage("Required", propertyName);
        }

        protected string GetMaxLengthMessage(string propertyName, int maxLength)
        {
            return GetFormattedMessage("MaxLength", propertyName, maxLength);
        }

        private string GetFormattedMessage(string key, params object[] args)
        {
            return string.Format(key.TransformLanguage(), args);
        }      
    }
}
