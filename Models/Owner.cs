namespace Moot.Models
{
    public class Owner
    {
        
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public ICollection<Property> Properties { get; set; }
    }
}
