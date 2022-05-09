using System.Collections.Generic;

namespace Shoeholic.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public List<Shoe> Shoes { get; set; }
    }
}
