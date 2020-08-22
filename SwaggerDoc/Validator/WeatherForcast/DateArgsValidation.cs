using FluentValidation;

namespace SwaggerDoc.Controllers
{
    public class DateArgsValidation : AbstractValidator<DateArgs>
    {
        public DateArgsValidation()
        {
            RuleFor(d => d.Begin).NotEmpty();

            RuleFor(d => d.End).NotEmpty()
                               .GreaterThanOrEqualTo(d => d.Begin);
        }
    }
}
