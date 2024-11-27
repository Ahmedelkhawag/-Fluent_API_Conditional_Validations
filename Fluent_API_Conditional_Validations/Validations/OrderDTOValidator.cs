using Fluent_API_Conditional_Validations.DTOs;
using Fluent_API_Conditional_Validations.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Fluent_API_Conditional_Validations.Validations
{
    public class OrderDTOValidator:AbstractValidator<OrderDTO>
    {
        private readonly MyContext context;

        public OrderDTOValidator(MyContext context)
        {
            this.context = context;
            RuleFor(order => order.CustomerId)
                .NotEmpty()
                .WithMessage("CustomerId is required.")
                .MustAsync(async (cusId, CancellationToken) =>
                {
                    return await context.Customers.AnyAsync(c => c.Id == cusId, CancellationToken);
                }
                ).WithMessage("Invalid Customer");


            RuleFor(order => order.OrderAmount)
                .GreaterThan(0)
                .WithMessage("OrderAmount must be greater than zero");


            RuleFor(Order => Order.UPID)
                .NotEmpty()
                .When(order => order.PaymentMode == "UPI")
                .WithMessage("UPIId is required for UPI payments.");


            When(order => order.PaymentMode == "CreditCard", () =>
            {
                RuleFor(order => order.CreditCardNumber)
                    .NotEmpty()
                    .WithMessage("CreditCardNumber is required for Credit Card payments.");
            });


            Unless(order => order.PaymentMode == "Cash", () =>
            {
                RuleFor(order => order.Discount)
                    .InclusiveBetween(10, 30)
                    .WithMessage("Discount must be between 10% and 30% for non-cash payments.");
            });

           
        }
    }
}
