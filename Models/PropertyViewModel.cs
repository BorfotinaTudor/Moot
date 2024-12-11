namespace Moot.Models
{
    public class PropertyViewModel
    {
        public int ID { get; set; }
        public string PropertyType { get; set; }
        public decimal Price { get; set; }
        public string FullName { get; set; }
        public int? NeighborhoodID { get; set; }
        public string NeighborhoodName { get; set; }
    }
}
