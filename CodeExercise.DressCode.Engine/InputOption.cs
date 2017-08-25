using System;
using System.Collections.Generic;
using System.Linq;
using CodeExercise.DressCode.Engine.ValueObjects;

namespace CodeExercise.DressCode.Engine
{
    public class InputOption : IInputOption
    {
        public string[] GetTemperatureOptions()
        {
            return Enum.GetNames(typeof(Temperature));
        }
        public IDictionary<int,string> GetCommands()
        {
            return Enum.GetValues(typeof(Command))
                .Cast<Command>()
                .ToDictionary(t => (int)t, t => t.ToString());
        }
    }
}
