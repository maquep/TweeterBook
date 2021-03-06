using System;
using System.Collections.Generic;

namespace TweeterBook.Contracts.V1.Responses
{
    public class PagedResponse<T>
    {
        public PagedResponse() { }

        public PagedResponse(IEnumerable<T> data)
        {
            Data = data;
        }

        public IEnumerable<T> Data { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string PageNext { get; set; }
        public string PreviousPage { get; set; }
    }
}
