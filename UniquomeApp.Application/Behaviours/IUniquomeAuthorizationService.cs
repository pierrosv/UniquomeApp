namespace UniquomeApp.Application.Behaviours
{
    public interface IUniquomeAuthorizationService
    {
        Task<bool> AuthorizeAsync(string userId, string policy);
        Task<bool> IsInRoleAsync(string userId, string role);
    }
}