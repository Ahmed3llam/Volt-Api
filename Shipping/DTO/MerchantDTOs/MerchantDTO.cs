using System.ComponentModel.DataAnnotations;

namespace Shipping.DTO.MerchantDTOs
{
    public class MerchantDTO
    {
        public string? Id { get; set; }
        
        public string Name { get; set; }
       
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? Status { get; set; } = true;
        public string? BranchName { get; set; }       
        public string? Role { get; set; }
        public string? Password { get; set; }
        public string Address { get; set; }
        public string Government { get; set; }
        public string City { get; set; }
        public int PickUpSpecialCost { get; set; }
        public int RefusedOrderPercent { get; set; }
    }
}
