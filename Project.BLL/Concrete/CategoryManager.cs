﻿using Project.BLL.Abstract;
using Project.ENTITIES.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Concrete
{
    public class CategoryManager:ManagerBase<Category>
    {
        //public override int Delete(Category category)
        //{

        //    NoteManager noteManager = new NoteManager();
        //    LikedManager likedManager = new LikedManager();
        //    CommentManager commentManager = new CommentManager();

        //    // Kategori ile iliskili notlarin silinmesi gerekiyor.
        //    foreach (Note note in category.Notes.ToList())
        //    {
        //        // Note ile iliskili like'larin silinmesi

        //        foreach (Liked like in note.Likes.ToList())
        //        {
        //            likedManager.Delete(like);
        //        }

        //        //Note ile iliskili commentlerin silinmesi

        //        foreach (Comment comment in note.Comments.ToList())
        //        {
        //            commentManager.Delete(comment);
        //        }

        //        noteManager.Delete(note);
        //    }

        //    return base.Delete(category);
        //}
    }
}