using CodeExercise.DressCode.Engine.Repository;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine.Clothing
{
    internal class Socks: Clothing
    {
        private readonly Temperature _temperature;
        private readonly IDressHolder _dressHolder;
        public Socks(Temperature temperature, IDressHolder dressHolder)
             : base(dressHolder, OutfitType.Socks)
        {
            _temperature = temperature;
            _dressHolder = dressHolder;
        }
        public override string Wear()
        {
            if (_temperature == Temperature.Hot)
            {
                throw new CannotPutOnSocksWhenHotViolation();
            }

            _dressHolder.AddOutfit(OutfitType.Socks);

            return Outfit.Socks.ToDescription();
        }
    }
}
