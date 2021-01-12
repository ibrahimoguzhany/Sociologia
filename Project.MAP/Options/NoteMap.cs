using Project.ENTITIES.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class NoteMap : BaseMap<Note>
    {
        public NoteMap()
        {
            ToTable("Notlar");

            Property(x => x.Title).HasMaxLength(60).IsRequired().HasColumnName("Not Basligi");
            Property(x => x.Text).HasColumnName("Not Metni").HasMaxLength(2000).IsRequired();
            Property(x => x.IsDraft).HasColumnName("Taslak");
            Property(x => x.LikeCount).HasColumnName("Begenilme");
            Property(x => x.CategoryID).HasColumnName("Kategori");


            HasRequired(x => x.User).WithMany(x => x.Notes).WillCascadeOnDelete(false);
            
        }
    }
}
