﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCDto.Entity.Base
{
    #region MyRegion
    [Serializable]
    public class ReturnMsg
    {
        public Guid Guid { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }


        public ReturnMsg()
        {
            Guid = Guid.NewGuid();
        }
        public ReturnMsg(bool isSuccess)
        {
            Guid = Guid.NewGuid();
            IsSuccess = isSuccess;
            Message = isSuccess ? "请求成功" : "请求出错";
        }
        public ReturnMsg(bool isSuccess, string message)
        {
            Guid = Guid.NewGuid();
            IsSuccess = isSuccess;
            Message = message;
        }

        public ReturnMsg(bool isSuccess, string message, Guid guid)
        {
            Guid = guid;
            IsSuccess = isSuccess;
            Message = message;
        }

        public static implicit operator ReturnMsg(Task<ReturnMsg> v)
        {
            return v.Result;
        }
    }
    #endregion

}