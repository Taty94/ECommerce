using Course.ECommerce.Aplication.Dtos;
using FluentValidation;

namespace Course.ECommerce.Aplication.Helpers
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            //RuleFor(p => p.Id).NotNull()
            //                  .WithMessage("{PropertyName} no debe estar vacio");
            RuleFor(p => p.Price).NotNull().NotEmpty()
                                 .WithMessage("{PropertyName} no debe estar vacio")
                                 .ScalePrecision(2,18)
                                 .WithMessage("{PropertyName} no debe tener más de {ExpectedPrecision} dígitos en total, con margen para {ExpectedScale} decimales. Se encontraron {Digits} dígitos y {ActualScale} decimales");
            RuleSet("ProductInfo", () =>
            {
                RuleFor(p => p.Name).NotNull().NotEmpty()
                                    .WithMessage("{PropertyName} no debe estar vacio")
                                    .Length(0, 100)
                                    .WithMessage("{PropertyName} debe tener entre {MinLength} y {MaxLength} caracteres. Ingresaste {TotalLength} caracteres");
                RuleFor(p => p.Description).NotNull().NotEmpty()
                                    .WithMessage("{PropertyName} no debe estar vacio")
                                    .Length(0,256)
                                    .WithMessage("{PropertyName} debe tener entre {MinLength} y {MaxLength} caracteres. Ingresaste {TotalLength} caracteres");
            });
            RuleSet("ProductRelation", () =>
            {
                //RuleFor(p => p.ProductType).NotNull()
                //                           .WithMessage("{PropertyName} no debe estar vacio");
                //RuleFor(p => p.ProductType).NotNull()
                //                           .WithMessage("{PropertyName} no debe estar vacio");
                RuleFor(p => p.ProductTypeId).NotNull().NotEmpty()
                                           .WithMessage("{PropertyName} no debe estar vacio");
                RuleFor(p => p.ProductBrandId).NotNull().NotEmpty()
                                           .WithMessage("{PropertyName} no debe estar vacio");
            });
            //RuleSet("ProductDates", () =>
            //{
            //    RuleFor(p => p.CreationDate).NotNull()
            //                                .WithMessage("{PropertyName} no debe estar vacio")
            //                                .GreaterThan(DateTime.MinValue)
            //                                .WithMessage("{PropertyName} debe ser mayor a {ComparisonValue}");
            //    RuleFor(p => p.ModifiedDate).GreaterThan(DateTime.MinValue)
            //                                .WithMessage("{PropertyName} debe ser mayor a {ComparisonValue}");
            //});
        }
    }
}