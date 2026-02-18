using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared
{
    public class ExternalApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public int StatusCode { get; set; }

        public static ExternalApiResponse<T> Ok(T data, int code = 200)
            => new() { Success = true, Data = data, StatusCode = code };

        public static ExternalApiResponse<T> Fail(string error, int code)
            => new() { Success = false, ErrorMessage = error, StatusCode = code };
    }

}
