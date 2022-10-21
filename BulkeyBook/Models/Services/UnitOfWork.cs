using BulkeyBook.Models.DataAccess;
using BulkeyBook.Models.DataAccess.Modul;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext _db;
        private UserManager<UserINtoUser> _userManager;

        public UnitOfWork(ApplicationDBContext db, UserManager<UserINtoUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            shoppingRepository = new ShoppingRepository(_db);
            applicationRepository = new ApplicationRepository(_db, userManager);

        }
        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IShoppingRepository shoppingRepository { get; private set; }
        public IApplicationRepository applicationRepository { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
