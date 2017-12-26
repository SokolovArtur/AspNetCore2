using System;
using System.ComponentModel.DataAnnotations;

namespace Tochka.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class NotNullAttribute : ValidationAttribute
    {
        public NotNullAttribute()
        {
            ErrorMessage = "The Name field is required.";
        }

        public override string FormatErrorMessage(string name)
        {
            return "The " + name + " field is required.";
        }

        public override bool IsValid(object value)
        {
            return (value == null) ? false : true;
        }
    }
}
