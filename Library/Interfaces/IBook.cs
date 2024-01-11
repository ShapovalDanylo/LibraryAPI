using Library.Models;

namespace Library.Interfaces
{
    public interface IBook
    {
        ICollection<Book> GetBooks();
        Book GetBookById(int id);
        Book AddBook(Book book);
        Book DeleteBook(int id);
    }
}
