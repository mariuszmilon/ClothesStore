namespace ClothesStore.Models
{
    public class PagesResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemFrom { get; set; }
        public int ItemTo { get; set; }
        public int TotalItemCount { get; set; }

        public PagesResult(List<T> items, int itemCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItemCount = itemCount;
            ItemFrom = pageSize * (pageNumber - 1) + 1;
            ItemTo = (ItemFrom + pageSize) - 1;
            TotalPages = (int)Math.Ceiling(itemCount / (double)pageSize);
        }
    }
}
