using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Entities
{
    public class Liked:BaseEntity    
    {
      
        public int ID { get; set; }

        //Relational Properties
        public virtual Note Note { get; set; }
        public virtual User LikedUser { get; set; }


    }
}
