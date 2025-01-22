using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Shared
{
    public class ItemNotFoundException : KeyNotFoundException
    {

        public ItemNotFoundException() : base("The specified Record not found") { }

        public ItemNotFoundException( string message) : base(message) {
            
        }         
    }
}
    