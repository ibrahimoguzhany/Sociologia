using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileImageFileName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsAdmin{ get; set; }

        public Guid? ActivateGuid { get; set; }


        //Relational Properties

        //public virtual Note Note  { get; set; }
        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }



    }
}
