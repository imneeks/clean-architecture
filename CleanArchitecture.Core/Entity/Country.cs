using CleanArchitecture.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Entity
{
    public class Country : SoftDeleteEntity, IEntityWithId
    {        

        public Country()
        {

        }

        public int CountryId { get; set; }
        public required string Name { get; set; }
        public required string CountryCode { get; set; }
        public required string CountryDialCode { get; set; }

        public object Id => CountryId;
    }
}
