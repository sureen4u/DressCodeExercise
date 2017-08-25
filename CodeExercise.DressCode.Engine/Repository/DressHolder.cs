using System.Collections.Generic;
using CodeExercise.DressCode.Engine.ValueObjects;

namespace CodeExercise.DressCode.Engine.Repository
{
    internal class DressHolder: IDressHolder
    {
        private readonly List<OutfitType> _outfit = new List<OutfitType>();
        public DressHolder()
        {
            _outfit.Add(OutfitType.Pajama);
        }
        public bool HasOutfit(OutfitType outfitType)
        {
            return _outfit.Contains(outfitType);
        }

        public List<OutfitType> GetOutfit()
        {
            return _outfit;
        }
        public void AddOutfit(OutfitType outfitType)
        {
           _outfit.Add(outfitType);

        }
        public void RemoveOutfit(OutfitType outfitType)
        {
            _outfit.Remove(outfitType);

        }
    }
}
