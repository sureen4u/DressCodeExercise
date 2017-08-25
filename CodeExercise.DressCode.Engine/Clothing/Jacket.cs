using CodeExercise.DressCode.Engine.Repository;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine.Clothing
{
    internal class Jacket : IClothing
    {
        private readonly Temperature _temperature;
        private readonly IDressHolder _dressHolder;
        public Jacket(Temperature temperature, IDressHolder dressHolder)
        {
            _temperature = temperature;
            _dressHolder = dressHolder;
        }
        public string WearAppropriateOutfit()
        {
            if (_dressHolder.HasOutfit(OutfitType.Jacket))
                throw new OnlyOnePieceOfEachClothingAllowedViolation();

            if (!_dressHolder.HasOutfit(OutfitType.Shirt))
            {
                throw new ShirtMustBePutOnBeforeJacketViolation();
            }

            if (_temperature == Temperature.Hot)
            {
                throw new CannotPutJacketOnWhenHotViolation();
            }

            _dressHolder.AddOutfit(OutfitType.Jacket);

            return Outfit.Jacket.ToDescription();
        }
       
    }
}
