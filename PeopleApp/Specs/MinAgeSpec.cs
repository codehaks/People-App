using MyProfile.Common;
using PeopleApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PeopleApp.Specs
{
    public class MinAgeSpec : Specification<User>
    {
        private readonly int _minAge;
        public MinAgeSpec(int minAge)
        {
            _minAge = minAge;
        }
        public override Expression<Func<User, bool>> ToExpression()
        {
            return User => User.Age > _minAge;
        }
    }
}
