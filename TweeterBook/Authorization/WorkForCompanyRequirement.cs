using Microsoft.AspNetCore.Authorization;

namespace TweeterBook.Authorization
{
    public class WorkForCompanyRequirement : IAuthorizationRequirement
    {
        public string DomainName { get;}
        public WorkForCompanyRequirement(string domainName)
        {
            DomainName = domainName;
        }
    }
}
