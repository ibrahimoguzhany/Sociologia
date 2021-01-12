using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Project.DAL.EntityFramework;
using Project.ENTITIES.Entities;

namespace Project.DAL
{
    public class MyInitializer : CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            //Adding admin user...
            User admin = new User()
            {
                Name = "Oguzhan",
                Surname = "YILMAZ",
                Email = "ibrahimoguzhany@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                UserName = "forthecoder",
                Password = "123456",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "ibrahimoguzhany"
            };

            //Adding standart user...
            User standartuser = new User
            {
                Name = "Ibrahim",
                Surname = "YILMAZ",
                Email = "ibrahim.oguzhan_4@hotmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                UserName = "kozolak",
                Password = "654321",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now.AddHours(1),
                ModifiedUsername = "ibrahimoguzhany"
            };
            for (int i = 0; i < 8; i++)
            {

                User user = new User
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    UserName = $"user{i}",
                    Password = "123",
                    CreatedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUsername = $"user{i}"
                };
                context.Users.Add(user);
            }


            context.Users.Add(admin);
            context.Users.Add(standartuser);
            context.SaveChanges();


            //Adding fake Categories
            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                };

                context.Categories.Add(cat);

                //Adding Notes
                for (int k = 0; k < FakeData.NumberData.GetNumber(5, 9); k++)
                {
                    Note note = new Note
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        Category = cat,
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        User = (k % 2 == 0) ? admin : standartuser,
                        CreatedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUsername = (k % 2 == 0) ? admin.UserName : standartuser.UserName,
                    };

                    cat.Notes.Add(note);

                    //Adding Comments

                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 5); j++)
                    {
                        Comment comment = new Comment
                        {
                            Text = FakeData.TextData.GetSentence(),
                            Note = note,
                            User = (j % 2 == 0) ? admin : standartuser,
                            CreatedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUsername = (j % 2 == 0) ? admin.UserName : standartuser.UserName,
                        };

                        note.Comments.Add(comment);

                    }

                    //Adding Fake Likes

                    List<User> userList = context.Users.ToList();

                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked
                        {
                            LikedUser = userList[m],
                        };
                        note.Likes.Add(liked);
                    }

                }
            }

            context.SaveChanges();
        }
    }
}
