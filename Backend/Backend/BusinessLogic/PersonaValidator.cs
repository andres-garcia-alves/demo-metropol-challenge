using Backend.Entities;
using FluentValidation;

namespace Backend.BusinessLogic
{
    public class PersonaValidator : AbstractValidator<Persona>
    {
        public PersonaValidator()
        {
            RuleFor(p => p.Nombre).NotEmpty().WithMessage("El nombre es requerido.");
            RuleFor(p => p.Apellido).NotEmpty().WithMessage("El apellido es requerido.");
            RuleFor(p => p.DNI).NotEmpty().WithMessage("El DNI es requerido.")
                               .Matches(@"^\d+$").WithMessage("El DNI debe ser numérico.");
            RuleFor(p => p.Sexo).NotEmpty().WithMessage("El sexo es requerido.");
            RuleFor(p => p.Mail).NotEmpty().WithMessage("El mail es requerido.")
                                .EmailAddress().WithMessage("El mail debe tener un formato válido.");
        }
    }
}
