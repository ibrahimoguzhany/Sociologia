using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Entities
{
    public class Category : BaseEntity
    {
        [DisplayName("Baslik"), Required(ErrorMessage = "{0} alani gereklidir."),
        StringLength(50, ErrorMessage = "{0} alani max.{1} karakter icermeli.")]
        public string Title { get; set; }


        [DisplayName("Aciklama"),
       StringLength(50, ErrorMessage = "{0} alani max.{1} karakter icermeli.")]
        public string Description { get; set; }


        //Relational Properties
        public virtual List<Note> Notes { get; set; }

        public Category()
        {
            Notes = new List<Note>();
        }

    }
}
