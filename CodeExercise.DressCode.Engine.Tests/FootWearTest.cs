using NUnit.Framework;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine.Tests
{
    [TestFixture]
    public class FootWearTest
    {
        [Test]
        [TestCase(Temperature.Hot)]
        [TestCase(Temperature.Cold)]
        public void Pajama_Must_Taken_Off_Before_FootWear_Put_On(Temperature temperature)
        {
            var dressPicker = new DressPicker(temperature.ToString());

            Assert.Throws<PajamaMustTakeOffViolation>(() => dressPicker.Process((int)Command.PutOnFootWear));
          
        }
        [Test]
        public void Only_One_Peice_Of_FootWare_Can_Put_On()
        {
            var dressPicker = new DressPicker(Temperature.Hot.ToString());
            dressPicker.Process((int)Command.TakeOffPajama);
            dressPicker.Process((int)Command.PutOnPants);
            dressPicker.Process((int)Command.PutOnFootWear);
            Assert.Throws<OnlyOnePieceOfEachClothingAllowedViolation>(() => dressPicker.Process((int)Command.PutOnFootWear));
        }
        [Test]
        public void When_Hot_FootWear_Should_Be_Sandals()
        {
            var dressPicker = new DressPicker(Temperature.Hot.ToString());

            dressPicker.Process((int)Command.TakeOffPajama);
            dressPicker.Process((int)Command.PutOnPants);

            Assert.AreEqual(Outfit.Sandals.ToDescription(), dressPicker.Process((int)Command.PutOnFootWear));
        }
        
        [Test]
        public void Socks_Must_Be_Put_On_Before_Shoes()
        {
            var dressPicker = new DressPicker(Temperature.Cold.ToString());

            dressPicker.Process((int)Command.TakeOffPajama);
            dressPicker.Process((int)Command.PutOnPants);

            Assert.Throws<SocksMustBePutOnBeforeShoesViolation>(() => dressPicker.Process((int)Command.PutOnFootWear));
           
        }
        [Test]
        public void When_Cold_FootWear_Should_Be_Boots()
        {
            var dressPicker = new DressPicker(Temperature.Cold.ToString());

            dressPicker.Process((int)Command.TakeOffPajama);
            dressPicker.Process((int)Command.PutOnSocks);
            dressPicker.Process((int)Command.PutOnPants);
            var footWear = dressPicker.Process((int)Command.PutOnFootWear);

            Assert.AreEqual(Outfit.Boots.ToDescription(), footWear);
        }
        [Test]
        [TestCase(Temperature.Hot)]
        [TestCase(Temperature.Cold)]
        public void Pants_Must_Be_Put_On_Before_FootWear(Temperature temperature)
        {
            var dressPicker = new DressPicker(temperature.ToString());
            dressPicker.Process((int)Command.TakeOffPajama);
            if(temperature == Temperature.Cold) dressPicker.Process((int)Command.PutOnSocks);

            Assert.Throws<PantsMustBePutOnBeforeShoesViolation>(() => dressPicker.Process((int)Command.PutOnFootWear));
        }
        [Test]
        public void When_Cold_FootWear_Is_Manditory_To_Leave_House()
        {
            var dressPicker = new DressPicker(Temperature.Cold.ToString());

            dressPicker.Process((int)Command.TakeOffPajama);
            dressPicker.Process((int)Command.PutOnPants);
            dressPicker.Process((int)Command.PutOnSocks);
            dressPicker.Process((int)Command.PutOnShirt);
            dressPicker.Process((int)Command.PutOnJacket);
            dressPicker.Process((int)Command.PutOnHeadWear);

            Assert.Throws<NotValidToLeaveHouseViolation>(() => dressPicker.Process((int)Command.LeaveHouse));

            dressPicker.Process((int)Command.PutOnFootWear);

            Assert.AreEqual("leaving house", dressPicker.Process((int)Command.LeaveHouse));
        }
        [Test]
        public void When_Hot_FootWear_Is_Manditory_To_Leave_House()
        {
            var dressPicker = new DressPicker(Temperature.Hot.ToString());

            dressPicker.Process((int)Command.TakeOffPajama);
            dressPicker.Process((int)Command.PutOnPants);
            dressPicker.Process((int)Command.PutOnShirt);
            dressPicker.Process((int)Command.PutOnHeadWear);

            Assert.Throws<NotValidToLeaveHouseViolation>(() => dressPicker.Process((int)Command.LeaveHouse));
            dressPicker.Process((int)Command.PutOnFootWear);

            Assert.AreEqual("leaving house", dressPicker.Process((int)Command.LeaveHouse));
        }
    }
}
