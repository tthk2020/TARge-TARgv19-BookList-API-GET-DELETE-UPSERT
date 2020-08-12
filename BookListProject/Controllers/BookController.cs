using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListProject.Controllers
{
    [Route("api/Book")] //the route at which the API is called
    [ApiController] //specifying that the class is the API controller
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        //implementing HTTP GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //returning data from the db in JSON format
            return Json(new { data = await _db.Book.ToListAsync() });
        }

        //implementing HTTP DELETE
        [HttpDelete]

        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if(bookFromDb == null)
            {
                return Json(new { success = false, message = "Error, failed to delete an item" });
            }

            _db.Book.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Item successfully deleted" });

        }

    }
}
