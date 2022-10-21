﻿using BulkeyBook.Models.DataAccess;
using BulkeyBook.Models.DataAccess.Modul;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{

    public class ShoppingRepository : Repository<ShoppingCart>, IShoppingRepository{
        private ApplicationDBContext _db;

        public ShoppingRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public void Update(ShoppingCart obj)
        {
            _db.shoppingCarts.Update(obj);
        }
    }
}
