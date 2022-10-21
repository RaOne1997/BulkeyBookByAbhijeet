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
    public class ApplicationRepository : Repository<UserINtoUser>, IApplicationRepository
    {
        private ApplicationDBContext _db;

        private UserManager<UserINtoUser> _userManager;
        public ApplicationRepository(ApplicationDBContext db, UserManager<UserINtoUser> userManager) : base(db)
        {
            _db = db;
        }


        public async Task Update(UserINtoUser obj)
        {
           await _userManager.UpdateAsync(obj);
        }
    }
}
