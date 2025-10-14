using LibraryNetwork.Models.Entety;

namespace LibraryNetwork.Models
{
    public interface IBookRepository
    {
        public IEnumerable<ILibraryObject> GetAll();
        public void Add(ILibraryObject libraryObject);
        public void Delete(TypeOfLibraryObject typeOfLibraryObject, int id);
        public void ClearByType(TypeOfLibraryObject typeOfLibraryObject);
        public bool Validate(string item, TypeOfLibraryObject typeOfLibraryObject);
    }
}