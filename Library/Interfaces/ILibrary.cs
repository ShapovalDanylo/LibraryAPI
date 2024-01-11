namespace Library.Interfaces
{
    public interface ILibrary
    {
        ICollection<Models.Library> GetLibraries();
        Models.Library GetLibraryById(int id);
        Models.Library AddLibrary(Models.Library library);
        Models.Library DeleteLibrary(int id);
    }
}
