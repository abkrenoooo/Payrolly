using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.BLL.Helpers.Responses
{
    public class GenericResponse<T> where T : class
    {
        public bool Success { get; set; }

        public int StatusCode { get; set; } = 401;

        public string Message { get; set; } = "Bad Request";

        public T? Data { get; set; }
    }
}
