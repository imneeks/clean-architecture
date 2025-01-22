using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArchitecture.Shared
{
    public sealed record Error(string Code,
          [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            string? Description = null,
          [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            string? CorrelationId = null)
    {
        public static readonly Error None = null; //new Error(string.Empty, null);
        public static readonly Error NullValue = null;// new Error(string.Empty, null);
    }
}
