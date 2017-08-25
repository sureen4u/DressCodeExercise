using CodeExercise.DressCode.Engine.Repository;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine.Clothing
{
    internal class Pants : IClothing
    {
        private readonly Temperature _temperature;
        private readonly IDressHolder _dressHolder;
        public Pants(Temperature temperature, IDressHolder dressHolder)
        {
            _temperature = temperature;
            _dressHolder = dressHolder;
        }
        public string WearAppropriateOutfit()
        {
            if (_dressHolder.HasOutfit(OutfitType.Pants))
                throw new OnlyOnePieceOfEachClothingAllowedViolation();

            _dressHolder.AddOutfit(OutfitType.Pants);

            return _temperature == Temperature.Hot ? Outfit.Shorts.ToDescription() : Outfit.Pants.ToDescription();
        }
       
    }
}
