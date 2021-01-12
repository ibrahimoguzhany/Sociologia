using Project.ENTITIES.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class CategoryMap:BaseMap<Category>
    {
        public CategoryMap()
        {
            ToTable("Kategoriler");


            Property(x => x.Title).HasColumnName("Kategori").HasMaxLength(50).IsRequired();
            Property(x => x.Description).HasColumnName("Aciklama").HasMaxLength(150);



            HasMany(x => x.Notes).WithRequired(x => x.Category).WillCascadeOnDelete(false);

        }

    }
}
