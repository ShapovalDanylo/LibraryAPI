using Library.Data;
using Library.Interfaces;

namespace Library.Repositories
{
    public class LibraryRepository : ILibrary
    {
        private readonly DataContext _context;

        public LibraryRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Models.Library> GetLibraries()
        {
            return _context.Libraries.OrderBy(l => l.Id).ToList();
        }

        public Models.Library GetLibraryById(int id)
        {
            return _context.Libraries.FirstOrDefault(l => l.Id == id);
        }

        public Models.Library AddLibrary(Models.Library library)
        {
            var existingLibrary = _context.Libraries.FirstOrDefault(l => l.Id == library.Id);

            if (existingLibrary != null)
            {
                existingLibrary.Name = library.Name;
                _context.SaveChanges();

                return existingLibrary;
            }

            _context.Libraries.Add(library);
            _context.SaveChanges();

            return library;
        }

        public Models.Library DeleteLibrary(int id)
        {
            var libraryToDelete = _context.Libraries.FirstOrDefault(l => l.Id == id);

            if (libraryToDelete != null)
            {
                _context.Libraries.Remove(libraryToDelete);
                _context.SaveChanges();
            }

            return libraryToDelete;
        }
    }
}
