using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweeterBook.Contracts.V1.Responses
{
    public class Response<T>
    {
        public Response() {}

        public Response(T response)
        {
            Data = response;
        }

        public T Data { get; set; }
    }
}
