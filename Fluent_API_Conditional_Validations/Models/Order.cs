using System.ComponentModel.DataAnnotations.Schema;

namespace Fluent_API_Conditional_Validations.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string PaymentMode { get; set; }

        public string? CreditCardNumber { get; set; }

        public string? UPID { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public virtual Customer  Customer { get; set; }

        public decimal OrderAmount { get; set; }

        public decimal Discount { get; set; }
    }
}
