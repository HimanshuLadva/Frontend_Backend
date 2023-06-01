using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // get all book
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            //var records = await _context.Books.Select(x => new BookModel()
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Description = x.Description,
            //}).ToListAsync();
            var records = await _context.Books.ToListAsync();

            return _mapper.Map<List<BookModel>>(records);
        }

        // get one book
        public async Task<BookModel> GetOneBooksAsync(int bookId)
        {
            //var records = await _context.Books.Where(x => x.Id == bookId).Select(x => new BookModel()
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Description = x.Description,
            //}).FirstOrDefaultAsync();

            // using automapper
            var record = await _context.Books.FindAsync(bookId);
            return _mapper.Map<BookModel>(record);
        }

        // add new book
        public async Task<string> AddNewBookAsync(BookModel model)
        {
            var book = new Books()
            {
                Title = model.Title,
                Description = model.Description,
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return "Added book Completely";
        }

        // update existing book
        public async Task UpdateExistingBookAsync(int bookId, BookModel model)
        {
            /*  var book = await _context.Books.FindAsync(bookId);
             if (book != null)
             {
                 book.Title = model.Title;
                 book.Description = model.Description;

                 await _context.SaveChangesAsync();
             } */

            var book = new Books()
            {
                Id = bookId,
                Title = model.Title,
                Description = model.Description,
            };

            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        // update property of book
        public async Task UpdateExistingBookDataAsync(int bookId, JsonPatchDocument model)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                model.ApplyTo(book);
                await _context.SaveChangesAsync();
            }
        }

        // delete row
        public async Task DeleteBookAsync(int bookId)
        {
            var book = new Books() {
                Id = bookId
            };

           _context.Books.Remove(book);
           await _context.SaveChangesAsync();
            // var book = await _context.Books.FindAsync(bookId);
            // if (book != null)
            // {
            //     _context.Books.Remove(book);
            //     await _context.SaveChangesAsync();
            // }
        }
    }
}
