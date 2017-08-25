using System;

namespace CodeExercise.DressCode.Engine.Violations
{
    public class NotValidTemperatureException : Exception
    {
        public NotValidTemperatureException(string message = "") : base(message)
        {
            
        }
    }
}
