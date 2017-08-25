using System;

namespace CodeExercise.DressCode.Engine.Violations
{
    public class Violation : Exception
    {
        public Violation(string message) : base(message)
        {
            
        }
    }
}
