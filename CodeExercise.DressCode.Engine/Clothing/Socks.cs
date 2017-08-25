using CodeExercise.DressCode.Engine.Repository;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine.Clothing
{
    internal class Socks: IClothing
    {
        private readonly Temperature _temperature;
        private readonly IDressHolder _dressHolder;
        public Socks(Temperature temperature, IDressHolder dressHolder)
        {
            _temperature = temperature;
            _dressHolder = dressHolder;
        }
        public string WearAppropriateOutfit()
        {
            if (_dressHolder.HasOutfit(OutfitType.Socks))
                throw new OnlyOnePieceOfEachClothingAllowedViolation();

            if (_temperature == Temperature.Hot)
            {
                throw new CannotPutOnSocksWhenHotViolation();
            }

            _dressHolder.AddOutfit(OutfitType.Socks);

            return Outfit.Socks.ToDescription();
        }
    }
}
