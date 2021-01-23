using Project.COMMON;
using Project.ENTITIES.Entities;
using Project.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.UI.Init
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUsername()
        {
            User user = CurrentSession.User;

            if (user != null)
                return user.UserName;
            else 
                return "system";
                
        }
    }
}