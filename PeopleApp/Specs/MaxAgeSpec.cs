using MyProfile.Common;
using PeopleApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PeopleApp.Specs
{
    public class MaxAgeSpec:Specification<User>
    {
        private readonly int _maxAge;
        public MaxAgeSpec(int maxAge)
        {
            _maxAge = maxAge;
        }
        public override Expression<Func<User, bool>> ToExpression()
        {
            return User => User.Age <= _maxAge;
        }
    }
}
