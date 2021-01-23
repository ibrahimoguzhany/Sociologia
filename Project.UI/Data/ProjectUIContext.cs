using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.UI.Data
{
    public class ProjectUIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ProjectUIContext() : base("name=ProjectUIContext")
        {
        }

        public System.Data.Entity.DbSet<Project.ENTITIES.Entities.User> Users { get; set; }
    }
}
