using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.UI.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        CategoryRepository crep;

        public CategoryController()
        {
            crep = new CategoryRepository();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}