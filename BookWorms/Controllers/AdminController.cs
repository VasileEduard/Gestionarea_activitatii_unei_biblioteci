using BookWorms.DataAccess;
using BookWorms.Models.Admins;
using BookWorms.BusinessLogic.DataModel;
using BookWorms.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace BookWorms.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly AdminService adminsService;
        public AdminController(UserManager<IdentityUser> userManager, AdminService adminsService)
        {
            this.userManager = userManager;
            this.adminsService = adminsService;
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var userId = userManager.GetUserId(User);
                var admin = adminsService.GetAdminByUserId(userId);
                var AdminBook = adminsService.GetAdminBooks(userId);
                return View(new AdminBookViewModel { Admin = admin, Books = AdminBook });
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received");
            }
        }

        [HttpPost]
        public IActionResult AddBook([FromForm] AdminAddBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var userId = userManager.GetUserId(User);
                adminsService.AddBook(userId, model.Name, model.Description);
                return Redirect(Url.Action("Index", "Admin"));
            }
            catch(Exception)
            {
                return BadRequest("Invalid request received");
            }

        }

        [HttpGet]
        public IActionResult DeleteBook()
        {
            //create instance

            return View();
        }
        [HttpGet]
        public IActionResult UpdateBook(Guid id)
        {
            var books = adminsService.GetBookById(id);

            return View(books);
        }


        [HttpPost]
        public IActionResult UpdateBook(Guid id, [FromForm] Book suggestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            adminsService.UpdateBook(suggestion);
            return Redirect(Url.Action("Index", "Admin"));
        }
        [HttpPost]
        public IActionResult DeleteBook(Guid Id)
        {

            adminsService.DeleteBook(Id);
            return Redirect(Url.Action("Index", "Admin"));
        }
       

    }
}