using Microsoft.AspNetCore.Mvc;
using PeopleApp.Common;
using PeopleApp.Data;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PeopleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly PeopleDbContext _db;
        public HomeController(PeopleDbContext db)
        {
            _db = db;
        }

        [Route("user/old/normal")]
        public IActionResult Index()
        {
            var model = _db.Users.Where(u => u.Age > 70).Select(u => new { u.Givenname, u.Surname, u.Age, u.Country });
            return Ok(model);
        }

        [Route("user/old/filter")]
        public IActionResult Index2()
        {
            var model = _db.Users.Where(u => OldUsers(u)).Select(u => new { u.Givenname, u.Surname, u.Age, u.Country });
            return Ok(model);
        }

        [Route("user/old/exp")]
        public IActionResult Index3()
        {
            var model = _db.Users.Where(IsOldUser).Select(u => new { u.Givenname, u.Surname, u.Age, u.Country });
            return Ok(model);
        }

        private Expression<Func<User, bool>> IsOldUser
        {
            get { return User => User.Age > 70; }
        }

        private bool OldUsers(User user)
        {
            return user.Age > 70;
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
            var user = _db.Users.CreateByName("Hakim");
            _db.SaveChanges();
            return Ok(user.Number);
        }


    }
}