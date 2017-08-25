using System.Collections.Generic;

namespace CodeExercise.DressCode.Engine
{
    public interface IInputOption 
    {
        string[] GetTemperatureOptions();
        IDictionary<int, string> GetCommands();
    }
}
