using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public object Errors { get; set; }
        public string Message { get; set; }

        public static ApiResponse<T> Ok(T data, string msg = "")
            => new() { Success = true, Data = data, Message = msg };

        public static ApiResponse<T> Fail(string msg, object errors = null)
            => new() { Success = false, Message = msg, Errors = errors };
    }


}
