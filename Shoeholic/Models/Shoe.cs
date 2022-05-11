using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Shoeholic.Models
{
    public class Shoe
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        public int RetailPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ColorWay { get; set; }
        public int CollectionId { get; set; }
        public Brand Brand { get; set; }
        public Collection Collection { get; set; }
        public List<Tag> Tags { get; set; }
        public List<int> ChoosenTags { get; set; }
        
    }
}
