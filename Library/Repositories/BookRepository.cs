using Library.Data;
using Library.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories
{
    public class BookRepository : IBook
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Book> GetBooks()
        {
            return _context.Books
                .Include(b => b.Library)
                .OrderBy(b => b.Id).ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books
                    .Include(b => b.Library)
                    .FirstOrDefault(b => b.Id == id);
        }

        public Book AddBook(Book book)
        {
            var existingBook = _context.Books
                                .Include(b => b.Library)
                                .FirstOrDefault(b => b.Id == book.Id);

            var existingLibrary = _context.Libraries.FirstOrDefault(l => l.Id == book.Library.Id && l.Name == book.Library.Name);

            if (existingLibrary == null)
            {
                throw new InvalidOperationException("Library doesn't exist!");
            } 
            else
            {
                book.Library = existingLibrary;
            }

            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Year = book.Year;
                existingBook.Library = existingLibrary;
            } 
            else
            {
                _context.Books.Add(book);
            }
            _context.SaveChanges();

            return book;
        }

        public Book DeleteBook(int id)
        {
            var bookToDelete = _context.Books
                                .Include(b => b.Library)
                                .FirstOrDefault(b => b.Id == id);

            if (bookToDelete != null)
            {
                _context.Books.Remove(bookToDelete);
                _context.SaveChanges();
            }

            return bookToDelete;
        }
    }
}
