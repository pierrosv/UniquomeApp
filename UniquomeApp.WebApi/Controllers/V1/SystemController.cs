using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniquomeApp.Application.Proteomes.Commands;
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
    [HttpPost(ApiRoutesV1.System.SeedIdentity)]
    public async Task<ActionResult> SeedIdentity()
    {
        await AppIdentityDbContextSeed.SeedAsync(_serviceProvider);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutesV1.System.LoadProteomes)]
    public async Task<ActionResult> LoadProteomes([FromBody] LoadProteomesCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}