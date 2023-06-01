using BookStoreAPI.Models;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // get all books
        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return Ok(books);
        }

        // get one book
        [HttpGet("{id}")]

        public async Task<IActionResult> GetOneBook([FromRoute] int id)
        {
            var book = await _bookRepository.GetOneBooksAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // add new book 
        [HttpPost("")]

        public async Task<IActionResult> AddBook([FromBody] BookModel model)
        {
            var message = await _bookRepository.AddNewBookAsync(model);
            return Ok(message);
        }

        //update existing book
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookModel model)
        {
           await _bookRepository.UpdateExistingBookAsync(id, model);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateDataOfBook([FromRoute] int id, [FromBody] JsonPatchDocument model)
        {
           await _bookRepository.UpdateExistingBookDataAsync(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute]int id)
        {
            await _bookRepository.DeleteBookAsync(id);
            return Ok();
        }
    }
}
