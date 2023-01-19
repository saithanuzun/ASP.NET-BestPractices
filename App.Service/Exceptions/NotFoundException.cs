using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Service.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string Message) : base(Message)
        {
            
        }
    }
}