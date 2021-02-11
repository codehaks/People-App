using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleApp.Common;
using PeopleApp.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace PeopleApp.Controllers
{

    public class HomeController : Controller
    {
        private readonly PeopleDbContext _db;

        public HomeController(PeopleDbContext db) => _db = db;

        public IActionResult Index() =>
             Ok(_db.Users.OrderBy(u => u.Maidenname).Take(10));

        [Route("api/project")]
        public IActionResult IndexProject() =>
           Ok(_db.Users.Take(10).ProjectToType<UserInfo>());

        [Route("api/adapt")]
        public IActionResult IndexAdapt()
        {
            var model = _db.Users.First().Adapt<UserInfo>();
            return Ok(model);
        }
        public class UserInfo
        {
            public string Title { get; set; }
            public string Givenname { get; set; }
            public string Middleinitial { get; set; }
            public string Surname { get; set; }
        }

        [Route("get")]
        public IActionResult Get()
        {
            var data = new List<string>();
            using var command = _db.Database.GetDbConnection().CreateCommand();
            command.CommandText = "SELECT gender,givenname,age FROM Users LIMIT 10";
            _db.Database.OpenConnection();

            using var result = command.ExecuteReader();
            while (result.Read())
            {
                data.Add(result.GetString(0) + " - " + result.GetString(1) + " - " + result.GetString(2));
            }

            return Ok(data);
        }


    }
}