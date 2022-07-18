using Course.ECommerce.Aplication.Dtos;
using FluentValidation;

namespace Course.ECommerce.Aplication.Helpers
{
    public class CreateDeliveryDtoValidator : AbstractValidator<CreateDeliveryDto>
    {
        public CreateDeliveryDtoValidator()
        {
            RuleFor(d => d.Id).NotNull().NotEmpty()
                                 .WithMessage("{PropertyName} no debe estar vacio")
                                 .Length(4)
                                 .WithMessage("La longitud de {PropertyName} debe tener 4 caracteres. Ingresaste {TotalLength} caracteres.")
                                 .Matches("^[a-zA-Z0-9]*$")
                                 .WithMessage("{PropertyName} no tiene el formato correcto. Debe contener numeros,letras");
            
            RuleFor(d => d.Name).NotNull().NotEmpty()
                                    .WithMessage("{PropertyName} no debe estar vacio")
                                    .Length(0, 256)
                                    .WithMessage("{PropertyName} debe tener entre {MinLength} y {MaxLength} caracteres. Ingresaste {TotalLength} caracteres");

            RuleFor(d => d.Time).NotNull().NotEmpty()
                                    .WithMessage("{PropertyName} no debe estar vacio")
                                    .Length(0, 50)
                                    .WithMessage("{PropertyName} debe tener entre {MinLength} y {MaxLength} caracteres. Ingresaste {TotalLength} caracteres")
                                    .Matches("([0-9]-[0-9])+ DAYS").WithMessage("Tiempo no coincide con el formato, deberia ser '[rango de dias] DAYS -> 2-3 DAYS'");

            RuleFor(d => d.Description).NotNull().NotEmpty()
                                    .WithMessage("{PropertyName} no debe estar vacio")
                                    .Length(0,256)
                                    .WithMessage("{PropertyName} debe tener entre {MinLength} y {MaxLength} caracteres. Ingresaste {TotalLength} caracteres");

            RuleFor(d => d.Price).NotNull().NotEmpty()
                                    .WithMessage("{PropertyName} no debe estar vacio")
                                    .ScalePrecision(2,10)
                                    .WithMessage("{PropertyName} no debe tener más de {ExpectedPrecision} dígitos en total, con margen para {ExpectedScale} decimales.Se encontraron {Digits} dígitos y {ActualScale} decimales.");
        }

    }
}