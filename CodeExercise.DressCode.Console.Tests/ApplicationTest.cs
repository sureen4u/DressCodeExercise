using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using CodeExercise.DressCode.Engine;
using CodeExercise.DressCode.Engine.Violations;
using StructureMap;

namespace CodeExercise.DressCode.Console.Tests
{
    [TestFixture]
    public class ApplicationTest
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
        [TestCase("1,2")]
        public void Return_Fail_For_Voilated_Rules_Test(string command)
        {
            var reader = new Mock<IReader>();
            reader.SetupSequence(t => t.ReadLine())
                .Returns("hot")
                .Returns(command);

            var dressPicker = new Mock<IDressPicker>();
            dressPicker.Setup(t => t.Process(It.IsAny<int>())).Throws(new OnlyOnePieceOfEachClothingAllowedViolation());
            _container.Inject(dressPicker.Object);

            var app = new Application(_writer.Object, reader.Object, _inputOption.Object);
            app.Run(_container);

            Assert.IsTrue(_writerText.ToLower().Contains("fail"));
        }
        [Test]
        [TestCase("1,2")]
        public void Gets_Data_From_Engine_Test(string command)
        {
            var reader = new Mock<IReader>();
            reader.SetupSequence(t => t.ReadLine())
                .Returns("hot")
                .Returns(command);
            
            var dressPicker = new Mock<IDressPicker>();
            dressPicker.SetupSequence(t => t.Process(It.IsAny<int>()))
                .Returns("somecommand")
                .Returns("othercommand");
            _container.Inject(dressPicker.Object);

            var app = new Application(_writer.Object, reader.Object, _inputOption.Object);
            app.Run(_container);

            Assert.AreEqual(_writerText.ToLower(), "somecommand,othercommand");
        }
    }
}
