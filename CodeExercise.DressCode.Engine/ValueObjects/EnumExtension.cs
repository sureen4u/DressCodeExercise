using System.ComponentModel;

namespace CodeExercise.DressCode.Engine.ValueObjects
{
    public static class EnumExension
    {
        public static string ToDescription(this System.Enum value)
        {
            var attribute = (DescriptionAttribute[])(value.
                                                     GetType()
                                                     .GetField(value.ToString()))
                                                     .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attribute.Length > 0 ? attribute[0].Description : value.ToString();
        }
    }
}
