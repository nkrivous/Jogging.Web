using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using Jogging.Web.Interfaces;
using Jogging.Web.Models;

namespace Jogging.Web.DAL
{
    public class JoggingRepository: Repository<JoggingItem>, IJoggingRepository
    {
        public override JoggingItem Get(int id)
        {
            return Get(x => x.Id == id).FirstOrDefault();
        }

        public override IEnumerable<JoggingItem> GetAll()
        {
            return db.Joggings;
        }

        public override IEnumerable<JoggingItem> Get(Expression<Func<JoggingItem, bool>> predicate)
        {
            return db.Joggings.Where(predicate);
        }

        public override JoggingItem Add(JoggingItem item)
        {
            var newJogging = db.Joggings.Add(item);
            db.SaveChanges();
            return newJogging;
        }

        public override void Update(JoggingItem item)
        {
            db.Joggings.AddOrUpdate(item);
            db.SaveChanges();
        }

        public override void Remove(int id)
        {
            db.Joggings.Remove(Get(id));
            db.SaveChanges();
        }

        public IEnumerable<JoggingItem> GetJoggingForUser(int userId)
        {
            return Get(x => int.Parse(x.User.Id) == userId);
        }

        public IEnumerable<JoggingItem> GetJoggingForUser(ApplicationUser user)
        {
            return Get(x => x.User == user);
        }
    }
}