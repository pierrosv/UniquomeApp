using Ardalis.Specification;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.Specs;

public sealed class ApplicationUserByEmailSpec : Specification<ApplicationUser>
{
    public ApplicationUserByEmailSpec(string email)
    {
        Query.Where(x => x.Email == email);
    }
}

