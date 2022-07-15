using Course.ECommerce.Aplication.Dtos;
using FluentValidation;

namespace Course.ECommerce.Aplication.Helpers
{
    public class CreateProductBrandDtoValidator : AbstractValidator<CreateProductBrandDto>
    {
        public CreateProductBrandDtoValidator()
        {
            RuleFor(pb => pb.Id).NotNull().NotEmpty()
                                 .WithMessage("{PropertyName} no debe estar vacio")
                                 .Length(4)
                                 .WithMessage("La longitud de {PropertyName} debe tener 4 caracteres. Ingresaste {TotalLength} caracteres.")
                                 .Matches("^[a-zA-Z0-9_.-]*$").WithErrorCode("400")
                                 .WithMessage("{PropertyName} no tiene el formato correcto. Debe contener numeros,letras, . , _ ó - ");
           
            RuleFor(p => p.Description).NotNull().NotEmpty()
                                    .WithMessage("{PropertyName} no debe estar vacio")
                                    .Length(0,256)
                                    .WithMessage("{PropertyName} debe tener entre {MinLength} y {MaxLength} caracteres. Ingresaste {TotalLength} caracteres");
        }
    }
}