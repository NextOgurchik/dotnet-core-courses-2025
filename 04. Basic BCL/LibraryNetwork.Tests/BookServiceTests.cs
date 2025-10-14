using FluentAssertions;
using LibraryNetwork.Implementation;
using LibraryNetwork.Models;
using LibraryNetwork.Models.Entety;
using Moq;

namespace LibraryNetwork.Tests
{
    public class BookServiceTests
    {
        private readonly List<ILibraryObject> libraryObjects =
        [
            new Newspaper(1,"������1","��������1",null,null,null,null,null,null,null),
            new Newspaper(2,"�����2","��������2",null,null,null,null,null,null,null),
            new Book(1, "�����1", ["�����1"], "����� �������1", "��������1", 1966, 100, "����������1", "978-5-699-12014-7"),
            new Book(2, "�����2", ["�����1", "�����2"], "����� �������2", "��������2", 2000, 120, "����������2", "978-5-699-12015-7"),
            new Book(3, "�����3", ["�����3"], "����� �������3", "��������1", 1990, 90, "����������3", "978-5-699-12016-7")
        ];

        private Mock<IBookRepository> GetBookRepositoryMock()
        {
            var repositoryMoq = new Mock<IBookRepository>();
            repositoryMoq.Setup(repos => repos.GetAll()).Returns(libraryObjects);
            return repositoryMoq;
        }
        [Test]
        public void Add_AddLibraryObjectToRepository()
        {
            List<ILibraryObject> addedBooks =
            [
                new Newspaper(1,"������1","��������1",null,null,null,null,null,null,null),
                new Newspaper(2,"�����2","��������2",null,null,null,null,null,null,null),
                new Book(1, "�����1", ["�����1"], "����� �������1", "��������1", 1966, 100, "����������1", "978-5-699-12014-7"),
                new Book(2, "�����2", ["�����1", "�����2"], "����� �������2", "��������2", 2000, 120, "����������2", "978-5-699-12015-7"),
                new Book(3, "�����3", ["�����3"], "����� �������3", "��������1", 1990, 90, "����������3", "978-5-699-12016-7"),
                new Book(4, "�����99", ["�����99"], "����� �������99", "��������99", 1986, 400, "����������99", "978-5-699-12099-7")
            ];
            //var repositoryMoq = GetBookRepositoryMock();
            //repositoryMoq.Setup(repos => repos.GetAll()).Returns(libraryObjects);

            //IBookService bookService = new BookService(repositoryMoq.Object);
            BookRepository bookRepository = new BookRepository();
            bookRepository.ClearByType(TypeOfLibraryObject.Book);
            bookRepository.ClearByType(TypeOfLibraryObject.Newspaper);
            IBookService bookService = new BookService(bookRepository);

            bookService.Add(new Newspaper(1, "������1", "��������1", null, null, null, null, null, null, null));
            bookService.Add(new Newspaper(2, "�����2", "��������2", null, null, null, null, null, null, null));
            bookService.Add(new Book(1, "�����1", ["�����1"], "����� �������1", "��������1", 1966, 100, "����������1", "978-5-699-12014-7"));
            bookService.Add(new Book(2, "�����2", ["�����1", "�����2"], "����� �������2", "��������2", 2000, 120, "����������2", "978-5-699-12015-7"));
            bookService.Add(new Book(3, "�����3", ["�����3"], "����� �������3", "��������1", 1990, 90, "����������3", "978-5-699-12016-7"));
            bookService.Add(new Book(4, "�����99", ["�����99"], "����� �������99", "��������99", 1986, 400, "����������99", "978-5-699-12099-7"));

            IEnumerable<ILibraryObject> result = bookService.GetAll();

            result.Should().BeEquivalentTo(addedBooks);
        }

