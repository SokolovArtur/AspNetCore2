using System;
using System.ComponentModel.DataAnnotations;

namespace Tochka.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class NotNullAttribute : ValidationAttribute
    {
        public NotNullAttribute() { }

        public override bool IsValid(object value)
        {
            return (value == null) ? false : true;
        }
    }
}
