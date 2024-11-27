using Fluent_API_Conditional_Validations.DTOs;
using Fluent_API_Conditional_Validations.Models;
using Fluent_API_Conditional_Validations.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fluent_API_Conditional_Validations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly OrderDTOValidator validator;

        public OrderController(MyContext context)
        {
            _context = context;
            validator = new OrderDTOValidator(_context);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order is null)
            {
                return BadRequest(ModelState);
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderDTO orderDTO)
        {
            var valditionresuls = await validator.ValidateAsync(orderDTO);

            if (!valditionresuls.IsValid)
            { 
            var Errors = valditionresuls.Errors.Select(e => new { field = e.PropertyName , Error = e.ErrorMessage});
                return BadRequest(Errors);
            }
            var order = new Order
            {
                CustomerId = orderDTO.CustomerId,
                PaymentMode = orderDTO.PaymentMode,
                CreditCardNumber = orderDTO.CreditCardNumber,
                UPID = orderDTO.UPID,
                OrderAmount = orderDTO.OrderAmount,
                Discount = orderDTO.Discount
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }
    }
}
