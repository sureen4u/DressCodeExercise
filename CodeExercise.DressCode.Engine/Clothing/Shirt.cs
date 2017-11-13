using CodeExercise.DressCode.Engine.Repository;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine.Clothing
{
    internal class Shirt : Clothing
    {
        private readonly Temperature _temperature;
        private readonly IDressHolder _dressHolder;
        public Shirt(Temperature temperature, IDressHolder dressHolder)
             : base(dressHolder, OutfitType.Shirt)
        {
            _temperature = temperature;
            _dressHolder = dressHolder;
        }
        public override string Wear()
        {
            _dressHolder.AddOutfit(OutfitType.Shirt);

            return _temperature == Temperature.Hot ? Outfit.Tshirt.ToDescription() : Outfit.Shirt.ToDescription();
        }
    }
}
