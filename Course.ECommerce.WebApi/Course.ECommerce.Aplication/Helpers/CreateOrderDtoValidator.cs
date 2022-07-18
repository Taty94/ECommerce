using Course.ECommerce.Aplication.Dtos;
using FluentValidation;

namespace Course.ECommerce.Aplication.Helpers
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(o => o.UserEmail).NotNull().NotEmpty().WithMessage("{PropertyName} no debe estar vacio")
                .Matches(@".+\@.+\..+").WithMessage("{PropertyName} no tiene el formato correo -> correoexample@example.com");

            RuleFor(d => d.BasketId).NotNull().NotEmpty()
                                 .WithMessage("{PropertyName} no debe estar vacio")
                                 .Length(4)
                                 .WithMessage("La longitud de {PropertyName} debe tener 4 caracteres. Ingresaste {TotalLength} caracteres.")
                                 .Matches("^[a-zA-Z0-9]*$")
                                 .WithMessage("{PropertyName} no tiene el formato correcto. Debe contener numeros,letras");

            RuleFor(d => d.DeliveryId).NotNull().NotEmpty()
                                 .WithMessage("{PropertyName} no debe estar vacio")
                                 .Length(4)
                                 .WithMessage("La longitud de {PropertyName} debe tener 4 caracteres. Ingresaste {TotalLength} caracteres.")
                                 .Matches("^[a-zA-Z0-9]*$")
                                 .WithMessage("{PropertyName} no tiene el formato correcto. Debe contener numeros,letras");
        }

    }
}