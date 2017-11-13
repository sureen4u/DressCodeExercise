using CodeExercise.DressCode.Engine.Repository;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine.Clothing
{
    internal class HeadWear : Clothing
    {
        private readonly Temperature _temperature;
        private readonly IDressHolder _dressHolder;
        public HeadWear(Temperature temperature, IDressHolder dressHolder)
             : base(dressHolder, OutfitType.HeadWear)
        {
            _temperature = temperature;
            _dressHolder = dressHolder;
        }
        public override string Wear()
        {
            if (!_dressHolder.HasOutfit(OutfitType.Shirt))
            {
                throw new ShirtMustBePutOnBeforeHeadwearViolation();
            }

            _dressHolder.AddOutfit(OutfitType.HeadWear);

            return _temperature == Temperature.Hot ? Outfit.SunVisor.ToDescription() : Outfit.Hat.ToDescription();
        }
    }
}
