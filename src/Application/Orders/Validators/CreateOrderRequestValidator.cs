using Application.DTOs;
using FluentValidation;

namespace Application.Orders.Validators;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.InvoiceAddress)
            .NotEmpty()
            .WithMessage("Invoice address is required.");

        RuleFor(x => x.InvoiceEmailAddress)
            .NotEmpty().WithMessage("Invoice Email address is required.")
            .EmailAddress().WithMessage("Invoice Email address format in invalid");

        RuleFor(x => x.InvoiceCreditCardNumber)
            .NotEmpty().WithMessage("Invoice Credit Card Number is required.")
            .CreditCard().WithMessage("Invoice Credit Card Number format is invalid");

        RuleForEach(x => x.Products).SetValidator(new ProductItemValidator());
    }
}