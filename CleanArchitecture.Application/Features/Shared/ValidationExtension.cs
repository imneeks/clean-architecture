using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Shared
{
    public static class ValidationExtension
    {
        public static IRuleBuilderOptions<T, string> MaxLength<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            int maxLength,
            string message)
        {
            return ruleBuilder
                .MaximumLength(maxLength)
                .WithMessage(message);
        }

        // For string properties
        public static IRuleBuilderOptions<T, string> Required<T>(this IRuleBuilder<T, string> ruleBuilder, string message)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage(message);
        }

        // For value types like int, double, Guid, etc.
        public static IRuleBuilderOptions<T, TProperty> Required<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string message)
            where TProperty : struct
        {
            return ruleBuilder
                .NotNull()
                 .WithMessage(message);
        }

        // For nullable value types
        public static IRuleBuilderOptions<T, TProperty?> Required<T, TProperty>(this IRuleBuilder<T, TProperty?> ruleBuilder, string message)
            where TProperty : struct
        {
            return ruleBuilder
                .NotNull()
                 .WithMessage(message);
        }
    }
}
