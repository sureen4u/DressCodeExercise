using CodeExercise.DressCode.Engine.Repository;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine.Clothing
{
    internal class Pants : Clothing
    {
        private readonly Temperature _temperature;
        private readonly IDressHolder _dressHolder;
        public Pants(Temperature temperature, IDressHolder dressHolder)
             : base(dressHolder, OutfitType.Pants)
        {
            _temperature = temperature;
            _dressHolder = dressHolder;
        }
        public override string Wear()
        {
            _dressHolder.AddOutfit(OutfitType.Pants);

            return _temperature == Temperature.Hot ? Outfit.Shorts.ToDescription() : Outfit.Pants.ToDescription();
        }
       
    }
}
