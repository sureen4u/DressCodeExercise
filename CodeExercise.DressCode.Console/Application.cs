using System;
using System.Collections.Generic;
using System.Linq;
using CodeExercise.DressCode.Engine;
using CodeExercise.DressCode.Engine.Violations;
using StructureMap;

namespace CodeExercise.DressCode.Console
{
    public class Application
    {
        private readonly IWriter _writer;
        private readonly IReader _reader;
        private readonly IInputOption _input;
        private const string InvalidCommand = "Invalid Commands!.Please try again.";
        private const string InvalidTemperature = "Invalid Temperature!.Please try again.";
        public Application(IWriter writer, IReader reader, IInputOption input)
        {
            _writer = writer;
            _reader = reader;
            _input = input;
        }
        public void Run(IContainer container)
        {
            try
            {
                var temperature = GetTemperature();

                if (string.IsNullOrEmpty(temperature)) return;

                var dressPicker = container
                    .With("temperature").EqualTo(temperature)
                    .GetInstance<IDressPicker>();

                var commands = GetCommands();
                if (commands.Count == 0) return;

                var result = new List<string>();
                foreach (var command in commands)
                {
                    try
                    {
                        result.Add(dressPicker.Process(command));
                    }
                    catch (Violation)
                    {
                        result.Add("fail");
                        break;
                    }
                }
                _writer.WriteLine(string.Join(",", result.ToArray()));
            }
            catch (NotValidTemperatureException)
            {
                _writer.WriteLine(InvalidTemperature);
            }
            catch (NotValidCommandException)
            {
                _writer.WriteLine(InvalidCommand);
            }
           
        }
        private string GetTemperature()
        {
           var temperatures = _input.GetTemperatureOptions();
            _writer.Write("Enter Temperature (" + string.Join("/", temperatures.ToArray()) + "):");
            var input =  _reader.ReadLine();
            if (temperatures.Contains(input, StringComparer.CurrentCultureIgnoreCase))
                return input;
            _writer.WriteLine(InvalidTemperature);
            return null;
        }
        private IList<int> GetCommands()
        {
            var result = new List<int>();

            var commands = _input.GetCommands();
            _writer.WriteLine("Valid Commands");
            _writer.WriteLine("--------------------------------------------------");
            _writer.WriteLine(commands.Aggregate(string.Empty, (current, command) => current + (command.Key + " - " + command.Value + "\n")));
            _writer.Write("Enter (Comma sperated list of commands):");

            var input = _reader.ReadLine()?.Trim(',');

            if (string.IsNullOrEmpty(input))
            {
                _writer.WriteLine(InvalidCommand);
                return result;
            }
            int x;
            if (!int.TryParse(input.Replace(",", ""), out x))
            {
                _writer.WriteLine(InvalidCommand);
                return result;
            }

            result.AddRange(input.Split(',').Select(cmd => Convert.ToInt32(cmd)));

            return result;
        }
    }
}