        [Test]
        public void Delete_DeleteLibraryObjectFromRepositoryById()
        {
            List<ILibraryObject> deletedBooks =
            [
                new Newspaper(1,"������1","��������1",null,null,null,null,null,null,null),
                new Newspaper(2,"�����2","��������2",null,null,null,null,null,null,null),
                new Book(2, "�����2", ["�����1", "�����2"], "����� �������2", "��������2", 2000, 120, "����������2", "978-5-699-12015-7"),
                new Book(3, "�����3", ["�����3"], "����� �������3", "��������1", 1990, 90, "����������3", "978-5-699-12016-7")
            ];

            //var repositoryMoq = new Mock<IBookRepository>();
            //repositoryMoq.Setup(repos => repos.GetAll()).Returns(libraryObjects);

            //IBookService bookService = new BookService(repositoryMoq.Object);
            BookRepository bookRepository = new BookRepository();
            bookRepository.ClearByType(TypeOfLibraryObject.Book);
            bookRepository.ClearByType(TypeOfLibraryObject.Newspaper);
            IBookService bookService = new BookService(bookRepository);

            bookService.Add(new Newspaper(1, "������1", "��������1", null, null, null, null, null, null, null));
            bookService.Add(new Newspaper(2, "�����2", "��������2", null, null, null, null, null, null, null));
            bookService.Add(new Book(1, "�����1", ["�����1"], "����� �������1", "��������1", 1966, 100, "����������1", "978-5-699-12014-7"));
            bookService.Add(new Book(2, "�����2", ["�����1", "�����2"], "����� �������2", "��������2", 2000, 120, "����������2", "978-5-699-12015-7"));
            bookService.Add(new Book(3, "�����3", ["�����3"], "����� �������3", "��������1", 1990, 90, "����������3", "978-5-699-12016-7"));
            bookService.Delete(TypeOfLibraryObject.Book,1);
            IEnumerable<ILibraryObject> result = bookService.GetAll();

            result.Should().BeEquivalentTo(deletedBooks);
        }
        
        [Test]
        public void GetAll_ReturnAllLibraryObjectList()
        {
            var repositoryMoq = GetBookRepositoryMock();

            IBookService bookService = new BookService(repositoryMoq.Object);
            IEnumerable<ILibraryObject> result = bookService.GetAll();

            result.Should().BeEquivalentTo(libraryObjects);
        }

        [Test]
        [TestCase(OrderBy.Asc)]
        [TestCase(OrderBy.Desc)]
        public void GetAllSorted_ReturnAllLibraryObjectListAscOrDescOrderByYear(OrderBy orderBy)
        {
            var repositoryMoq = GetBookRepositoryMock();

            IBookService bookService = new BookService(repositoryMoq.Object);
            IEnumerable<ILibraryObject> result = bookService.GetAll(orderBy);

            result.Should().Equal(orderBy == OrderBy.Asc ? 
                                  libraryObjects.OrderBy(b => b.YearOfPublication) : 
                                  libraryObjects.OrderByDescending(b => b.YearOfPublication));
        }

        [Test]
        [TestCase(OrderBy.Asc)]
        [TestCase(OrderBy.Desc)]
        public void GetAll_ReturnAllLibraryObjectByTypeAscAndDescByYearOfPublication(OrderBy orderBy)
        {
            List<ILibraryObject> books =
            [
                new Book(1,
                         "�����1",
                         ["�����1"],
                         "����� �������1",
                         "��������1",
                         1966,
                         100,
                         "����������1",
                         "978-5-699-12014-7"),
                new Book(2,
                         "�����2",
                         ["�����1", "�����2"],
                         "����� �������2",
                         "��������2",
                         2000,
                         120,
                         "����������2",
                         "978-5-699-12015-7"),
                new Book(3,
                         "�����3",
                         ["�����3"],
                         "����� �������3",
                         "��������1",
                         1990,
                         90,
                         "����������3",
                         "978-5-699-12016-7")
            ];

            var repositoryMoq = new Mock<IBookRepository>();
            repositoryMoq.Setup(repos => repos.GetAll()).Returns(libraryObjects);

            IBookService bookService = new BookService(repositoryMoq.Object);
            IEnumerable<ILibraryObject> result = bookService.GetAll(TypeOfLibraryObject.Book, orderBy);

            result.Should().BeEquivalentTo(orderBy == OrderBy.Asc ?
                                  books.OrderBy(b => b.YearOfPublication) :
                                  books.OrderByDescending(b => b.YearOfPublication));
        }

