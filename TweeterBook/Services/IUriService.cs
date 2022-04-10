using System;
using TweeterBook.Contracts.V1.Requests.Queries;

namespace TweeterBook.Services
{
    public interface IUriService
    {
        Uri GetPostUri(string postId);
        Uri GetAllPostUri(PaginationQuery pagination = null);
    }
}
