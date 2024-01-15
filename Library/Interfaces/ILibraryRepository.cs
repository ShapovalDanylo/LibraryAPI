namespace Library.Interfaces
{
    public interface ILibraryRepository
    {
        ICollection<Models.Library> GetLibraries();
        Models.Library GetLibraryById(int id);
        Models.Library AddLibrary(string library);
        Models.Library DeleteLibrary(int id);
    }
}
