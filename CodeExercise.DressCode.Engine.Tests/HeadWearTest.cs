using NUnit.Framework;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine.Tests
{
    [TestFixture]
    public class HeadWearTest
    {
       
        [Test]
        [TestCase(Temperature.Hot)]
        [TestCase(Temperature.Cold)]
        public void Pajama_Must_Taken_Off_Before_Headwear_Put_On(Temperature temperature)
        {
            var dressPicker = new DressPicker(temperature.ToString());

            Assert.Throws<PajamaMustTakeOffViolation>(() => dressPicker.Process((int)Command.PutOnHeadWear));

        }
        [Test]
        public void Shirt_Must_Be_Put_On_before_Headwear()
        {
            var dressPicker = new DressPicker(Temperature.Cold.ToString());

            dressPicker.Process((int)Command.TakeOffPajama);

            Assert.Throws<ShirtMustBePutOnBeforeHeadwearViolation>(() => dressPicker.Process((int)Command.PutOnHeadWear));
        }
        [Test]
        public void Only_One_Peice_Of_HeadWear_Can_Put_On()
        {
            var dressPicker = new DressPicker(Temperature.Cold.ToString());
            dressPicker.Process((int)Command.TakeOffPajama);
            dressPicker.Process((int)Command.PutOnShirt);
            dressPicker.Process((int)Command.PutOnHeadWear);
            Assert.Throws<OnlyOnePieceOfEachClothingAllowedViolation>(() => dressPicker.Process((int)Command.PutOnHeadWear));
        }

        [Test]
        public void When_Hot_Sun_Visor_Can_Be_Put_On()
        {
            var dressPicker = new DressPicker(Temperature.Hot.ToString());
            dressPicker.Process((int)Command.TakeOffPajama);
            dressPicker.Process((int)Command.PutOnShirt);

            Assert.AreEqual(Outfit.SunVisor.ToDescription(), dressPicker.Process((int)Command.PutOnHeadWear));
        }
        [Test]
        public void When_Cold_Hat_Can_Be_Put_On()
        {
            var dressPicker = new DressPicker(Temperature.Cold.ToString());

            dressPicker.Process((int)Command.TakeOffPajama);
            dressPicker.Process((int)Command.PutOnShirt);

            Assert.AreEqual(Outfit.Hat.ToDescription(),dressPicker.Process((int)Command.PutOnHeadWear));
        }
        [Test]
        public void When_Cold_HeadWear_Is_Manditory_To_Leave_House()
        {
            var dressPicker = new DressPicker(Temperature.Cold.ToString());

            dressPicker.Process((int)Command.TakeOffPajama);
            dressPicker.Process((int)Command.PutOnPants);
            dressPicker.Process((int)Command.PutOnSocks);
            dressPicker.Process((int)Command.PutOnFootWear);
            dressPicker.Process((int)Command.PutOnShirt);
            dressPicker.Process((int)Command.PutOnJacket);

            Assert.Throws<NotValidToLeaveHouseViolation>(() => dressPicker.Process((int)Command.LeaveHouse));

            dressPicker.Process((int)Command.PutOnHeadWear);

            Assert.AreEqual("leaving house", dressPicker.Process((int)Command.LeaveHouse));
        }
        [Test]
        public void When_Hot_Headwear_Is_Manditory_To_Leave_House()
        {
            var dressPicker = new DressPicker(Temperature.Hot.ToString());

            dressPicker.Process((int)Command.TakeOffPajama);
            dressPicker.Process((int)Command.PutOnPants);
            dressPicker.Process((int)Command.PutOnFootWear);
            dressPicker.Process((int)Command.PutOnShirt);

            Assert.Throws<NotValidToLeaveHouseViolation>(() => dressPicker.Process((int)Command.LeaveHouse));
            dressPicker.Process((int)Command.PutOnHeadWear);

            Assert.AreEqual("leaving house", dressPicker.Process((int)Command.LeaveHouse));
        }
    }
}
