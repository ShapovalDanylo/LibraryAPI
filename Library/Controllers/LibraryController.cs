using Microsoft.AspNetCore.Mvc;
using Library.Interfaces;
using Library.ResponseEntities;

namespace Library.Controllers
{
    [Route("api/libraries")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryRepository _libraryInterface;

        public LibraryController(ILibraryRepository libraryInterface)
        {
            _libraryInterface = libraryInterface;
        }

        /// <summary>
        /// Gets all Libraries
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<LibraryResponseShema>))]
        public IActionResult GetLibraries()
        {
            ICollection<Models.Library> libraries = _libraryInterface.GetLibraries();

            ICollection<LibraryResponseShema> response = new List<LibraryResponseShema>();

            foreach (var library in libraries)
            {
                List<BookResponseShema> bookList = new List<BookResponseShema>();

                foreach (var book in library.Books)
                {
                    bookList.Add(new BookResponseShema
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Year = book.Year,
                        LibraryId = book.LibraryId
                    });
                }

                LibraryResponseShema libraryResponseShema = new LibraryResponseShema()
                {
                    Id = library.Id,
                    Name = library.Name,
                    Books = bookList
                };

                response.Add(libraryResponseShema);
            }

            return Ok(response);
        }

        /// <summary>
        /// Gets Library by id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(LibraryResponseShema))]
        public IActionResult GetLibraryById(int id)
        {
            Models.Library library = _libraryInterface.GetLibraryById(id);

            if (library == null)
                return NotFound();

            List<BookResponseShema> bookResponseShema = new List<BookResponseShema>();

            foreach (var book in library.Books)
            {
                bookResponseShema.Add(new BookResponseShema()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Year = book.Year,
                    LibraryId = book.LibraryId
                });
            }

            LibraryResponseShema response = new LibraryResponseShema()
            {
                Id = library.Id,
                Name = library.Name,
                Books = bookResponseShema
            };

            return Ok(response);
        }

        /// <summary>
        /// Creates a Library
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(LibraryResponseShema))]
        public ActionResult<LibraryResponseShema> AddLibrary([FromBody] string library)
        {
            try
            {
                Models.Library createdLibrary = _libraryInterface.AddLibrary(library);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                LibraryResponseShema response = new LibraryResponseShema()
                {
                    Id = createdLibrary.Id,
                    Name = library,
                    Books = new List<BookResponseShema>()
                };

                return Ok(response);
            } 
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a Library
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(202, Type = typeof(LibraryResponseShema))]
        public ActionResult<LibraryResponseShema> DeleteLibrary(int id)
        {
            Models.Library deletedLibrary = _libraryInterface.DeleteLibrary(id);

            if (deletedLibrary == null)
                return NotFound();

            List<BookResponseShema> deletedLibraryBooks = new List<BookResponseShema>();

            foreach (var book in deletedLibrary.Books)
            {
                deletedLibraryBooks.Add(new BookResponseShema()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Year = book.Year,
                    LibraryId = book.LibraryId
                });
            }

            LibraryResponseShema response = new LibraryResponseShema()
            {
                Id = deletedLibrary.Id,
                Name = deletedLibrary.Name,
                Books = deletedLibraryBooks
            };

            return Ok(response);
        }
    }
}
