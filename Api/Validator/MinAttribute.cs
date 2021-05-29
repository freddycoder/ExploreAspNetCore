using System.ComponentModel.DataAnnotations;

namespace ExploreAspNetCore.Controllers
{
    /// <summary>
    /// Attribue pour valider que l'objet de type <see cref="int"/> est plus grand qu'une valeur minimale.
    /// </summary>
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

        /// <inheritdoc />
        public override bool IsValid(object value)
        {
            return (int)value > _min;
        }
    }
}