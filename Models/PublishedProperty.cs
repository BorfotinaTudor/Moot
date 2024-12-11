namespace Moot.Models
{
    public class PublishedProperty
    {
        public int ID { get; set; }
        public int AgentID { get; set; }
        public int PropertyID { get; set; }
        public Agent Agent { get; set; }
        public Property Property { get; set; }
    }
}
