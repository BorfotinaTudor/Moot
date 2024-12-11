namespace Moot.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string ClientAdress { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Offer>? Offers { get; set; }
    }
}
