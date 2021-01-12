using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Entities
{
    public class Note:BaseEntity
    {

        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsDraft { get; set; }
        public int LikeCount { get; set; }
        public int CategoryID { get; set; }
        public int UserID { get; set; }



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
