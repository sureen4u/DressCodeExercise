using CodeExercise.DressCode.Engine.Repository;
using CodeExercise.DressCode.Engine.ValueObjects;
using CodeExercise.DressCode.Engine.Violations;

namespace CodeExercise.DressCode.Engine.Clothing
{
    internal abstract class Clothing
    {
        private readonly IDressHolder _dressHolder;
        private readonly OutfitType _type = new OutfitType();
        public Clothing(IDressHolder dressHolder,OutfitType type)
        {
            _type = type;
            _dressHolder = dressHolder;
        }
        public abstract string Wear();

        public string WearAppropriateOutfit()
        {
            if (_dressHolder.HasOutfit(_type))
                throw new OnlyOnePieceOfEachClothingAllowedViolation();

            return Wear();
        }
        
    }
}
