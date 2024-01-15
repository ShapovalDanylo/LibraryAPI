using Library.Interfaces;
using Library.RequestEntities;
using Library.ResponseEntities;
using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace Library.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookInterface;

        public BookController(IBookRepository bookInterface)
        {
            _bookInterface = bookInterface;
        }

        /// <summary>
        /// Gets all Books
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<BookResponseShema>))]
        public IActionResult GetBooks()
        {
            ICollection<Book> books = _bookInterface.GetBooks();

            ICollection<BookResponseShema> response = new List<BookResponseShema>();

            foreach (var book in books)
            {
                response.Add(new BookResponseShema
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Year = book.Year,
                    LibraryId = book.LibraryId,
                });
            }

            return Ok(response);
        }

        /// <summary>
        /// Gets Book by id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BookResponseShema))]
        public IActionResult GetBookById(int id)
        {
            Book book = _bookInterface.GetBookById(id);

            if (book == null)
                return NotFound();

            BookResponseShema response = new BookResponseShema()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                LibraryId = book.LibraryId,
            };

            return Ok(response);
        }

        /// <summary>
        /// Creates a Book
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BookResponseShema))]
        public ActionResult<BookResponseShema> AddBook([FromBody] BookRequestShema book)
        {
            try
            {
                Book createdBook = _bookInterface.AddBook(book);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                BookResponseShema response = new BookResponseShema()
                {
                    Id = createdBook.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Year = book.Year,
                    LibraryId = book.LibraryId
                };

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a Book
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(202, Type = typeof(BookResponseShema))]
        public ActionResult<BookResponseShema> DeleteBook(int id)
        {
            Book deletedBook = _bookInterface.DeleteBook(id);

            if (deletedBook == null)
                return NotFound();

            BookResponseShema response = new BookResponseShema()
            {
                Id = deletedBook.Id,
                Title = deletedBook.Title,
                Author = deletedBook.Author,
                Year = deletedBook.Year,
                LibraryId = deletedBook.LibraryId
            };

            return Ok(response);
        }
    }
}
