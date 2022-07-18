using Course.ECommerce.Aplication.Dtos;
using FluentValidation;

namespace Course.ECommerce.Aplication.Helpers
{
    public class CreateLocationInfoDtoValidator : AbstractValidator<CreateLocationInfoDto>
    {
        public CreateLocationInfoDtoValidator()
        {
            RuleFor(li => li.Name).NotNull().NotEmpty()
                                 .WithMessage("{PropertyName} no debe estar vacio")
                                 .Length(0, 50)
                                 .WithMessage("{PropertyName} debe tener entre {MinLength} y {MaxLength} caracteres. Ingresaste {TotalLength} caracteres");

            RuleFor(li => li.LastName).NotNull().NotEmpty()
                                 .WithMessage("{PropertyName} no debe estar vacio")
                                 .Length(0, 50)
                                 .WithMessage("{PropertyName} debe tener entre {MinLength} y {MaxLength} caracteres. Ingresaste {TotalLength} caracteres");

            RuleFor(li => li.Email).NotNull().NotEmpty().WithMessage("{PropertyName} no debe estar vacio")
                .Matches(@".+\@.+\..+").WithMessage("{PropertyName} no tiene el formato correo -> correoexample@example.com");
            RuleFor(li => li.MainStreet).NotNull().NotEmpty().WithMessage("{PropertyName} no debe estar vacio");
            RuleFor(li => li.SecondaryStreet).NotNull().NotEmpty().WithMessage("{PropertyName} no debe estar vacio");
            RuleFor(li => li.City).NotNull().NotEmpty().WithMessage("{PropertyName} no debe estar vacio");
            RuleFor(li => li.Phone).NotNull().NotEmpty()
                .Length(10)
                .WithMessage("La longitud de {PropertyName} debe tener 10 caracteres. Ingresaste {TotalLength} caracteres.")
                .Matches(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}")
                .WithMessage("{PropertyName} no coincide con el Formato [(xxx)-xxxxxxx,(xxx)-+xxxxxxx, (0x)-xxx-xxx,0x-xxx-xxx");
        }

    }
}