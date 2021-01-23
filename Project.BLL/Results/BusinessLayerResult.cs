﻿using Project.ENTITIES.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Results
{
    public class BusinessLayerResult<T> where T : class
    {
        public List<ErrorMessageObj> Errors { get; set; }
        public T Result { get; set; }
        public string Message { get; private set; }

        public BusinessLayerResult()
        {
            Errors = new List<ErrorMessageObj>();
        }

        public void AddError(ErrorMessageCode code, string message)
        {
            Errors.Add(new ErrorMessageObj 
                { 
                     Code = code,
                     Message = message
                });
        }
    }
}
