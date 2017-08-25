
using System.Collections.Generic;

namespace CodeExercise.DressCode.Engine.Contract.ValueObjects
{
    public interface IDressPicker
    {
        public string Process(Command command);
        bool HasOutfitType(OutfitType outfitType);
        IList<OutfitType> GetOutfit();
    }

}
