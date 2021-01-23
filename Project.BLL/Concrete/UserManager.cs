using Project.BLL.Abstract;
using Project.DAL.DesignPatterns.GenericRepository.ConcRep;
using Project.BLL.Results;
using Project.COMMON.Helpers;
using Project.ENTITIES.Entities;
using Project.ENTITIES.Messages;
using Project.ENTITIES.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Concrete
{
    public class UserManager : ManagerBase<User>
    {
        public BusinessLayerResult<User> RegisterUser(RegisterViewModel data)
        {
            User user = Find(x => x.UserName == data.Username || x.Email == data.EMail);

            BusinessLayerResult<User> res = new BusinessLayerResult<User>();
            if (user != null)
            {
                if (user.UserName == data.Username)
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanici adi kayitli");


                if (user.Email == data.EMail)
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-Mail adi kayitli");

            }
            else
            {
                int dbResult = base.Insert(new User
                {
                    UserName = data.Username,
                    Email = data.EMail,
                    Password = data.Password,
                    ProfileImageFileName = "user.png",
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false,

                });


                if (dbResult > 0)
                {
                    res.Result = Find(x => x.Email == data.EMail && x.UserName == data.Username);

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivation/{res.Result.ActivateGuid}";
                    string body = $"Merhaba {res.Result.UserName};<br><br>Hesabinizi aktiflestirmek icin <a href='{activateUri}' target='_blank'> tiklayiniz.</a>";

                    MailHelper.SendMail(body, res.Result.Email, "Blognot Hesap Aktiflestirme");

                }
            }
            return res;
        }

        public BusinessLayerResult<User> GetUserById(int id)
        {
            BusinessLayerResult<User> res = new BusinessLayerResult<User>();
            res.Result = Find(x => x.ID == id);

            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanici Bulunamadi.");
            }

            return res;


        }

        public BusinessLayerResult<User> LoginUser(LoginViewModel data)
        {
            //giris kontrolu
            //hesap aktif mi
            BusinessLayerResult<User> res = new BusinessLayerResult<User>
            {
                Result = Find(x => x.UserName == data.Username && x.Password == data.Password)
            };


            if (res.Result != null)
            {
                if (!res.Result.IsActive.Value)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActivated, "Kullanıcı aktıfleştirilmemiştir.");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Lutfen e-posta adresinizi kontrol ediniz.");
                }

            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanici adi ya da sifre hatali.");

            }
            return res;
        }

        public BusinessLayerResult<User> UpdateProfile(User data)
        {
            User db_user = Find(x => x.UserName == data.UserName || x.Email == data.Email);
            BusinessLayerResult<User> res = new BusinessLayerResult<User>();

            if (db_user != null && db_user.ID != data.ID)
            {
                if (db_user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanici adi kayitli");
                }

                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayitli");
                }

                return res;
            }

            res.Result = Find(x => x.ID == data.ID);
            res.Result.Email = data.Email;
            res.Result.UserName = data.UserName;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Name = data.Name;

            if (string.IsNullOrEmpty(data.ProfileImageFileName) == false)
            {
                res.Result.ProfileImageFileName = data.ProfileImageFileName;
            }

            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil Guncellenemedi");
            }

            return res;

        }

        public BusinessLayerResult<User> RemoveUserById(int id)
        {
            BusinessLayerResult<User> res = new BusinessLayerResult<User>();
            User user = Find(x => x.ID == id);


            if (user != null)
            {
                if (Delete(user) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemoved, "Kullanici silinemedi");
                    return res;
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFound, "Kullanici bulunamadi");
            }
            return res;
        }

        public BusinessLayerResult<User> ActivateUser(Guid activateId)
        {
            BusinessLayerResult<User> res = new BusinessLayerResult<User>();

            res.Result = Find(x => x.ActivateGuid == activateId);

            if (res.Result != null)
            {
                if (res.Result.IsActive.Value)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Kullanici zaten aktif edilmistir.");
                    return res;
                }

                res.Result.IsActive = true;
                Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExist, "Aktifleştirilecek kullanıcı bulunamadı.");

            }



            return res;


        }


        public new BusinessLayerResult<User> Insert(User data)
        {
            //Method hiding..
            User user = Find(x => x.UserName == data.UserName || x.Email == data.Email);
            BusinessLayerResult<User> res = new BusinessLayerResult<User>();

            res.Result = data;

            if (user != null)
            {
                if (user.UserName == data.UserName)
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanici adi kayitli");


                if (user.Email == data.Email)
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-Mail adi kayitli");

            }
            else
            {
                res.Result.ProfileImageFileName = "user.png";
                res.Result.ActivateGuid = Guid.NewGuid();               

                if(base.Insert(res.Result)==0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotInserted, "Kullanici eklenemedi");
                }
            }
            return res;

        }

        public new BusinessLayerResult<User> Update(User data)
        {
            User db_user = Find(x => x.UserName == data.UserName || x.Email == data.Email);
            BusinessLayerResult<User> res = new BusinessLayerResult<User>();

            res.Result = data;

            if (db_user != null && db_user.ID != data.ID)
            {
                if (db_user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanici adi kayitli");
                }

                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayitli");
                }

                return res;
            }

            res.Result = Find(x => x.ID == data.ID);
            res.Result.Email = data.Email;
            res.Result.UserName = data.UserName;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Name = data.Name;
            res.Result.IsActive = data.IsActive;
            res.Result.IsAdmin= data.IsAdmin;

            if (string.IsNullOrEmpty(data.ProfileImageFileName) == false)
            {
                res.Result.ProfileImageFileName = data.ProfileImageFileName;
            }

            if(base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.UserCouldNotUpdated, "Kullanici Guncellenemedi");
            }

            return res;
        }


    }
}
