namespace ClothesStore.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Title { get; set; }
        public string Material { get; set; }
        public string Colour { get; set; }
        public string Size { get; set; }
        public bool Unused { get; set; }
        public string Sex { get; set; }
        public decimal Price { get; set; }
        public int? CreatedById { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

    }
}
