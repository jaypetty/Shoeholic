namespace Shoeholic.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
