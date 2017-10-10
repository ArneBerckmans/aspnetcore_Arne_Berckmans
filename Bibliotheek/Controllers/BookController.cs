using System;
using System.Collections.Generic;
using System.Linq;
using Bibliotheek.Data;
using Bibliotheek.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bibliotheek.Entities;

namespace Bibliotheek.Controllers
{
    public class BookController : Controller
    {
        private readonly EntityContext _entityContext;

        public BookController(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        [HttpGet("/books")]
        public IActionResult Index()
        {
            var model = new BookListViewModel();
            model.Books = new List<BookDetailViewModel>();
            var allBooks = _entityContext.Books.OrderBy(x => x.Title).Include(x => x.Authors).ThenInclude(x => x.Author).ToList();
            foreach (var book in allBooks)
            {
                var vm = ConvertBookDetailViewModel(book);
                model.Books.Add(vm);
            }
            return View(model);

        }

        private static BookDetailViewModel ConvertBookDetailViewModel(Book book)
        {
            var vm = new BookDetailViewModel();
            vm.Id = book.Id;
            vm.Title = book.Title;
            vm.Author = String.Join(",", book.Authors.Select(x => x.Author.FullName));
           /* foreach(var ba in book.Authors)
            {     
                
                vm.Author = ba.Author.FirstName + " " + ba.Author.LastName;
            }*/

            return vm;
        }

        [HttpGet("/books/{id}")]
        public IActionResult Detail([FromRoute] int id)
        {          
            //var vm = new BookDetailViewModel();

            var book = _entityContext.Books.Include(x => x.Authors).ThenInclude(x => x.Author).FirstOrDefault(x => x.Id == id);
            var vm = ConvertBookDetailViewModel(book);
            if(book == null)
            {
                return NotFound();
            }

            return View(vm);
        }

      

    }
}