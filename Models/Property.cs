namespace Moot.Models
{
    public class Property
    {
        public int ID { get; set; }
        public string PropertyType { get; set; }
        public decimal Price { get; set; }
        public int? OwnerID { get; set; }
        public Owner? Owner { get; set; }
        public int? NeighborhoodID { get; set; }
        public Neighborhood? Neighborhood { get; set; }
        public ICollection<Offer>? Offers { get; set; }

        public ICollection<PublishedProperty> PublishedProperties { get; set; }
    }
}
