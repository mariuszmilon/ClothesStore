namespace ClothesStore.Models
{
    public class TrousersDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Material { get; set; }
        public string Colour { get; set; }
        public bool Unused { get; set; }
        public string Sex { get; set; }
        public string Type { get; set; }
        public string Fastener { get; set; }
        public string Waisted { get; set; }
        public string CountOfPocket { get; set; }
        public string TrouserLeg { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
    }
}
