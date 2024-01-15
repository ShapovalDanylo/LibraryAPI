using Library.Data;
using Library.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly DataContext _context;

        public LibraryRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Models.Library> GetLibraries()
        {
            return _context.Libraries
                    .Include(l => l.Books)
                    .OrderBy(l => l.Id).ToList();
        }

        public Models.Library GetLibraryById(int id)
        {
            return _context.Libraries
                    .Include(l => l.Books)
                    .FirstOrDefault(l => l.Id == id);
        }

        public Models.Library AddLibrary(string library)
        {
            var existingLibrary = _context.Libraries.FirstOrDefault(l => l.Name == library);

            if (existingLibrary != null)
                throw new InvalidOperationException("Library already exists!");

            Models.Library newLibrary = new Models.Library()
            {
                Name = library,
                Books = new List<Book>()
            };

            _context.Libraries.Add(newLibrary);
            _context.SaveChanges();

            return newLibrary;
        }

        public Models.Library DeleteLibrary(int id)
        {
            var libraryToDelete = _context.Libraries
                                    .Include(l => l.Books)
                                    .FirstOrDefault(l => l.Id == id);

            if (libraryToDelete == null)
                return null;

            _context.Libraries.Remove(libraryToDelete);
            _context.SaveChanges();

            return libraryToDelete;
        }
    }
}
