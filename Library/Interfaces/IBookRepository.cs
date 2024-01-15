using Library.RequestEntities;
using Library.Models;

namespace Library.Interfaces
{
    public interface IBookRepository
    {
        ICollection<Book> GetBooks();
        Book GetBookById(int id);
        Book AddBook(BookRequestShema book);
        Book DeleteBook(int id);
    }
}
