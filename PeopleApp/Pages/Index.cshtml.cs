using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PeopleApp.Data;

namespace PeopleApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger _logger;

        public IndexModel(PeopleDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
        }

        private readonly PeopleDbContext _db;

        public int MinAge { get; set; }

        public IList<User> UserList { get; set; }

        public async Task<IActionResult> OnGet(int minAge,CancellationToken cancellationToken)
        {
            await Task.Delay(10_000, cancellationToken);

            UserList = await _db.Users.Where(u=>u.Age>=minAge).Take(10).ToListAsync(cancellationToken);
            MinAge = minAge;

            return Page();
        }
    }
}