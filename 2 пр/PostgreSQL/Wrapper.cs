using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateSQL.PostgreSQL
{
    public static class Wrapper
    {
        public static T SelectValue<T>(this EstateContext context, long id) where T : class
        {
            var dbSetProperty = context.GetType().GetProperty(typeof(T).Name + "s"); // Assuming DbSet properties are named in plural form
            if (dbSetProperty == null)
            {
                throw new ArgumentException($"DbSet for type {typeof(T).Name} not found in DbContext.");
            }

            var dbSet = dbSetProperty.GetValue(context) as DbSet<T>;
            if (dbSet == null)
            {
                throw new InvalidOperationException($"Property {dbSetProperty.Name} is not of type DbSet<{typeof(T).Name}>.");
            }

            var entity = dbSet.ToList().FirstOrDefault(x => (long)x.GetType().GetProperty("Id").GetValue(x) == id);
            return entity;
        }
    }
}
