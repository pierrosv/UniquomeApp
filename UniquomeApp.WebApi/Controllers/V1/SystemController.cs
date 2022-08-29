using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniquomeApp.Infrastructure.Security;

namespace UniquomeApp.WebApi.Controllers.V1;

[ApiController]
public class SystemController : BaseApiController
{
    private readonly IServiceProvider _serviceProvider;

    public SystemController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [AllowAnonymous]
    [HttpGet(ApiRoutesV1.System.ReleaseVersion)]
    public ActionResult<string> VersionId()
    {
        return  Ok("0.0.1");
    }

    [AllowAnonymous]
    [HttpPost("initializeidentity")]
    public async Task<ActionResult> InitializeIdentity()
    {
        await AppIdentityDbContextSeed.SeedAsync(_serviceProvider);
        return Ok();
    }
}