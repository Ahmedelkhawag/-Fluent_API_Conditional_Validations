using Fluent_API_Conditional_Validations.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fluent_API_Conditional_Validations.DTOs
{
    public class OrderDTO
    {
        public string PaymentMode { get; set; }

        public string? CreditCardNumber { get; set; }

        public string? UPID { get; set; }
       
        public int CustomerId { get; set; }

        public decimal OrderAmount { get; set; }

        public decimal Discount { get; set; }
    }
}
