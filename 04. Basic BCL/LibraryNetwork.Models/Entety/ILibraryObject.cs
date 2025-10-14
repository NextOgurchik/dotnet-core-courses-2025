namespace LibraryNetwork.Models.Entety
{
    public interface ILibraryObject
    {
        public uint Id { get; set; }
        public string Title { get; set; }
        public uint? YearOfPublication { get; set; }
    }
}
