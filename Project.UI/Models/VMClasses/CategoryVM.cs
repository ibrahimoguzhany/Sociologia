using Project.ENTITIES.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.UI.Models.VMClasses
{
    public class CategoryVM
    {
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }

    }
}