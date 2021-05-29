using FluentValidation;

namespace ExploreAspNetCore.Controllers
{
    /// <summary>
    /// Classe de validation pour le modèle <see cref="DateArgs" />
    /// </summary>
    public class DateArgsValidation : AbstractValidator<DateArgs>
    {
        /// <summary>
        /// Constructeur par défaut avec les règles de validation du modèle.
        /// </summary>
        public DateArgsValidation()
        {
            RuleFor(d => d.Begin).NotEmpty();

            RuleFor(d => d.End).NotEmpty()
                               .GreaterThanOrEqualTo(d => d.Begin);
        }
    }
}
