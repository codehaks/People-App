using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

        //public async Task<IActionResult> OnGet(int minAge,CancellationToken cancellationToken)
        //{
        //    _logger.LogWarning("Operation Started!");
        //    await Task.Delay(10_000, cancellationToken);

        //    UserList = await _db.Users.Where(u=>u.Age>=minAge).Take(10).ToListAsync(cancellationToken);
        //    MinAge = minAge;
        //    _logger.LogWarning("Operation Finished!");
        //    return Page();
        //}

        public async Task<IActionResult> OnGet(int minAge)
        {
            _logger.LogWarning("Operation Started!");
            await Task.Delay(10_000);

            UserList = await _db.Users.Where(u => u.Age >= minAge).Take(10).ToListAsync();
            MinAge = minAge;
            _logger.LogWarning("Operation Finished!");
            return Page();
        }


        //public async Task<IActionResult> OnGet(int minAge, CancellationToken cancellationToken)
        //{
        //    _logger.LogWarning("Operation Started!");

        //    var cts = new CancellationTokenSource();
        //    cts.CancelAfter(3000);

        //    int counter = 0;
        //    while (true)
        //    {
        //        await Task.Delay(100);
        //        counter++;

        //        if (cancellationToken.IsCancellationRequested || cts.IsCancellationRequested)
        //        {
        //            _logger.LogWarning($" Counter : {counter} -> Operation canceled!");
        //            break;

        //        }
        //    }
        //    UserList = await _db.Users.Where(u => u.Age >= minAge).Take(1000).ToListAsync(cancellationToken);

        //    //var top10=UserList.sec


        //    _logger.LogWarning("Operation Finished!");
        //    return Page();
        //}
    }
}