        [Test]
        [TestCase(OrderBy.Asc)]
        [TestCase(OrderBy.Desc)]
        public void GetAllByTitle_ReturnAllLibraryObjectByTitleAscAndDescByYearOfPublication(OrderBy orderBy)
        {
            List<ILibraryObject> libraryObjectsByTitle =
            [
            new Newspaper(2,"�����2","��������2",null,null,null,null,null,null,null),
            new Book(2, "�����2", ["�����1", "�����2"], "����� �������2", "��������2", 2000, 120, "����������2", "978-5-699-12015-7"),
            ];

            var repositoryMoq = new Mock<IBookRepository>();
            repositoryMoq.Setup(repos => repos.GetAll()).Returns(libraryObjects);

            IBookService bookService = new BookService(repositoryMoq.Object);
            IEnumerable<ILibraryObject> result = bookService.GetAllByTitle("�����2",orderBy);

            result.Should().BeEquivalentTo(orderBy == OrderBy.Asc ?
                                  libraryObjectsByTitle.OrderBy(b => b.YearOfPublication) :
                                  libraryObjectsByTitle.OrderByDescending(b => b.YearOfPublication));
        }

        [Test]
        [TestCase(OrderBy.Asc)]
        [TestCase(OrderBy.Desc)]
        public void GetBooksByAuthor_ReturnAllBooksByAuthorAscAndDescByYearOfPublication(OrderBy orderBy)
        {
            List<ILibraryObject> booksByAuthor =
            [
            new Book(1, "�����1", ["�����1"], "����� �������1", "��������1", 1966, 100, "����������1", "978-5-699-12014-7"),
            new Book(2, "�����2", ["�����1", "�����2"], "����� �������2", "��������2", 2000, 120, "����������2", "978-5-699-12015-7"),
            ];

            var repositoryMoq = new Mock<IBookRepository>();
            repositoryMoq.Setup(repos => repos.GetAll()).Returns(libraryObjects);

            IBookService bookService = new BookService(repositoryMoq.Object);
            IEnumerable<ILibraryObject> result = bookService.GetBooksByAuthor("�����1");

            result.Should().BeEquivalentTo(orderBy == OrderBy.Asc ?
                                  booksByAuthor.OrderBy(b => b.YearOfPublication) :
                                  booksByAuthor.OrderByDescending(b => b.YearOfPublication));
        }

        [Test]
        [TestCase(OrderBy.Asc)]
        [TestCase(OrderBy.Desc)]
        public void GetBooksByPublisher_ReturnAllBooksByPublisherAsc(OrderBy orderBy)
        {
            List<ILibraryObject> booksByPublisher =
            [
            new Book(1, "�����1", ["�����1"], "����� �������1", "��������1", 1966, 100, "����������1", "978-5-699-12014-7"),
            new Book(3, "�����3", ["�����3"], "����� �������3", "��������1", 1990, 90, "����������3", "978-5-699-12016-7")
            ];

            var repositoryMoq = new Mock<IBookRepository>();
            repositoryMoq.Setup(repos => repos.GetAll()).Returns(libraryObjects);

            IBookService bookService = new BookService(repositoryMoq.Object);
            IEnumerable<ILibraryObject> result = bookService.GetBooksByPublisher("��������1", OrderBy.Asc);

            result.Should().BeEquivalentTo(orderBy == OrderBy.Asc ?
                                  booksByPublisher.OrderBy(b => b.YearOfPublication) :
                                  booksByPublisher.OrderByDescending(b => b.YearOfPublication));
        }
    }
}