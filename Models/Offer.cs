namespace Moot.Models
{
    public class Offer
    {
        public int? OfferID { get; set; }
        public int? ClientID { get; set; }
        public int? PropertyID { get; set; }
        public DateTime? OfferDate { get; set; }
        public Client? Client { get; set; }
        public Property? Properties { get; set; }
    }
}
