using Microsoft.AspNetCore.Mvc;
using Library.Interfaces;

namespace Library.Controllers
{
    [Route("api/libraries")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibrary _libraryInterface;

        public LibraryController(ILibrary libraryInterface)
        {
            _libraryInterface = libraryInterface;
        }

        /// <summary>
        /// Gets all Libraries
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Models.Library>))]
        public IActionResult GetLibraries()
        {
            var libraries = _libraryInterface.GetLibraries();

            return Ok(libraries);
        }

        /// <summary>
        /// Gets Library by id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Models.Library))]
        public IActionResult GetLibraryById(int id)
        {
            var library = _libraryInterface.GetLibraryById(id);

            if (library == null)
                return NotFound();

            return Ok(library);
        }

        /// <summary>
        /// Creates a Library
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Models.Library))]
        public ActionResult<Models.Library> AddLibrary([FromBody] Models.Library library)
        {
            Models.Library createdLibrary = _libraryInterface.AddLibrary(library);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(createdLibrary);
        }

        /// <summary>
        /// Deletes a Library
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(202, Type = typeof(Models.Library))]
        public ActionResult<Models.Library> DeleteLibrary(int id)
        {
            Models.Library deletedLibrary = _libraryInterface.DeleteLibrary(id);

            if (deletedLibrary == null)
                return NotFound();

            return Ok(deletedLibrary);
        }
    }
}
