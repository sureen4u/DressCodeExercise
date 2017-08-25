using CodeExercise.DressCode.Engine;
using StructureMap;

namespace CodeExercise.DressCode.Console
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var container = Container.For<ConsoleRegistry>();
            var app = container.GetInstance<Application>();

            while (true)
            {
                app.Run(container);
            }
        }

        public class ConsoleRegistry : Registry
        {
            public ConsoleRegistry()
            {
                Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
                For<IDressPicker>().Use<DressPicker>().Ctor<string>("temperature");
                For<IInputOption>().Use<InputOption>();
            }
        }
    }
}
