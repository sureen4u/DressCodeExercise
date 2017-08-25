using System.Collections.Generic;
using CodeExercise.DressCode.Engine.ValueObjects;

namespace CodeExercise.DressCode.Engine.Repository
{
    internal interface IDressHolder
    {
        bool HasOutfit(OutfitType outfitType);
        List<OutfitType> GetOutfit();
        void AddOutfit(OutfitType outfitType);
        void RemoveOutfit(OutfitType outfitType);
    }
}
