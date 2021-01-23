using Project.DAL.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL
{
    public class Test
    {
        public Test()
        {
            MyContext db = new MyContext();
            db.Categories.ToList();
          
        }
    }
}
