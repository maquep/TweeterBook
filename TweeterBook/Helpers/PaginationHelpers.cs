using System.Collections.Generic;
using TweeterBook.Domain;
using TweeterBook.Services;
using TweeterBook.Contracts.V1.Requests.Queries;
using TweeterBook.Contracts.V1.Responses;
using System.Linq;

namespace TweeterBook.Helpers
{
    public class PaginationHelpers
    {
        public static PagedResponse<T> CreatePaginationResponse<T>(IUriService uriService, PaginationFilter paginationFilter, List<T> response)
        {
            var nextPage = paginationFilter.PageNumber >= 1
                ? uriService.GetAllPostUri(new PaginationQuery(paginationFilter.PageNumber + 1, paginationFilter.PageSize)).ToString()
                : string.Empty;

            var previousPage = paginationFilter.PageNumber - 1 >= 1
                ? uriService.GetAllPostUri(new PaginationQuery(paginationFilter.PageNumber - 1, paginationFilter.PageSize)).ToString()
                : string.Empty;

            return new PagedResponse<T>
            {
                Data = response,
                PageNumber = paginationFilter.PageNumber >= 1 ? paginationFilter.PageNumber : (int?)null,
                PageSize = paginationFilter.PageSize >= 1 ? paginationFilter.PageSize : (int?)null,
                PageNext = response.Any() ? nextPage : null,
                PreviousPage = previousPage

            };
        }
    }
}
