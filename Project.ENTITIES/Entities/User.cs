using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Entities
{
    public class User : BaseEntity
    {
        [DisplayName("İsim"), Required(ErrorMessage = "{0} alani gereklidir."),
           StringLength(50, ErrorMessage = "{0} alani max.{1} karakter icermeli.")]
        public string Name { get; set; }


        [DisplayName("Soyisim"), Required(ErrorMessage = "{0} alani gereklidir."),
        StringLength(50, ErrorMessage = "{0} alani max.{1} karakter icermeli.")]
        public string Surname { get; set; }


        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alani gereklidir."),
        StringLength(50, ErrorMessage = "{0} alani max.{1} karakter icermeli.")]
        public string UserName { get; set; }


        [DisplayName("Mail"), Required(ErrorMessage = "{0} alani gereklidir."),
        StringLength(50, ErrorMessage = "{0} alani max.{1} karakter icermeli.")]
        public string Email { get; set; }


        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alani gereklidir."),
        StringLength(16, ErrorMessage = "{0} alani max.{1} karakter icermeli.")]
        public string Password { get; set; }


        [StringLength(30), ScaffoldColumn(false)]
        public string ProfileImageFileName { get; set; }


        [DisplayName("Aktif mi"), Required(ErrorMessage = "{0} alani gereklidir.")]
        public bool? IsActive { get; set; }



        [DisplayName("Admin mi"), Required(ErrorMessage = "{0} alani gereklidir.")]
        public bool? IsAdmin { get; set; }



        [ScaffoldColumn(false)]
        public Guid? ActivateGuid { get; set; }


        //Relational Properties

        //public virtual Note Note  { get; set; }
        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }



    }
}
