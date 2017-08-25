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
        public void When_Cold_Has_To_Leave_House_After_Wearing_All_Outfit_Types()
        {
            var dressPicker = new DressPicker(Temperature.Cold.ToString());
            dressPicker.Process((int)Command.TakeOffPajama);
            dressPicker.Process((int)Command.PutOnShirt);
            dressPicker.Process((int)Command.PutOnPants);
            dressPicker.Process((int)Command.PutOnSocks);

            Assert.Throws<NotValidToLeaveHouseViolation>(() => dressPicker.Process((int)Command.LeaveHouse));

            dressPicker.Process((int)Command.PutOnFootWear);
            dressPicker.Process((int)Command.PutOnJacket);
            dressPicker.Process((int)Command.PutOnHeadWear);
             
            Assert.AreEqual("leaving house", dressPicker.Process((int)Command.LeaveHouse));
        }
        [Test]
        public void When_Hot_Can_Leave_House_After_Wearing_All_Outfit_Types_Except_Socks_Jacket()
        {
            var dressPicker = new DressPicker(Temperature.Hot.ToString());
            dressPicker.Process((int)Command.TakeOffPajama);
            dressPicker.Process((int)Command.PutOnShirt);
            dressPicker.Process((int)Command.PutOnPants);

            Assert.Throws<NotValidToLeaveHouseViolation>(() => dressPicker.Process((int)Command.LeaveHouse));

            dressPicker.Process((int)Command.PutOnFootWear);
            dressPicker.Process((int)Command.PutOnHeadWear);

            Assert.AreEqual("leaving house", dressPicker.Process((int)Command.LeaveHouse));
        }

    }
}
