﻿using NUnit.Framework;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine.Tests
{
    [TestFixture]
    public class ShirtTest
    {
        [Test]
        [TestCase(Temperature.Hot)]
        [TestCase(Temperature.Cold)]
        public void Pajama_Must_Taken_Off_Before_Shirts_Put_On(Temperature temperature)
        {
            var dressPicker = new DressPicker(temperature.ToString());

            Assert.Throws<PajamaMustTakeOffViolation>(() => dressPicker.Process((int)Command.PutOnShirt));

        }
        [Test]
        [TestCase(Temperature.Hot)]
        [TestCase(Temperature.Cold)]
        public void Only_One_Peice_Of_Shirt_Can_Put_On(Temperature temperature)
        {
            var dressPicker = new DressPicker(temperature.ToString());
            dressPicker.Process((int)Command.TakeOffPajama);
            dressPicker.Process((int)Command.PutOnShirt);
            Assert.Throws<OnlyOnePieceOfEachClothingAllowedViolation>(() => dressPicker.Process((int)Command.PutOnShirt));
        }

        [Test]
        public void When_Hot_TShirt_Can_Be_Put_On()
        {
            var dressPicker = new DressPicker(Temperature.Hot.ToString());

            dressPicker.Process((int)Command.TakeOffPajama);

            Assert.AreEqual(Outfit.Tshirt.ToDescription(), dressPicker.Process((int)Command.PutOnShirt));
        }
        
        [Test]
        public void When_Cold_Shirt_Can_Be_Put_On()
        {
            var dressPicker = new DressPicker(Temperature.Cold.ToString());

            dressPicker.Process((int)Command.TakeOffPajama);

            Assert.AreEqual(Outfit.Shirt.ToDescription(),dressPicker.Process((int)Command.PutOnShirt));
        }
        [Test]
        public void When_Cold_Shirt_Is_Manditory_To_Leave_House()
        {
            var dressPicker = new DressPicker(Temperature.Cold.ToString());

            dressPicker.Process((int)Command.TakeOffPajama);
            dressPicker.Process((int)Command.PutOnPants);
            dressPicker.Process((int)Command.PutOnSocks);
            dressPicker.Process((int)Command.PutOnFootWear);

            Assert.Throws<NotValidToLeaveHouseViolation>(() => dressPicker.Process((int)Command.LeaveHouse));

            dressPicker.Process((int)Command.PutOnShirt);
            dressPicker.Process((int)Command.PutOnJacket);
            dressPicker.Process((int)Command.PutOnHeadWear);

            Assert.AreEqual("leaving house", dressPicker.Process((int)Command.LeaveHouse));

        }
        [Test]
        public void When_Hot_Shirt_Is_Manditory_To_Leave_House()
        {
            var dressPicker = new DressPicker(Temperature.Hot.ToString());

            dressPicker.Process((int)Command.TakeOffPajama);
            dressPicker.Process((int)Command.PutOnPants);
            dressPicker.Process((int)Command.PutOnFootWear);

            Assert.Throws<NotValidToLeaveHouseViolation>(() => dressPicker.Process((int)Command.LeaveHouse));
            dressPicker.Process((int)Command.PutOnShirt);
            dressPicker.Process((int)Command.PutOnHeadWear);

            Assert.AreEqual("leaving house", dressPicker.Process((int)Command.LeaveHouse));
        }
    }
}
