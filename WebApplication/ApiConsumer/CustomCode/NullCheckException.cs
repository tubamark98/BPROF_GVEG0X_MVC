using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiConsumer
{
    public class NullCheckException : Exception
    {
        public NullCheckException()
        {

        }

        public NullCheckException(string message)
            : base(message)
        {

        }
        public NullCheckException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
