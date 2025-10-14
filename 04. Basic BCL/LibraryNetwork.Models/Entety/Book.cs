using System.Collections;

namespace LibraryNetwork.Models.Entety
{
    public class Book: ILibraryObject
    {
        public uint Id { get; set; }

        private string title;
        public string Title
        { 
            get => title;
            set
            { 
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Book title cannot be empty.", nameof(value));
                }
                title = value; 
            }
        }

        private List<string> authors;
        public List<string> Authors
        {
            get => authors;
            private set
            {
                if (value == null || value.Count == 0|| value.Any(a => string.IsNullOrWhiteSpace(a))) 
                {
                    throw new ArgumentException("Book authors cannot be empty.", nameof(value));
                }
                authors = value;
            }
        }
        
        private string placeOfPublication;
        public string PlaceOfPublication 
        { 
            get => placeOfPublication;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Book place of publication cannot be empty.", nameof(value));
                }  
                placeOfPublication = value;
            }
        }
        
        private string publisher;
        public string Publisher 
        { 
            get => publisher; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Book publisher cannot be empty.", nameof(value));
                }
                publisher = value;
            }
        }

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
        
        public uint? NumberOfPages { get; set; }
        
        public string? Note { get; set; } 
        
        public string? ISBN { get; set; }
        
        public Book(uint id,
                    string title,
                    List<string> authors,
                    string placeOfPublication,
                    string publisher,
                    uint? year,
                    uint? numberOfPages,
                    string? note,
                    string? isbn)
        {
            Id = id;
            Title = title;
            Authors = authors ?? [];
            PlaceOfPublication = placeOfPublication;
            Publisher = publisher;
            YearOfPublication = year;
            NumberOfPages = numberOfPages;
            Note = note;
            ISBN = isbn;
        }
        public override string ToString()
        {
            return $"{Id}|{Title}|{string.Join(";", Authors)}|{PlaceOfPublication}|{Publisher}|{YearOfPublication}|{NumberOfPages}|{Note}|{ISBN}";
        }
    }
}
