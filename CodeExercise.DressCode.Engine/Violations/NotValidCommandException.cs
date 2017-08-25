
using System;

namespace CodeExercise.DressCode.Engine.Violations
{
    public class NotValidCommandException : Exception
    {
        public NotValidCommandException(string message = "") : base(message)
        {
            
        }
    }
}
