namespace Domain
{
    public class PaginatedListZaduzivanja
    {
        public IEnumerable<Zaduzivanje> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious => PageIndex > 1;
        public bool HasNext => PageIndex < TotalPages;
    }
}
