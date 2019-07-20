using Microsoft.AspNetCore.Mvc;
using PeopleApp.Common;
using PeopleApp.Data;
using System.Linq;

namespace PeopleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly PeopleDbContext _db;
        public HomeController(PeopleDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var model = _db.Users.Take(10);
            return Ok(model);
        }

    }
}