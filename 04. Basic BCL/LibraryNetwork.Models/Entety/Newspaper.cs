namespace LibraryNetwork.Models.Entety
{
    public class Newspaper : ILibraryObject
    {
        public uint Id { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string? PlaceOfPublication { get; set; }

        private uint? yearOfPublication;
        public uint? YearOfPublication
        {
            get => yearOfPublication;
            set
            {
                if (value < 1900)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"The year of publication of the book must be after 1900. Received:{value}.");
                }
                yearOfPublication = value;
            }
        }
        
        public uint? Pages { get; set; }
        public string? Note {  get; set; }
        public uint? Number { get; set; }
        public DateTime? Date { get; set; }
        public string? ISSN { get; set; }

        public Newspaper(uint id,
                         string title,
                         string publisher,
                         string? placeOfPublication,
                         uint? yearOfPublication,
                         uint? pages,
                         string? note,
                         uint? number,
                         DateTime? date,
                         string? issn)
        {
            Id = id;
            Title = title;
            Publisher = publisher;
            PlaceOfPublication = placeOfPublication;
            YearOfPublication = yearOfPublication;
            Pages = pages;
            Note = note;
            Number = number;
            Date = date;
            ISSN = issn;
        }
        public override string ToString()
        {
            var formatedDate = (Date.HasValue) ? Date.Value.ToString("yyyy-MM-dd") : null;
            return $"{Id}|{Title}|{Publisher}|{PlaceOfPublication}|{YearOfPublication}|{Pages}|{Note}|{Number}|{formatedDate}|{ISSN}";
        }
    }
}
