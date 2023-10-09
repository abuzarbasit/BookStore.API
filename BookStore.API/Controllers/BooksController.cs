using BookStore.API.Models;
using BookStore.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository) { _bookRepository = bookRepository; }


        [HttpGet("All")]
        public async Task<IActionResult> GetAllBooks()
        {
            try {
                var books = await _bookRepository.GetAllBooksAsync();
                return Ok(books);
            }
            catch {
                return BadRequest();
            }
        }

        [HttpGet("Book")]
        public async Task<IActionResult> GetBook(int id)
        {
            try
            {
                var book = await _bookRepository.GetBookAsync(id);
                if (book == null)
                {
                    return NotFound();
                }
                else {
                    return Ok(book);
                }
            }
            catch
            { 
                return BadRequest();
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddBook(BookModel model)
        {
            try
            {
                var book = await _bookRepository.AddBookAsync(model);
                return Ok(book);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateBook(BookModel model)
        {
            try
            {
                var book = await _bookRepository.UpdateBook(model);
                return Ok(book);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
