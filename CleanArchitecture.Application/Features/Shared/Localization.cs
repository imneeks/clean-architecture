using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Shared
{
    public static class Localization
    {
        public static string TransformLanguage(this string message)
        {
            return message.TransformLanguage("ValidationMessages");
        }

        public static string TransformLanguage(this string message, string resource) {
            ResourceManager ResourceManager = new ResourceManager("CleanArchitecture.Application.Resources.ValidationMessages",
                typeof(Localization).Assembly);

            return ResourceManager.GetString(message, CultureInfo.CurrentUICulture)
               ?? $"Missing localization for {message}";
        }
    }
}
