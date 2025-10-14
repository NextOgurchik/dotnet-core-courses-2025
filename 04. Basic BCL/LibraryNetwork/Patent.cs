namespace LibraryNetwork
{
    internal class Patent
    {
        private string name;
        public string Name
        {
            get => name;
            set => name = value;
        }
        public string Inventor { get; set; }
        public string Country { get; set; }
        public int Number { get; set; }
        public DateTime DateOfFiling { get; set; }
        public DateTime DateOfPublication { get; set; }
        public int Pages { get; set; }
        private string note;
        public string Note
        {
            get => note;
            set => note = value;
        }

        public Patent(string name, string note)
        {
            Name = name;
            Note = note;
        }
    }
}
