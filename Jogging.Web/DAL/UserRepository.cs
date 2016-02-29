using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using Jogging.Web.Interfaces;
using Jogging.Web.Models;

namespace Jogging.Web.DAL
{
    public class UserRepository: Repository<ApplicationUser>, IUserRepository
    {

        public override ApplicationUser Get(int id)
        {
            return Get(x => int.Parse(x.Id) == id).FirstOrDefault();
        }

        public override IEnumerable<ApplicationUser> GetAll()
        {
            return db.Users;
        }

        public override IEnumerable<ApplicationUser> Get(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return db.Users.Where(predicate);
        }

        public override ApplicationUser Add(ApplicationUser item)
        {
            var newUser = db.Users.Add(item);
            db.SaveChanges();
            return newUser;
        }

        public override void Update(ApplicationUser item)
        {
            db.Users.AddOrUpdate(item);
            db.SaveChanges();
        }

        public override void Remove(int id)
        {
            db.Users.Remove(Get(id));
            db.SaveChanges();
        }
    }
}