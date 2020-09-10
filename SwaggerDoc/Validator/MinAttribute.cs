using System;
using System.ComponentModel.DataAnnotations;

namespace SwaggerDoc.Controllers
{
    public class MinAttribute : ValidationAttribute
    {
        private int _min;

        /// <summary>
        /// Validate if an int is greater or equal of a minimal value
        /// </summary>
        /// <param name="min">The minimal value</param>
        public MinAttribute(int min)
        {
            _min = min;
        }

        public override bool IsValid(object value)
        {
            return (int)value > _min;
        }
    }
}