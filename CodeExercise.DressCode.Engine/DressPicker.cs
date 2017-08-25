using System;
using System.Linq;
using CodeExercise.DressCode.Engine.Clothing;
using CodeExercise.DressCode.Engine.Repository;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine
{
    public class DressPicker:IDressPicker
    {
        private readonly IDressHolder _dressHolder = new DressHolder();
        private readonly Temperature _temperature;
        public int NumberOfDressesWeared => _dressHolder.GetOutfit().Count;
        public bool IsPajamaOn => _dressHolder.HasOutfit(OutfitType.Pajama);

        public DressPicker(string temperature)
        {
            var parsed = Enum.TryParse(temperature, true, out _temperature);

            if (!parsed || !Enum.IsDefined(typeof(Temperature), _temperature))
                throw new NotValidTemperatureException();
        }

        public string Process(int commandValue)
        {
            var command = (Command)commandValue;

            if (!Enum.IsDefined(typeof(Command), command))
                throw new NotValidCommandException();

            if (command == Command.TakeOffPajama)
                return RemovePajama();

            if (command == Command.LeaveHouse)
                return LeaveHouse();

            return Wear(command);
        }
        private string Wear(Command command)
        {
            if (_dressHolder.HasOutfit(OutfitType.Pajama))
                throw new PajamaMustTakeOffViolation();

            var outfit = new ClothingFactory(_temperature, _dressHolder).GetClothingInstance(command)
                                .WearAppropriateOutfit();

            return outfit;
        }
        private string LeaveHouse()
        {
             if(typeof(OutfitType).MandiToryOutFitTypesToLeaveHome(_temperature)
                .Any(t=> !_dressHolder.GetOutfit().Contains(t)))
                throw new NotValidToLeaveHouseViolation();
           
            return "leaving house";
        }
        private string RemovePajama()
        {
            if(!_dressHolder.HasOutfit(OutfitType.Pajama))
                throw new PajamaNotFoundViolation();

            _dressHolder.RemoveOutfit(OutfitType.Pajama);

            return "Removing PJs";
        }
      
    }
}
