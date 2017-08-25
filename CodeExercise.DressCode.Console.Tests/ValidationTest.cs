using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using CodeExercise.DressCode.Engine;
using CodeExercise.DressCode.Engine.Violations;
using StructureMap;

namespace CodeExercise.DressCode.Console.Tests
{
    [TestFixture]
    public class ValidationTest
    {
        private IContainer _container;
        private string _writerText = "";
        private readonly Mock<IWriter> _writer = new Mock<IWriter>();
        private readonly Mock<IInputOption> _inputOption = new Mock<IInputOption>();
        private Dictionary<int, string> _commands;

        [SetUp]
        public void SetUp()
        {
            _container = new Container(x =>
            {
                x.For<IDressPicker>().Use<DressPicker>().Ctor<string>("temperature");
            });

            _writer.Setup(t => t.WriteLine(It.IsAny<string>())).Callback<string>((m) => _writerText = m);
            _writer.Setup(t => t.Write(It.IsAny<string>()));

            _commands = new Dictionary<int, string> { { 1, "somecommand" }, { 2, "othercommand" } };

            _inputOption.Setup(t => t.GetTemperatureOptions())
                .Returns(new[] { "Hot", "Cold" });
            _inputOption.Setup(t => t.GetCommands())
                .Returns(_commands);
        }

        [Test]
        [TestCase("snow")]
        [TestCase("breezy")]
        [TestCase(null)]
        [TestCase("")]
        public void Temperature_Input_Validation_Test(string temperature)
        {
            var reader = new Mock<IReader>();
          
            reader.SetupSequence(t => t.ReadLine())
                .Returns(temperature)
                .Returns("8,6,4,2,1,7");

            var dressPicker = new Mock<IDressPicker>();
            
            dressPicker.Setup(t => t.Process(It.IsAny<int>())).Returns("some string");
            _container.Inject(dressPicker.Object);

            var app = new Application(_writer.Object,reader.Object, _inputOption.Object);
            app.Run(_container);

            Assert.AreEqual(_writerText, "Invalid Temperature!.Please try again.");
        }
        [Test]
        [TestCase("hot")]
        [TestCase("cold")]
        public void Takes_valid_Temperature_Test(string temperature)
        {
            var reader = new Mock<IReader>();
            reader.SetupSequence(t => t.ReadLine())
                .Returns(temperature)
                .Returns("86,4,2,1,7");

            var dressPicker = new Mock<IDressPicker>();

            dressPicker.Setup(t => t.Process(It.IsAny<int>())).Returns("some string");
            _container.Inject(dressPicker.Object);

            var app = new Application(_writer.Object, reader.Object, _inputOption.Object);
            app.Run(_container);

            Assert.AreNotEqual(_writerText, "Invalid Temperature!.Please try again.");
        }
        [Test]
        [TestCase("1,abc,2")]
        [TestCase("1,2,#")]
        [TestCase("")]
        [TestCase(null)]
        public void Command_Validation_Test(string command)
        {
            var reader = new Mock<IReader>();
            reader.SetupSequence(t => t.ReadLine())
                .Returns("hot")
                .Returns(command);

            var dressPicker = new Mock<IDressPicker>();
            dressPicker.Setup(t => t.Process(It.IsAny<int>())).Returns("oufit name");
            _container.Inject(dressPicker.Object);

            var app = new Application(_writer.Object, reader.Object, _inputOption.Object);
            app.Run(_container);

            Assert.AreEqual(_writerText, "Invalid Commands!.Please try again.");
        }
        [Test]
        [TestCase("1,2")]
        public void Valid_Command_Test(string command)
        {
            var reader = new Mock<IReader>();
            reader.SetupSequence(t => t.ReadLine())
                .Returns("hot")
                .Returns(command);

            var dressPicker = new Mock<IDressPicker>();
            dressPicker.Setup(t => t.Process(It.IsAny<int>())).Returns("oufit name");
            _container.Inject(dressPicker.Object);

            var app = new Application(_writer.Object, reader.Object, _inputOption.Object);
            app.Run(_container);

            Assert.AreNotEqual(_writerText, "Invalid Commands!.Please try again.");
        }
        [Test]
        [TestCase("1,2,3")]
        public void Handles_Invalid_Command_exception_From_Dress_Picker(string command)
        {
            var reader = new Mock<IReader>();
            reader.SetupSequence(t => t.ReadLine())
                .Returns("hot")
                .Returns(command);
            
            var dressPicker = new Mock<IDressPicker>();
            dressPicker.Setup(t => t.Process(It.IsAny<int>())).Throws(new NotValidCommandException());
            _container.Inject(dressPicker.Object);

            var app = new Application(_writer.Object, reader.Object, _inputOption.Object);
            app.Run(_container);

            Assert.AreEqual(_writerText, "Invalid Commands!.Please try again.");
        }
    }
}
