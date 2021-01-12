﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Entities
{
    public class Category:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }


        //Relational Properties
        public virtual List<Note> Notes { get; set; }

        public Category()
        {
            Notes = new List<Note>();
        }

    }
}