using System.Collections.Generic;
using System.Linq;
using LibraryNetwork.Models;
using LibraryNetwork.Models.Entety;

namespace LibraryNetwork.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public void Add(ILibraryObject libraryObject)
        {
            bookRepository.Add(libraryObject);
        }
        public void Delete(TypeOfLibraryObject typeOfLibraryObject, int id)
        {
            bookRepository.Delete(typeOfLibraryObject, id);
        }
        public IEnumerable<ILibraryObject> GetAll(OrderBy? orderBy=null)
        {
            return orderBy switch
            {
                OrderBy.Asc => bookRepository.GetAll()
                                             .OrderBy(b => b.YearOfPublication),

                OrderBy.Desc => bookRepository.GetAll()
                                              .OrderByDescending(b => b.YearOfPublication),

                _ => bookRepository.GetAll()
            };
        }
        public IEnumerable<ILibraryObject> GetAll(TypeOfLibraryObject typeOfLibraryObject, OrderBy? orderBy=null)
        {
            return orderBy switch
            {
                OrderBy.Asc => bookRepository.GetAll()
                                             .Where(x => x.GetType().Name
                                             .Equals(typeOfLibraryObject.ToString()))
                                             .OrderBy(y => y.YearOfPublication),
                OrderBy.Desc => bookRepository.GetAll()
                                              .Where(x => x.GetType().Name
                                              .Equals(typeOfLibraryObject.ToString()))
                                              .OrderByDescending(y => y.YearOfPublication),
                _ => bookRepository.GetAll()
                                   .Where(x => x.GetType().Name
                                   .Equals(typeOfLibraryObject.ToString()))
            };
        }
        public IEnumerable<ILibraryObject> GetAllByTitle(string title, OrderBy? orderBy = null)
        {
            return orderBy switch
            {
                OrderBy.Asc => bookRepository.GetAll()
                                             .Where(t => t.Title.Equals(title))
                                             .OrderBy(y => y.YearOfPublication),
                OrderBy.Desc => bookRepository.GetAll()
                                              .Where(t => t.Title.Equals(title))
                                              .OrderByDescending(y => y.YearOfPublication),
                _ => bookRepository.GetAll()
                                   .Where(t => t.Title.Equals(title))
            };
        }  
        public IEnumerable<Book> GetBooksByAuthor(string author, OrderBy? orderBy = null)
        {
            return orderBy switch
            {
                OrderBy.Asc => GetAll(TypeOfLibraryObject.Book).OfType<Book>()
                                                               .Where(a => a.Authors
                                                               .Contains(author))
                                                               .OrderBy(y => y.YearOfPublication),

                OrderBy.Desc => GetAll(TypeOfLibraryObject.Book).OfType<Book>()
                                                                .Where(a => a.Authors
                                                                .Contains(author))
                                                                .OrderByDescending(y => y.YearOfPublication),

                _ => GetAll(TypeOfLibraryObject.Book).OfType<Book>()
                                                     .Where(a => a.Authors
                                                     .Contains(author))
            };
        }
        public IEnumerable<Book> GetBooksByPublisher(string publisher, OrderBy? orderBy=null)
        {
            return orderBy switch
            {
                OrderBy.Asc => GetAll(TypeOfLibraryObject.Book)
                                .OfType<Book>()
                                .Where(p => p.Publisher.Contains(publisher))
                                .OrderBy(p => p.Publisher),

                OrderBy.Desc => GetAll(TypeOfLibraryObject.Book)
                                .OfType<Book>()
                                .Where(p => p.Publisher.Contains(publisher))
                                .OrderByDescending(p => p.Publisher),

                _ => GetAll(TypeOfLibraryObject.Book)
                    .OfType<Book>()
                    .Where(p => p.Publisher.Contains(publisher))
            };
        }
        public void ClearByType(TypeOfLibraryObject typeOfLibraryObject)
        {
            bookRepository.ClearByType(typeOfLibraryObject);
        }
    }
}