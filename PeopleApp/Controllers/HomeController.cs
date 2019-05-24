using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PeopleApp.Common;
using PeopleApp.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly PeopleDbContext _db;


        public IConfiguration Configuration { get; }

        public HomeController(PeopleDbContext db, IConfiguration configuration)
        {
            _db = db;
            Configuration = configuration;
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
            var user = _db.Users.CreateByName("Hakim");
            _db.SaveChanges();
            return Ok(user.Number);
        }

        [Route("user/dapper/sync")]
        public IActionResult GetSync()
        {
            var s = new System.Diagnostics.Stopwatch();
            s.Start();
            using (IDbConnection connection = 
                new SqliteConnection("Data Source=People.db"))
            {
                var q = connection.Query<User>("SELECT * FROM USERS");
                var u = q.ToList();
            }
            s.Stop();
            return Ok(s.ElapsedMilliseconds);
        }

        [Route("user/dapper/result")]
        public IActionResult GetAsyncResult()
        {
            var s = new System.Diagnostics.Stopwatch();
            s.Start();
            using (IDbConnection connection =
                new SqliteConnection("Data Source=People.db"))
            {
                var q = connection.QueryAsync<User>("SELECT * FROM USERS")
                    .Result.ToList();
            }
            s.Stop();
            return Ok(s.ElapsedMilliseconds);
        }

        [Route("user/dapper/async")]
        public async Task<IActionResult> GetAsync()
        {
            var s = new System.Diagnostics.Stopwatch();
            s.Start();
            using (IDbConnection connection = new 
                SqliteConnection("Data Source=People.db"))
            {
                var q = await connection
                    .QueryAsync<User>("SELECT * FROM USERS");
                var q2 = q.ToList();
            }
            s.Stop();
            return Ok(s.ElapsedMilliseconds);
        }

        [Route("user/ef/async")]
        public async Task<IActionResult> GetEFAsync()
        {
            var s = new System.Diagnostics.Stopwatch();
            s.Start();
            var u = await _db.Users.ToListAsync();
            s.Stop();
          
            return Ok(s.ElapsedMilliseconds);

        }

        [Route("user/raw/async")]
        public async Task<IActionResult> GetRawAsync()
        {
            var s = new System.Diagnostics.Stopwatch();
            s.Start();
            var u = await _db.Users
                .FromSql(new RawSqlString("SELECT * FROM USERS"))
                .ToListAsync();
            s.Stop();

            return Ok(s.ElapsedMilliseconds);

        }

    }
}