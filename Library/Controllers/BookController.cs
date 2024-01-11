using Library.Interfaces;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBook _bookInterface;

        public BookController(IBook bookInterface)
        {
            _bookInterface = bookInterface;
        }

        /// <summary>
        /// Gets all Books
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Book>))]
        public IActionResult GetBooks()
        {
            var books = _bookInterface.GetBooks();

            return Ok(books);
        }

        /// <summary>
        /// Gets Book by id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        public IActionResult GetBookById(int id)
        {
            var book = _bookInterface.GetBookById(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        /// <summary>
        /// Creates a Book
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Book))]
        public ActionResult<Book> AddBook([FromBody] Book book)
        {
            try
            {
                Book createdBook = _bookInterface.AddBook(book);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(createdBook);
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
        [ProducesResponseType(202, Type = typeof(Book))]
        public ActionResult<Book> DeleteBook(int id)
        {
            Book deletedBook = _bookInterface.DeleteBook(id);

            if (deletedBook == null)
                return NotFound();

            return Ok(deletedBook);
        }
    }
}
