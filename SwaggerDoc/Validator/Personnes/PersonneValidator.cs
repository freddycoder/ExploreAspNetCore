using FluentValidation;
using SwaggerDoc.Model.Personnes;

namespace SwaggerDoc.Validator.Personnes
{
    /// <summary>
    /// Permet de valider le modèle <see cref="Personne" />
    /// </summary>
    public class PersonneValidator : AbstractValidator<Personne>
    {
        /// <summary>
        /// Constructeur par défault avec les règles de validation du modèle.
        /// </summary>
        public PersonneValidator()
        {
            RuleFor(p => p.Prenom).NotEmpty();
            RuleFor(p => p.Nom).NotEmpty();
            RuleFor(p => p.Genre).IsInEnum().WithMessage("Le genre doit être 'M' ou 'F'.");
            RuleFor(p => p.DateNaissance).NotEmpty();
        }
    }
}
