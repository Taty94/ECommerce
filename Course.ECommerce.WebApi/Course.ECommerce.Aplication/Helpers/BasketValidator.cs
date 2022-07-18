using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Domain.Entities.BasketClasses;
using FluentValidation;

namespace Course.ECommerce.Aplication.Helpers
{
    public class BasketValidator : AbstractValidator<Basket>
    {
        public BasketValidator()
        {
            RuleFor(b => b.Id).NotNull().NotEmpty()
                                 .WithMessage("{PropertyName} no debe estar vacio")
                                 .Length(4)
                                 .WithMessage("La longitud de {PropertyName} debe tener 4 caracteres. Ingresaste {TotalLength} caracteres.")
                                 .Matches("^[a-zA-Z0-9]*$")
                                 .WithMessage("{PropertyName} no tiene el formato correcto. Debe contener numeros,letras");

            RuleFor(b => b.Items).Must(list => list.Count() > 0).WithMessage("El carrito debe contener al menos un item");
        }

    }
}