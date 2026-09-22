using SchoolSystem.Enums;
using System;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Business.Common.Exceptions
{
    public class clsCannotDeleteException : Exception
    {
        public enErrors ErrorType { get; }

        public clsCannotDeleteException(enErrors errorType): base(errorType.ToString())
        {
            ErrorType = errorType;
        }
    }

}
