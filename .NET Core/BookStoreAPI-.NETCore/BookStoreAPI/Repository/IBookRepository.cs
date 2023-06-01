using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStoreAPI.Repository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetOneBooksAsync(int bookId);
        Task<string> AddNewBookAsync(BookModel model);
        Task UpdateExistingBookAsync(int bookId, BookModel model);
        Task UpdateExistingBookDataAsync(int bookId, JsonPatchDocument model);
        Task DeleteBookAsync(int bookId);
    }
}
