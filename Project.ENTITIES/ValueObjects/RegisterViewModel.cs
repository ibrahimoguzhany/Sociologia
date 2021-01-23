using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.ENTITIES.ValueObjects
{
    public class RegisterViewModel
    {
        [DisplayName("Kullanıcı Adı"),
        Required(ErrorMessage = "{0} alanı boş bırakılamaz"),
        StringLength(25, ErrorMessage = "{0} Maksimum {1} Karakter olmalı")]
        public string Username { get; set; }


        [DisplayName("E-Posta"), 
        Required(ErrorMessage = "{0} alanı boş geçilemez"), 
        DataType(DataType.Password), 
        StringLength(25, ErrorMessage = "{0} Maksimum {1} Karakter olmalı"), EmailAddress(ErrorMessage = "{0} alanı için geçerli bir E-Posta giriniz.")]
        public string EMail { get; set; }



        [DisplayName("Şifre"), 
        Required(ErrorMessage = "{0} alanı boş geçilemez"), 
        DataType(DataType.Password), 
        StringLength(25, ErrorMessage = "{0} Maksimum {1} Karakter olmalı")]
        public string Password { get; set; }



        [DisplayName("Şifre"), 
        Required(ErrorMessage = "{0} alanı boş geçilemez"),
        DataType(DataType.Password), 
        StringLength(25, ErrorMessage = "{0} Maksimum {1} Karakter olmalı"),
        Compare("Password",ErrorMessage ="{0} ile bir uyuşmuyor")]
        public string RePassword { get; set; }

    }
}