using System.Linq;
using NUnit.Framework;
using CodeExercise.DressCode.Engine.ValueObjects;

namespace CodeExercise.DressCode.Engine.Tests
{
    [TestFixture]
    public class InputOptionTest
    {
        [Test]
        public void Returns_Temperature_Options()
        {
            InputOption options = new InputOption();
            var temperature = options.GetTemperatureOptions();

            Assert.IsTrue(temperature.Contains(Temperature.Hot.ToString()));
            Assert.IsTrue(temperature.Contains(Temperature.Cold.ToString()));
            Assert.IsTrue(temperature.Length == 2);
        }
        [Test]
        public void Returns_Command_Options()
        {
            InputOption options = new InputOption();
            var command = options.GetCommands();

            Assert.IsTrue(command.Keys.Count == 8);
            Assert.IsTrue(command.Any(t=>t.Key ==1 && t.Value == Command.PutOnHeadWear.ToString()));
            Assert.IsTrue(command.Any(t => t.Key == 2 && t.Value == Command.PutOnFootWear.ToString()));
            Assert.IsTrue(command.Any(t => t.Key == 3 && t.Value == Command.PutOnSocks.ToString()));
            Assert.IsTrue(command.Any(t => t.Key == 4 && t.Value == Command.PutOnShirt.ToString()));
            Assert.IsTrue(command.Any(t => t.Key == 5 && t.Value == Command.PutOnJacket.ToString()));
            Assert.IsTrue(command.Any(t => t.Key == 6 && t.Value == Command.PutOnPants.ToString()));
            Assert.IsTrue(command.Any(t => t.Key == 7 && t.Value == Command.LeaveHouse.ToString()));
            Assert.IsTrue(command.Any(t => t.Key == 8 && t.Value == Command.TakeOffPajama.ToString()));
        }
       
    }
}
