using Project.ENTITIES.Entities;
using Project.MAP.Options;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.EntityFramework
{
    public class MyContext:DbContext
    {

        public MyContext():base("myConnection")
        {
            Database.SetInitializer(new MyInitializer());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new LikedMap());
            modelBuilder.Configurations.Add(new NoteMap());
            modelBuilder.Configurations.Add(new UserMap());
        }



        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Liked> Likes { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
