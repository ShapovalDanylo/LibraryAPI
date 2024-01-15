namespace Library.ResponseEntities
{
    public class BookResponseShema
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int LibraryId { get; set; }
    }
}
