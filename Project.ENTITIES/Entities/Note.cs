using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Entities
{
    public class Note:BaseEntity
    {


        [DisplayName("Not Başlığı"), Required,StringLength(60)]
        public string Title { get; set; }

        [DisplayName("Not Metni"), Required, StringLength(60)]
        public string Text { get; set; }

        [DisplayName("Taslak")]
        public bool IsDraft { get; set; }

        [DisplayName("Beğenilme")]
        public int LikeCount { get; set; }

        [DisplayName("Kategori ")]
        public int CategoryID { get; set; }

      


        //Relational Properties

        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }


        public Note()
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }

    }
}
