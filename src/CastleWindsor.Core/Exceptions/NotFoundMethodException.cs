using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleWindsor.Core.Exceptions
{
    internal class NotFoundMethodException : Exception
    {
        public NotFoundMethodException()
        {
        }

        public NotFoundMethodException(string? message) : base(message)
        {
        }
    }
}
