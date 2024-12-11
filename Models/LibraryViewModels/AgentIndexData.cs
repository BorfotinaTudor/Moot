namespace Moot.Models.LibraryViewModels
{
    public class AgentIndexData
    {
        public IEnumerable<Agent> Agents { get; set; }
        public IEnumerable<Property> Properties { get; set; }
        public IEnumerable<Offer> Offers { get; set; }
    }
}
