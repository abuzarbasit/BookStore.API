using AutoMapper;
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
        Task DeleteBook(int bookID);
    }
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext context;
        private readonly IMapper _mapper;
       public BookRepository(BookStoreContext dbContext,IMapper mapper)

        { 
            context = dbContext;
            _mapper = mapper;
        }
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
           
           var records=await context.Books.ToListAsync();
            return _mapper.Map<List<BookModel>>(records);
        }
        public async Task<BookModel> GetBookAsync(int bookID)
        {
            List<BookModel> books = new();
           

            var book = await context.Books.FindAsync(bookID);


            return _mapper.Map<BookModel>(book);
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
            var book = new Books()
            {
                Id=model.Id,
                Author = model.Author,
                Description = model.Description,
                Name = model.Name,
                Title = model.Title
                
            };
            context.Books.Update(book);
            await context.SaveChangesAsync();
            return book.Id;
        }

        public async Task DeleteBook(int bookID)
        {
            var book = context.Books.Where(i=>i.Id == bookID).FirstOrDefault();
            if (book != null) {
                context.Books.Remove(book);
                await context.SaveChangesAsync();
            }
           
            await context.SaveChangesAsync();
           
        }

    }
}
