using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PeopleApp.Data;

namespace PeopleApp.Pages
{
    public class IndexModel : PageModel
    {

        public IndexModel(PeopleDbContext db)
        {
            _db = db;
        }
       
        private readonly PeopleDbContext _db;
     

        public IList<User> UserList { get; set; }
        public void OnGet()
        {
            UserList = _db.Users.OrderBy(u=>u.Maidenname).Take(10).ToList();
        }
    }
}