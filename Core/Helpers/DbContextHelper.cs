using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Core.Helpers.DbContextHelper
{
    public static class DbContextHelper
    {
        ///<summary>
        /// Update every tables in given DbContext which has composite key.
        ///</summary>
        public static void UpdateCompositeKeys(this ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes();

            foreach (var entityType in entityTypes)
            {
                var properties = entityType.GetProperties();
                var keyProperties = properties
                    .Where(a => Attribute.GetCustomAttribute(a.PropertyInfo, typeof(KeyAttribute)) != null)
                    .ToList();

                if (keyProperties.Count() > 1)
                {
                    var keyNames = keyProperties.Select(a => a.Name).ToArray();
                    modelBuilder.Entity(entityType.ClrType).HasKey(keyNames);
                }

            }

        }
    }
}