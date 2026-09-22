using SchoolSystem.Enums;
using System;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Business.Common.Exceptions
{
    public class clsCannotAddException : Exception
    {
        public enErrors ErrorType { get; }

        public clsCannotAddException(enErrors errorType)
            : base(errorType.ToString())
        {
            ErrorType = errorType;
        }
    }
}