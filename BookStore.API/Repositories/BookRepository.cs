using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Repositories
{
    public interface IBookRepository {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookAsync(int bookID);
        Task<int> AddBookAsync(BookModel model);
        Task<int> UpdateBook(BookModel model);
    }
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext context;
       public BookRepository(BookStoreContext dbContext)
        { 
            context = dbContext;
        }
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            List<BookModel> books = new();
           var records=await context.Books.Select(i=>new BookModel() { 
           Id=i.Id,
           Author=i.Author,
           Description=i.Description,
           Name=i.Name,
           Title=i.Title
           }).ToListAsync();
            books.AddRange(records);
            
            



            return books;
        }
        public async Task<BookModel> GetBookAsync(int bookID)
        {
            List<BookModel> books = new();
            var records = await context.Books.Where(i=>i.Id==bookID).Select(i => new BookModel()
            {
                Id = i.Id,
                Author = i.Author,
                Description = i.Description,
                Name = i.Name,
                Title = i.Title
            }).FirstOrDefaultAsync();
            





            return records;
        }

        public async Task<int> AddBookAsync(BookModel model)
        {
            var book = new Books() {
            Author= model.Author,
            Description=model.Description,
            Name=model.Name,
             Title=model.Title};
            context.Books.Add(book);
            await context.SaveChangesAsync();
            return book.Id;
        }
        public async Task<int> UpdateBook(BookModel model)
        {
            var book = await context.Books.FindAsync(model.Id);
            if (book != null) { 
                book.Author = model.Author;
                book.Description = model.Description;
                book.Name = model.Name; 
                    book.Title = model.Title;
              await      context.SaveChangesAsync();
            }
            return book.Id;
        }
    }
}
