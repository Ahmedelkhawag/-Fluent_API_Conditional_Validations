namespace Fluent_API_Conditional_Validations.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string MembershipTier { get; set; }

        public virtual ICollection<Order>  Orders { get; set; }
    }
}
