using Project.ENTITIES.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class UserMap:BaseMap<User>
    {
        public UserMap()
        {
            ToTable("Kullanicilar");

            Property(x => x.Name).HasMaxLength(25).HasColumnName("isim");
            Property(x => x.Surname).HasMaxLength(25).HasColumnName("Soyad");
            Property(x => x.UserName).HasColumnName("Kullanici Adi").HasMaxLength(25);    
            Property(x => x.Email).HasColumnName("E-Posta").HasMaxLength(70);    
            Property(x => x.Password).HasColumnName("Sifre").HasMaxLength(25);
            Property(x => x.IsActive).HasColumnName("Aktif");
            Property(x => x.IsAdmin).HasColumnName("Yonetici");
            Property(x => x.ActivateGuid).IsOptional();


            //HasMany(x => x.Comments).WithOptional(x => x.User);

            HasMany(x => x.Notes).WithRequired(x => x.User);

            //HasOptional(x => x.Note).WithRequired(x => x.User);




        }
    }
}
