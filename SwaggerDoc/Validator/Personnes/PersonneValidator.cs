using FluentValidation;
using SwaggerDoc.Model.Personnes;

namespace SwaggerDoc.Validator.Personnes
{
    public class PersonneValidator : AbstractValidator<Personne>
    {
        public PersonneValidator()
        {
            RuleFor(p => p.Prenom).NotEmpty();
            RuleFor(p => p.Nom).NotEmpty();
            RuleFor(p => p.Genre).IsInEnum().WithMessage("Le genre doit être 'M' ou 'F'.");
            RuleFor(p => p.DateNaissance).NotEmpty();
        }
    }
}
