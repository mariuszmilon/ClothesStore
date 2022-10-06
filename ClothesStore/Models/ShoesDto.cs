﻿namespace ClothesStore.Models
{
    public class ShoesDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Size { get; set; }
        public string Material { get; set; }
        public string Colour { get; set; }
        public bool Unused { get; set; }
        public string Sex { get; set; }
        public string Type { get; set; }
        public string Heel { get; set; }
        public string Toecap { get; set; }
        public string Fastener { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
    }
}
