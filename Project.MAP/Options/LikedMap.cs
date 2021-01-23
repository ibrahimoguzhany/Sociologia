using Project.ENTITIES.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.MAP.Options
{
    public class LikedMap:BaseMap<Liked>
    {
        public LikedMap()
        {
            ToTable("Likes");

        }

        
    }
}
