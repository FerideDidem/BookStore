using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using System;
using System.Collections.Generic;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBooks;
using static WebApi.BookOperations.CreateBooks.CreateBookCommand;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.UpdateBook;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using WebApi.BookOperations.DeleteBook;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BooksController (BookStoreDbContext context)
        {
            _context = context;
        }

        // private static List<Book> BookList = new List<Book>()
        // {
        //     new Book 
        //     {
        //         Id = 1,
        //         Title ="Lean Startup",
        //         GenreId = 1,//Personal Growth
        //         PageCount =200,
        //         PublishDate = new DateTime(2001,06,12)
        //     },

        //     new Book 
        //     {
        //         Id = 1,
        //         Title ="Herland",
        //         GenreId = 2 , //Science Fiction
        //         PageCount =250,
        //         PublishDate = new DateTime(2010,05,23)
        //     },
        //     new Book 
        //     {
        //         Id = 1,
        //         Title ="Dune",
        //         GenreId = 2 , //Science Fiction
        //         PageCount =540,
        //         PublishDate = new DateTime(2001,12,21)
        //     },

        // };
        
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query =new GetBookDetailQuery(_context);
                query.BookId = id;
                result = query.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }
           
            return Ok(result);
        }

        // [HttpGet]
        // public Book Get([FromQuery] string id)
        // {
        //     var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook (int id,[FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                 UpdateBookCommand command = new UpdateBookCommand(_context);
                 command.BookId =id;
                 command.Model =updatedBook;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
    
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
           try
           {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            command.Handle();
           }
           catch (Exception ex)
           {
              return BadRequest(ex.Message);
           }
           return Ok();
        }
    }
    
}
 

    
    
