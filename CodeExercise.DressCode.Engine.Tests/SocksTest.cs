using System;
using NUnit.Framework;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine.Tests
{
    [TestFixture]
    public class SocksTest
    {
        [Test]
        [TestCase(Temperature.Hot)]
        [TestCase(Temperature.Cold)]
        public void Pajama_Must_Taken_Off_Before_Socks_Put_On(Temperature temperature)
        {
            var dressPicker = new DressPicker(temperature.ToString());

            Assert.Throws<PajamaMustTakeOffViolation>(() => dressPicker.Process((int)Command.PutOnSocks));

        }
        [Test]
        public void Only_One_Peice_Of_Socks_Can_Put_On()
        {
            var dressPicker = new DressPicker(Temperature.Cold.ToString());
            dressPicker.Process(((int)Command.TakeOffPajama));
            dressPicker.Process((int)Command.PutOnSocks);
            Assert.Throws<OnlyOnePieceOfEachClothingAllowedViolation>(() => dressPicker.Process((int)Command.PutOnSocks));
        }
        [Test]
        public void When_Hot_Cannot_Put_On_Socks()
        {
            var dressPicker = new DressPicker(Temperature.Hot.ToString());

            dressPicker.Process((int)Command.TakeOffPajama);

            Assert.Throws<CannotPutOnSocksWhenHotViolation>(() => dressPicker.Process((int)Command.PutOnSocks));
        }
        
        [Test]
        public void When_Cold_Can_Put_Socks()
        {
            var dressPicker = new DressPicker(Temperature.Cold.ToString());

            dressPicker.Process((int)Command.TakeOffPajama);

            Assert.AreEqual(Outfit.Socks.ToDescription(),dressPicker.Process((int)Command.PutOnSocks));
           
        }
        
    }
}
