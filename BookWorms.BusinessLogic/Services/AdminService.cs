using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using BookWorms.BusinessLogic.Abstractions;
using BookWorms.BusinessLogic.DataModel;

namespace BookWorms.BusinessLogic.Services
{
    public class AdminService
    {
        IAdminRepository adminRepository;
        IBookRepository bookRepository;

        public AdminService(IAdminRepository adminRepository, IBookRepository bookRepository)
        {
            this.adminRepository = adminRepository;
            this.bookRepository = bookRepository;
        }
        //public Admin GetAdminById(Guid Id)
        //{
        //    if (Id == null)
        //    {
        //        throw new Exception("Null id");
        //    }
        //    return adminRepository.GetAdminById(Id);
        //}
        public IEnumerable<Admin> GetAll()
        {
            return adminRepository.GetAll();
        }

        public void AddBook(string userId, string BookDescription, string BookTitle)
        {
            Guid userIdGuid = Guid.Empty;
            if (!Guid.TryParse(userId, out userIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }
            var admin = adminRepository.GetAdminByUserId(userIdGuid);
            if (admin == null)
            {
                throw new Exception("Null Admin");
            }
            bookRepository.Add(new Book() { Id = Guid.NewGuid(), Admin = admin, BookTitle = BookTitle, BookDescription = BookDescription });
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return bookRepository.GetAll();
        }

        public Book GetBookById(Guid Id)
        {
            if (Id == null)
            {
                throw new Exception("Null id");
            }
            return bookRepository.GetBookById(Id);
        }


        public IEnumerable<Book> GetAdminBooks(string userId)
        {
            Guid userIdGuid = Guid.Empty;
            if (!Guid.TryParse(userId, out userIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }

            return bookRepository.GetAll()
                            .Where(book => book.Admin != null && book.Admin.UserId == userIdGuid)
                            .AsEnumerable();
        }


        public Admin GetAdminByUserId(string userId)
        {
            Guid userIdGuid = Guid.Empty;
            if (!Guid.TryParse(userId, out userIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }

            var admin = adminRepository.GetAdminByUserId(userIdGuid);
            if (admin == null)
            {
                throw new Exception("Null admin");
            }

            return admin;
        }

        public void DeleteBook(Guid bookId)
        {
            var oneBook = bookRepository.GetBookById(bookId);
           
            bookRepository.Delete(oneBook);
        }

        public void UpdateBook(Book books)
        {
            bookRepository.Update(books);
        }

    }
}
