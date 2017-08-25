using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace CodeExercise.DressCode.Engine.ValueObjects
{
    public enum OutfitType
    {
        [ManditoryToLeaveHome()]
        HeadWear,
        [ManditoryToLeaveHome()]
        FootWear,
        [ManditoryToLeaveHome(Temperature.Cold)]
        Socks,
        [ManditoryToLeaveHome()]
        Shirt,
        [ManditoryToLeaveHome(Temperature.Cold)]
        Jacket,
        [ManditoryToLeaveHome()]
        Pants,
        Pajama
    }
    internal class ManditoryToLeaveHomeAttribute : Attribute
    {
        private readonly Temperature? _temperature;
        public ManditoryToLeaveHomeAttribute()
        {
        }
        public ManditoryToLeaveHomeAttribute(Temperature temperature)
        {
            _temperature = temperature;
        }
        public override string ToString()
        {
            return _temperature.ToString();
        }
    }
    public static class OutFitTypeExtensions
    {
        public static List<OutfitType> MandiToryOutFitTypesToLeaveHome(this Type outfitType,Temperature temperature)
        {
            return (from field in outfitType.GetFields(BindingFlags.Static
                                                                | BindingFlags.GetField
                                                                | BindingFlags.Public)

                    let outfit = (OutfitType)Enum.Parse(typeof(OutfitType), field.Name)

                    from Attribute attrib in field.GetCustomAttributes(true)
                    where attrib.GetType() == typeof(ManditoryToLeaveHomeAttribute)
                    where attrib.ToString() == string.Empty || attrib.ToString() == temperature.ToString()
                    select outfit).ToList();
        }
    }
}
