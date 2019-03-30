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
            var model = _db.Users.Take(10).Select(u => new { u.Givenname, u.Surname, u.Age, u.Country });
            return Ok(model);
        }

        [Route("user/order/age")]
        public IActionResult OrderByAge()
        {
            var model = _db.Users.OrderByAge(true).Take(10).Select(u => new { u.Givenname, u.Surname, u.Age, u.Country });
            return Ok(model);
        }

        [Route("user/create")]
        public IActionResult Create()
        {
            var user=_db.Users.CreateByName("Hakim");
            _db.SaveChanges();
            return Ok(user.Number);
        }

        [Route("test/foreach")]
        public IActionResult ForEachTest()
        {
            var users = _db.Users.Take(100).ToList();

            var count = 0;

            foreach (var item in users)
            {
                count++;
            }
          
            return Ok(count);
        }

        [Route("test/forloop")]
        public IActionResult ForLoopTest()
        {
            var users = _db.Users.Take(100).ToList();

            var count = 0;

            for (int i = 0; i < users.Count(); i++)
            {
                count++;
            }

            return Ok(count);
        }

    }
}