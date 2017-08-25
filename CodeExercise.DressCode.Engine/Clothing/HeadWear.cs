using CodeExercise.DressCode.Engine.Repository;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine.Clothing
{
    internal class HeadWear : IClothing
    {
        private readonly Temperature _temperature;
        private readonly IDressHolder _dressHolder;
        public HeadWear(Temperature temperature, IDressHolder dressHolder)
        {
            _temperature = temperature;
            _dressHolder = dressHolder;
        }
        public string WearAppropriateOutfit()
        {
            if (_dressHolder.HasOutfit(OutfitType.HeadWear))
                throw new OnlyOnePieceOfEachClothingAllowedViolation();

            if (!_dressHolder.HasOutfit(OutfitType.Shirt))
            {
                throw new ShirtMustBePutOnBeforeHeadwearViolation();
            }

            _dressHolder.AddOutfit(OutfitType.HeadWear);

            return _temperature == Temperature.Hot ? Outfit.SunVisor.ToDescription() : Outfit.Hat.ToDescription();
        }
    }
}
