namespace LibraryNetwork
{
    internal class Book : ILibraryItem
    {
        private string name;
        public string Name 
        { 
            get => name; 
            set => name = value; 
        }
        private string note;
        public string Note 
        { 
            get => note; 
            set => note = value; 
        }
        public Book(string name, string note)
        {
            Name = name;
            Note = note;
        }
    }
}
