using Microsoft.AspNetCore.Mvc;
using MyProfile.Common;
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

        public HomeController(PeopleDbContext db) => _db = db;


        [Route("user/old/normal")]
        public IActionResult Index()

            => Ok(_db
                .Users
                .Where(u => u.Age > 70)
                .Select(u => new
                {
                    u.Givenname,
                    u.Surname,
                    u.Age,
                    u.Country
                }));
        

        [Route("user/old/filter")]
        public IActionResult Index2()
        {
            var model = _db.Users.Where(u => OldUsers(u))
                .Select(u => new { u.Givenname, u.Surname, u.Age, u.Country });
            return Ok(model);
        }

        [Route("user/old/exp")]
        public IActionResult Index3()
        {
            var s1 = new Specs.MinAgeSpec(20);
            var s2 = new Specs.MaxAgeSpec(30);

            var AgeBetween20and30 = s1.And(s2);

            var model = _db.Users.Where(AgeBetween20and30.ToExpression())
                .Select(u => new { u.Givenname, u.Surname, u.Age, u.Country });
            return Ok(model.ToList());
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