using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Entity
{
    public abstract class BaseEntity
    {
        public int CreatedBy { get; set; }       
        public DateTime CreatedAt { get; set; }       
    }
}
