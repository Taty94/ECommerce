using Course.ECommerce.Aplication.Dtos;
using FluentValidation;

namespace Course.ECommerce.Aplication.Helpers
{
    public class CreateProductTypeDtoValidator : AbstractValidator<CreateProductTypeDto>
    {
        public CreateProductTypeDtoValidator()
        {
            RuleFor(pt => pt.Id).NotNull().NotEmpty()
                                 .WithMessage("{PropertyName} no debe estar vacio")
                                 .Length(4)
                                 .WithMessage("La longitud de {PropertyName} debe tener 4 caracteres. Ingresaste {TotalLength} caracteres.")
                                 .Matches("^[a-zA-Z0-9]*$")
                                 .WithMessage("{PropertyName} no tiene el formato correcto. Debe contener numeros,letras, . , _ ó - ");
           
            RuleFor(p => p.Description).NotNull().NotEmpty()
                                    .WithMessage("{PropertyName} no debe estar vacio")
                                    .Length(0,256)
                                    .WithMessage("{PropertyName} debe tener entre {MinLength} y {MaxLength} caracteres. Ingresaste {TotalLength} caracteres");
        }
    }
}