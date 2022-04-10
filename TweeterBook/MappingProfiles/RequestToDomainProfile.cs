using AutoMapper;
using TweeterBook.Contracts.V1.Requests.Queries;
using TweeterBook.Domain;

namespace TweeterBook.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}
