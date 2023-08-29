using System.ComponentModel.DataAnnotations;

namespace WhatsAppAPI.Attributes
{
    public class Attribute
    {
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
        public class NoNumbers : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (value is string stringValue)
                {
                    return !stringValue.Any(char.IsDigit);
                }
                return false;
            }
        }
    }
}
