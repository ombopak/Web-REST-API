using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTAPI.Data;
using RESTAPI.Models;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BOOKDATABASEContext _context;

        public BooksController(BOOKDATABASEContext context)
        {
            _context = context;
        }     

        // GET: api/GetAllBooks
        [HttpGet("GetAllBooks")]
        public async Task<IEnumerable<GetAllBookResult>> GetAllBookResultAsync()
        {
            var result = await _context.GetProcedures().GetAllBookAsync();

            if(result == null)
            {
                return (IEnumerable<GetAllBookResult>)BadRequest("Something Went Wrong!");
            }
            return result;
        }

        //GET: api/GetBookById/1
        [HttpGet("GetBookById")]
        public async Task<IEnumerable<GetBookByIdResult>> GetBookByIdsAsync(int BookId)
        {
            var result = await _context.GetProcedures().GetBookByIdAsync(BookId);

            if (result == null)
            {
                return (IEnumerable<GetBookByIdResult>)BadRequest("Something Went Wrong!");
            }
            return result;  
        }

        // PUT: api/EditBook
        [HttpPut("EditBook")]
        public async Task<IActionResult> UpdateBookAsync(int BookId, string BookTitle, string ISBN, string PublisherName, string AuthorName, string CategoryName)
        {
            var result = await _context.GetProcedures().UpdateBookAsync(BookId, BookTitle, ISBN, PublisherName, AuthorName, CategoryName);

            if (result < 0)
            {
                return BadRequest("IDSomething Went Wrong!");
            }
            return Ok("Success Edit Book");
        }

        // POST: api/AddBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddBook")]
        public async Task<IActionResult> CreateBookAsync(string Title, string Isbn, string PublisherName, string AuthorName, string CategoryName)
        {
            var result = await _context.GetProcedures().CreateBookAsync(Title,Isbn,PublisherName, AuthorName, CategoryName);

            if (result < 0)
            {
                return BadRequest("Something Went Wrong!");
            }
            return Ok("Success Add Book");
        }

        // DELETE: api/DeleteBooks/5
        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBookAsync(int BookId)
        {
            var result = await _context.GetProcedures().DeleteBookAsync(BookId);

            if (result < 0)
            {
                return BadRequest("Something Went Wrong!");
            }
            return Ok("Success Delete Book");
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
