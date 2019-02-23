using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PeopleApp.Data;

namespace PeopleApp.Controllers
{
    public class UserController : Controller
    {
        private readonly PeopleDbContext _db;

        public UserController(PeopleDbContext db)
        {
            _db = db;
        }

        [Route("api/users")]
        public IActionResult Get(int page)
        {
            var pageSize = 10;

            var skip = (page - 1) * pageSize;

            var model = _db.Users.Skip(skip).Take(pageSize);
            return Ok(model);
        }
    }
}