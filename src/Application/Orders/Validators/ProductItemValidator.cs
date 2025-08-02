using Application.DTOs;
using FluentValidation;

namespace Application.Orders.Validators
{
    public class ProductItemValidator : AbstractValidator<ProductItem>
    {
        public ProductItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.ProductAmount).GreaterThan(0);
            RuleFor(x => x.ProductPrice).GreaterThan(0);
        }
    }
}