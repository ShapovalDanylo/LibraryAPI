namespace Library.ResponseEntities
{
    public class LibraryResponseShema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookResponseShema> Books { get; set; }
    }
}
