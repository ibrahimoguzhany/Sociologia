using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Entities
{
    public class Comment:BaseEntity
    {

        public string Text { get; set; }
        public int NoteID { get; set; }
        public int UserID { get; set; }


        //Relational Properties

        public virtual Note Note { get; set; }
        public virtual User User{ get; set; }

    }
}
