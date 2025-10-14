using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using LibraryNetwork.Models;
using LibraryNetwork.Models.Entety;

namespace LibraryNetwork.Implementation
{
    public class BookRepository : IBookRepository
    {
        public string DbName { get; set; }
        public BookRepository(string dbName="Save")
        {
            DbName = dbName;
        }
        public void Add(ILibraryObject libraryObject)
        {
            string type;
            if (libraryObject is Book) { type = "Book"; }
            else if (libraryObject is Newspaper) { type = "Newspaper"; }
            else { throw new ArgumentException("libraryObject must be Book or Newspaper"); }

            if (!File.Exists($"{DbName}{type}.txt"))
            {
                using (var sw = new StreamWriter($"{DbName}{type}.txt", true)) { }
            }

            using (StreamWriter writer = new StreamWriter($"{DbName}{type}.txt", true, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(libraryObject.ToString());
            }
        }

        public void Delete(TypeOfLibraryObject typeOfLibraryObject, int id)
        {
            List<ILibraryObject> list = [];
            if (typeOfLibraryObject == TypeOfLibraryObject.Book)
            {
                list = GetAll().ToList().FindAll(x => x.Id != id && x is Book || x is Newspaper);
            }
            else
            {
                list = GetAll().ToList().FindAll(x => x.Id != id && x is Newspaper || x is Book);
            }
            
            ClearAll();
            foreach(ILibraryObject libraryObject in list)
            {
                Add(libraryObject);
            }
        }
        private IEnumerable<ILibraryObject> GetAllBooks()
        {
            List<ILibraryObject> list = [];
            if (!File.Exists($"{DbName}Book.txt"))
            {
                using (var sw = new StreamWriter($"{DbName}Book.txt", true)) { }
            }
            var lines = File.ReadAllLines($"{DbName}Book.txt", Encoding.UTF8);
            foreach (var line in lines)
            {
                //if (Validate(line, TypeOfLibraryObject.Book)) { continue; }
                List<string> authors;
                var splitedItem = line.Split('|');
                if (splitedItem[2].Contains(";"))
                {
                    authors = splitedItem[2].Split(";").ToList();
                }
                else
                {
                    authors = [splitedItem[2]];
                }
                var book = new Book(uint.Parse(splitedItem[0]),
                                    splitedItem[1],
                                    authors,
                                    splitedItem[3],
                                    splitedItem[4],
                                    uint.Parse(splitedItem[5]),
                                    uint.Parse(splitedItem[6]),
                                    splitedItem[7],
                                    splitedItem[8]);
                list.Add(book);
            }
            return list;
        }
        private IEnumerable<ILibraryObject> GetAllNewspapers()
        {
            List<ILibraryObject> list = [];
            if (!File.Exists($"{DbName}Newspaper.txt"))
            {
                using (var sw = new StreamWriter($"{DbName}Newspaper.txt", true)) { }
            }
            var lines = File.ReadAllLines($"{DbName}Newspaper.txt", Encoding.UTF8);
            foreach (var line in lines)
            {
                //if (Validate(line, TypeOfLibraryObject.Newspaper)) { continue; }
                string[]? splitedItem = line.Split('|');
                for (int i = 0; i < splitedItem.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(splitedItem[i]))
                    {
                        splitedItem[i] = null;
                    }
                }
                uint? sI4 = null, sI5 = null, sI7 = null;
                DateTime? sI8 = null;
                if (uint.TryParse(splitedItem[4],out uint _))
                {
                    sI4 = uint.Parse(splitedItem[4]);
                }
                if (uint.TryParse(splitedItem[5], out uint _))
                {
                    sI5 = uint.Parse(splitedItem[5]);
                }
                if (uint.TryParse(splitedItem[7], out uint _))
                {
                    sI7 = uint.Parse(splitedItem[7]);
                }
                if (DateTime.TryParseExact(splitedItem[8], "yyyy-MM-dd", CultureInfo.InvariantCulture,DateTimeStyles.None,out var _))
                {
                    sI8 = DateTime.ParseExact(splitedItem[8], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }

                var newspaper = new Newspaper(uint.Parse(splitedItem[0]), 
                                              splitedItem[1], 
                                              splitedItem[2],
                                              splitedItem[3],
                                              sI4,
                                              sI5, 
                                              splitedItem[6],
                                              sI7,
                                              sI8,
                                              splitedItem[9]); 
                list.Add(newspaper);
            }
            return list;
        }
        public IEnumerable<ILibraryObject> GetAll()
        {
            IEnumerable<ILibraryObject> libraryObjects = [];
            List<ILibraryObject> list = [];
            list.AddRange(GetAllNewspapers());
            list.AddRange(GetAllBooks());
            libraryObjects = list;
            return libraryObjects;
        }

        public void ClearByType(TypeOfLibraryObject typeOfLibraryObject)
        {
            using (StreamWriter writer = new StreamWriter($"{DbName}{typeOfLibraryObject}.txt", false, System.Text.Encoding.UTF8)) { }
        }
        public void ClearAll()
        {
            ClearByType(TypeOfLibraryObject.Book);
            ClearByType(TypeOfLibraryObject.Newspaper);
        }
        public bool Validate(string item, TypeOfLibraryObject typeOfLibraryObject)
        {
            if (typeOfLibraryObject == TypeOfLibraryObject.Book)
            {
                var splitedItem = item.Split("|");
                if (splitedItem.Length != 9)
                {
                    throw new ArgumentException();
                    return false;
                }
                if (!uint.TryParse(splitedItem[0], out var id))
                {
                    throw new ArgumentException();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(splitedItem[0])||
                    string.IsNullOrWhiteSpace(splitedItem[1])||
                    string.IsNullOrWhiteSpace(splitedItem[2])||
                    string.IsNullOrWhiteSpace(splitedItem[3])||
                    string.IsNullOrWhiteSpace(splitedItem[4]))
                {
                    throw new ArgumentException();
                    return false;
                }
                if (!uint.TryParse(splitedItem[5], out var year) && splitedItem[5] != "")
                {
                    throw new ArgumentException();
                    return false;
                }
                if (!uint.TryParse(splitedItem[6], out var numberOfPages) && splitedItem[6] != "")
                {
                    throw new ArgumentException();
                    return false;
                }
            }
            else if (typeOfLibraryObject == TypeOfLibraryObject.Newspaper)
            {
                var splitedItem = item.Split("|");
                if (splitedItem.Length != 10)
                {
                    throw new ArgumentException();
                    return false;
                }
                if (!uint.TryParse(splitedItem[0], out var id))
                {
                    throw new ArgumentException();
                    return false;
                }
                if (!uint.TryParse(splitedItem[4], out var yearOfPublication) && splitedItem[4]!="")
                {
                    throw new ArgumentException();
                    return false;
                }
                if (!uint.TryParse(splitedItem[5], out var pages) && splitedItem[5] != "")
                {
                    throw new ArgumentException();
                    return false;
                }
                if (!uint.TryParse(splitedItem[7], out var number) && splitedItem[7] != "")
                {
                    throw new ArgumentException();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(splitedItem[0]) ||
                    string.IsNullOrWhiteSpace(splitedItem[1]) ||
                    string.IsNullOrWhiteSpace(splitedItem[2]))
                {
                    throw new ArgumentException();
                    return false;
                }
            }
            return true;
        }
    }
}
