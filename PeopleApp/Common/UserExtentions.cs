using Microsoft.EntityFrameworkCore;
using PeopleApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleApp.Common
{
    public static class UserExtentions
    {
        public static IQueryable<User> OrderByAge (this IQueryable<User> queryable, bool ascending)
        {
            return ascending
                ? queryable.OrderBy(u => u.Age)
                : queryable.OrderByDescending(u => u.Age);
        }

        public static void CreateByName(this DbSet<User> users, string Name)
        {
            users.Add(new User { Givenname = Name });
            
        }
    }
}
