using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.BLL.Concrete;
using Project.ENTITIES.Entities;
using Project.UI.Models;

namespace Project.UI.Controllers
{
    public class NoteController : Controller
    {
        private NoteManager noteManager;
        private UserManager userManager;
        private CategoryManager categoryManager;
        private LikedManager likedManager;
        public NoteController()
        {
            noteManager = new NoteManager();
            userManager = new UserManager();
            categoryManager = new CategoryManager();
            likedManager = new LikedManager();
        }




        public ActionResult Index()
        {
            var notes = noteManager.ListQueryable().Include("Category").Include("User").Where(
                x => x.User.ID == CurrentSession.User.ID).OrderByDescending(
                x => x.ModifiedDate);

            //if (Session["login"] != null)
            //{
            //    User user = Session["login"] as User;
            //    var notes = noteManager.ListQueryable().Include("Category").Where(x => x.User.ID == user.ID);
            //    return View(noteManager.List());
            //}

            //return RedirectToAction("Index");




            return View(noteManager.List());
        }

        public ActionResult MyLikedNotes()
        {
            var notes = likedManager.ListQueryable().Include("LikedUser").Include("Note").Where(
                x => x.LikedUser.ID == CurrentSession.User.ID).Select(
                x => x.Note).Include("Category").Include("User").OrderByDescending(
                x => x.ModifiedDate);

            return View("Index", notes.ToList());
            
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.ID == id.Value);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }


        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(CacheHelper.GetCategoriesFromCache(), "ID", "Title");
          
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Note note)
        {
            ModelState.Remove("CreatedDate");
            ModelState.Remove("ModifiedDate");
            ModelState.Remove("ModifiedUserName");

            if (ModelState.IsValid)
            {
                note.User = CurrentSession.User;
                noteManager.Insert(note);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(CacheHelper.GetCategoriesFromCache(), "ID", "Title", note.CategoryID);
            //ViewBag.UserID = new SelectList(userManager.List(), "ID", "Name", note.UserID);
            return View(note);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.List().Find(x => x.ID == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(CacheHelper.GetCategoriesFromCache(), "ID", "Title", note.CategoryID);
            ViewBag.UserID = new SelectList(userManager.List(), "ID", "Name", note.ID);
            return View(note);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Note note)
        {
            ModelState.Remove("CreatedDate");
            ModelState.Remove("ModifiedDate");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                Note db_note = noteManager.Find(x => x.ID == note.ID);
                db_note.IsDraft = note.IsDraft;
                db_note.CategoryID = note.CategoryID;
                db_note.Text = note.Text;
                db_note.Title = note.Title;

                noteManager.Update(db_note);
                
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(CacheHelper.GetCategoriesFromCache(), "ID", "Title", note.CategoryID);
            
            return View(note);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.ID == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = noteManager.List().Find(x => x.ID == id);
            noteManager.Delete(note);
            return RedirectToAction("Index");
        }


    }
}
