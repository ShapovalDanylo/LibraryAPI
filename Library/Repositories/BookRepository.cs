using Library.Data;
using Library.Interfaces;
using Library.Models;
using Library.RequestEntities;
using Library.ResponseEntities;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Book> GetBooks()
        {
            return _context.Books.OrderBy(b => b.Id).ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        public Book AddBook(BookRequestShema book)
        {
            var library = _context.Libraries
                .Include(l => l.Books)
                .FirstOrDefault(l => l.Id == book.LibraryId);

            if (library == null)
            {
                throw new InvalidOperationException("Library doesn't exist!");
            }

            if (library.Books.Any(b => b.Title == book.Title && b.Author == book.Author && b.Year == book.Year))
            {
                throw new InvalidOperationException("The book is already in the library!");
            }

            Book createdBook = new Book()
            {
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                LibraryId = library.Id,
                Library = library
            };

            _context.Books.Add(createdBook);
            _context.SaveChanges();

            return createdBook;
        }

        public Book DeleteBook(int id)
        {
            Book bookToDelete = _context.Books.FirstOrDefault(b => b.Id == id);

            if (bookToDelete == null)
                return null;

            _context.Books.Remove(bookToDelete);
            _context.SaveChanges();

            return bookToDelete;
        }
    }
}
