using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using PeopleApp.Data;

namespace PeopleApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMemoryCache _cache;

        public IndexModel(PeopleDbContext db, IMemoryCache cache)
        {
            _db = db;
            _cache = cache;
        }

        private readonly PeopleDbContext _db;


        public IList<User> UserList { get; set; }
        public void OnGet()
        {
            List<User> users;
            if (_cache.TryGetValue("users", out users) == false)
            {
                users = _db.Users.OrderBy(u => u.Maidenname).Take(10).ToList();

                _cache.Set("users", users,
                    new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromSeconds(5)
                    });
            }

            UserList = users;

        }
    }
}