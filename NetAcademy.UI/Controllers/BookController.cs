using Microsoft.AspNetCore.Mvc;
using NetAcademy.DataBase.Entities;
using NetAcademy.Services.Abstractions;
using NetAcademy.UI.Models;

namespace NetAcademy.UI.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var books = (await _bookService.GetBooksAsync())
                .Select(book => new BookModel
                {
                    Id = book.Id,
                    ISBN = book.ISBN,
                    Name = book.Name,
                    Year = book.Year,
                    Price = book.Price,
                }).ToArray();
            return View(books);
        }

        public IActionResult Details(Guid id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookModel model)
        {
            if (model.Year> DateTime.Today.Year+2)
            {
                ModelState.AddModelError("Year", "Year is too big to have in our service");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var bookEntity = new Book()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                ISBN = model.ISBN,
                Year = model.Year.GetValueOrDefault(),
                Price = model.Price
            };
            await _bookService.AddBookAsync(bookEntity);
            return RedirectToAction("Index");
        }

    }
}
