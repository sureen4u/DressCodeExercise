using CodeExercise.DressCode.Engine.Repository;
using CodeExercise.DressCode.Engine.ValueObjects;

namespace CodeExercise.DressCode.Engine.Clothing
{
    internal class ClothingFactory
    {
        private readonly Temperature _temperature;
        private readonly IDressHolder _dressHolder;
        public ClothingFactory(Temperature temperature, IDressHolder dressHolder)
        {
            _temperature = temperature;
            _dressHolder = dressHolder;
        }
        public IClothing GetClothingInstance(Command command)
        {
            switch (command)
            {
                    case Command.PutOnFootWear:
                        return new FootWear(_temperature, _dressHolder);
                    case Command.PutOnSocks:
                        return new Socks(_temperature, _dressHolder);
                    case Command.PutOnHeadWear:
                        return new HeadWear(_temperature, _dressHolder);
                    case Command.PutOnShirt:
                        return new Shirt(_temperature, _dressHolder);
                    case Command.PutOnJacket:
                        return new Jacket(_temperature, _dressHolder);
                    case Command.PutOnPants:
                        return new Pants(_temperature, _dressHolder);
            }
            return null;
        }
       
    }
}
