namespace Shipping.DTO.CityDTO
{
    public class CityDTO
    {
        public int? id { get; set; }
        public string name { get; set; }
        public int shippingPrice { get; set; }
        public int pickUpPrice { get; set; }
        public bool? status { get; set; } = true;
        public int? governmentId { get; set; }
        public string? governmentName { get; set; }
    }
}
