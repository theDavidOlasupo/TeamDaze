using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamDaze.BLL.Exceptions
{
    public class GenericException : Exception
    {
        public string ErrorCode { get; private set; }

        public GenericException(string message) : base(message)
        {
        }

        public GenericException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
