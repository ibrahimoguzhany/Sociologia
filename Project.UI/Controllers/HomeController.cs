using Project.BLL;
using Project.BLL.Concrete;
using Project.BLL.Results;
using Project.ENTITIES.Entities;
using Project.ENTITIES.Messages;
using Project.ENTITIES.ValueObjects;
using Project.UI.Models;
using Project.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Project.UI.Controllers
{
    public class HomeController : Controller
    {
        private UserManager userManager;
        private NoteManager noteManager;
        private CategoryManager categoryManager;
        public HomeController()
        {
            userManager = new UserManager();
            noteManager = new NoteManager();
            categoryManager = new CategoryManager();
        }
         
        // GET: Home
        public ActionResult Index()
        {
            //if(TempData["mm"]!= null)
            //{
            //    return View(TempData["mm"] as List<Note>);
            //}

            return View(noteManager.ListQueryable().OrderByDescending(x => x.ModifiedDate).ToList());
            //return View(nm.GetAllNoteQuertable().OrderByDescending(x=>x.ModifiedDate).ToList());
        }

        public ActionResult ByCategory(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            Category cat = categoryManager.Find(x=>x.ID == id);

            if (cat == null)
            {
                return HttpNotFound();
            }

            return View("Index", cat.Notes.OrderByDescending(x => x.ModifiedDate).ToList());
        }

        public ActionResult MostLiked()
        {
            NoteManager nm = new NoteManager();
            return View("Index", nm.ListQueryable().OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult About()
        {
            return View();

        }

        public ActionResult ShowProfile()
        {
         
            BusinessLayerResult<User> res = userManager.GetUserById(CurrentSession.User.ID);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel
                {
                    Title = "Hata Olustur",
                    Items = res.Errors
                };
                return View("Error", errorNotifyObj);

            }

            return View(res.Result);
        }

        public ActionResult EditProfile()
        {

            BusinessLayerResult<User> res = userManager.GetUserById(CurrentSession.User.ID);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel
                {
                    Title = "Hata Olustur",
                    Items = res.Errors
                };
                return View("Error", errorNotifyObj);
            }
            return View(res.Result);

        }

        [HttpPost]
        public ActionResult EditProfile(User model, HttpPostedFile ProfileImage)
        {
            ModelState.Remove("ModifiedUsername");


            if (ModelState.IsValid)
            {
                if (ProfileImage != null &&
               (ProfileImage.ContentType == "image/jpeg" ||
               ProfileImage.ContentType == "image/jpg" ||
               ProfileImage.ContentType == "image/png"))
                {
                    string fileName = $"user_{model.ID}.{ProfileImage.ContentType.Split('/')[1]}";

                    ProfileImage.SaveAs(Server.MapPath($"~/images/{fileName}"));
                    model.ProfileImageFileName = fileName;
                }

                BusinessLayerResult<User> res = userManager.UpdateProfile(model);

                if (res.Errors.Count > 0)
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "Profil Guncellenemedi",
                        RedirectingUrl = "/Home/EditProfile"
                    };
                    return View("Error", errorNotifyObj);
                }
                //Profili guncelledigi icin session guncellendi.
                CurrentSession.Set<User>("login",res.Result);

                return RedirectToAction("ShowProfile");
            }

            return View(model);


        }

        public ActionResult DeleteProfile()
        {
           


            BusinessLayerResult<User> res = userManager.RemoveUserById(CurrentSession.User.ID);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel
                {
                    Items = res.Errors,
                    Title = "Profil silinemedi",
                    RedirectingUrl = "/Home/ShowProfile"
                };
                return View("Error", errorNotifyObj);
            }

            Session.Clear();

            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<User> res = userManager.LoginUser(model);

                if (res.Errors.Count > 0)
                {

                    if (res.Errors.Find(x => x.Code == ErrorMessageCode.UserIsNotActivated) != null)
                    {
                        ViewBag.SetLink = "https://localhost:44341/Home/UserActivation/" + res.Result.ActivateGuid; //TODO : Mail'den aktivasyon guidini gondermesi saglanacak. Simdilik MailHelper classim ile mail gonderebilmeyi basaramadim.
                    }
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(model);
                }

                CurrentSession.Set<User>("login", res.Result); // session'a kullanici bilgi saklama...
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {



                BusinessLayerResult<User> res = userManager.RegisterUser(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

            }
            OkViewModel notifyObj = new OkViewModel()
            {
                Title = "Kayit Basarili",
                RedirectingUrl = "/Home/Login",

            };

            notifyObj.Items.Add("Lutfen e-posta adresinize gonderdigimiz aktivasyon linkine tiklayarak hesabinizi aktive ediniz. Hesabinizi aktive etmeden not ekleyemez ve begenme yapamazsiniz.");

            return View("Ok", notifyObj);
        }



        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }

        public ActionResult UserActivation(Guid id)
        {
            BusinessLayerResult<User> res = userManager.ActivateUser(id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel
                {
                    Title = "Gecersiz islem",
                    Items = res.Errors
                };
                return View("Error", errorNotifyObj);
            }

            OkViewModel okNotifyObj = new OkViewModel()
            {
                Title = "Hesap Aktiflestirildi",
                RedirectingUrl = "/Home/Login",

            };

            okNotifyObj.Items.Add("Hesabiniz aktiflestirildi. Artik not paylasabilir ve begenme yapabilirsiniz");

            return View("Ok", okNotifyObj);

        }







    }
}