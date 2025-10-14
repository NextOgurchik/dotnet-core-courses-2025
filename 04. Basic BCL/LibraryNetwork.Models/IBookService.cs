using LibraryNetwork.Models.Entety;

namespace LibraryNetwork.Models
{
    public interface IBookService
    {
        public IEnumerable<ILibraryObject> GetAll(OrderBy? orderBy = null);
        public IEnumerable<ILibraryObject> GetAll(TypeOfLibraryObject typeOfLibraryObject, OrderBy? orderBy=null);
        public IEnumerable<ILibraryObject> GetAllByTitle(string title, OrderBy? orderBy = null);
        public IEnumerable<Book> GetBooksByAuthor(string author, OrderBy? orderBy = null);
        public IEnumerable<Book> GetBooksByPublisher(string publisher, OrderBy? orderBy = null);
        public void Add(ILibraryObject libraryObject);
        public void Delete(TypeOfLibraryObject typeOfLibraryObject, int id);
        public void ClearByType(TypeOfLibraryObject typeOfLibraryObject);
    }
}