using CodeExercise.DressCode.Engine.Repository;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine.Clothing
{
    internal class Shirt : IClothing
    {
        private readonly Temperature _temperature;
        private readonly IDressHolder _dressHolder;
        public Shirt(Temperature temperature, IDressHolder dressHolder)
        {
            _temperature = temperature;
            _dressHolder = dressHolder;
        }
        public string WearAppropriateOutfit()
        {
            if (_dressHolder.HasOutfit(OutfitType.Shirt))
                throw new OnlyOnePieceOfEachClothingAllowedViolation();

            _dressHolder.AddOutfit(OutfitType.Shirt);

            return _temperature == Temperature.Hot ? Outfit.Tshirt.ToDescription() : Outfit.Shirt.ToDescription();
        }
    }
}
