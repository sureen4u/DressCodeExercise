using NUnit.Framework;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine.Tests
{
    [TestFixture]
    public class DressPickerTest
    {
        [Test]
        [TestCase(Temperature.Hot)]
        [TestCase(Temperature.Cold)]
        public void Initial_State_is_with_Pajama_On(Temperature temperature)
        {
            var dressPicker = new DressPicker(temperature.ToString());

            Assert.IsTrue(dressPicker.IsPajamaOn);
            Assert.IsTrue(dressPicker.NumberOfDressesWeared == 1);
        }
        [Test]
        [TestCase(Temperature.Hot)]
        [TestCase(Temperature.Cold)]
        public void Pajama_Not_Found(Temperature temperature)
        {
            var dressPicker = new DressPicker(temperature.ToString());

            dressPicker.Process((int)Command.TakeOffPajama);

            Assert.Throws<PajamaNotFoundViolation>(() => dressPicker.Process((int)Command.TakeOffPajama));
        }
        [Test]
        [TestCase(Temperature.Hot)]
        [TestCase(Temperature.Cold)]
        public void Can_Remove_Pajama(Temperature temperature)
        {
            var dressPicker = new DressPicker(temperature.ToString());

            Assert.AreEqual("Removing PJs", dressPicker.Process((int)Command.TakeOffPajama));
        }
        [Test]
        public void Should_Not_Accept_Invalid_Temperature()
        {
            Assert.Throws<NotValidTemperatureException>(() =>
            {
                var picker = new DressPicker("CLOUDY");
            });
        }
        [Test]
        public void Should_Not_Accept_Invalid_Commands()
        {
            var dressPicker = new DressPicker(Temperature.Hot.ToString());

            Assert.Throws<NotValidCommandException>(() => dressPicker.Process(11));
        }
        [Test]
        [TestCase(Temperature.Hot)]
        [TestCase(Temperature.Cold)]
        public void Cannot_Leave_House_With_Pajama(Temperature temperature)
        {
            var dressPicker = new DressPicker(temperature.ToString());
            Assert.Throws<NotValidToLeaveHouseViolation>(() => dressPicker.Process((int)Command.LeaveHouse));

        }
       

    }
}
