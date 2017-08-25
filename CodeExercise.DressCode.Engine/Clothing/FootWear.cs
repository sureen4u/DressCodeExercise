using CodeExercise.DressCode.Engine.Repository;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine.Clothing
{
    internal class FootWear : IClothing
    {
        private readonly Temperature _temperature;
        private readonly IDressHolder _dressHolder;
        public FootWear(Temperature temperature, IDressHolder dressHolder)
        {
            _temperature = temperature;
            _dressHolder = dressHolder;
        }
        public string WearAppropriateOutfit()
        {
            if (_dressHolder.HasOutfit(OutfitType.FootWear))
                throw new OnlyOnePieceOfEachClothingAllowedViolation();

            if (!_dressHolder.HasOutfit(OutfitType.Pants))
                throw new PantsMustBePutOnBeforeShoesViolation();

            if (_temperature == Temperature.Cold 
                && !_dressHolder.HasOutfit(OutfitType.Socks))
                throw new SocksMustBePutOnBeforeShoesViolation();

            _dressHolder.AddOutfit(OutfitType.FootWear);

            return _temperature == Temperature.Hot? Outfit.Sandals.ToDescription():Outfit.Boots.ToDescription();
        }
       
    }
}
