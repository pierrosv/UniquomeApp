using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UniquomeApp.WebApi.Controllers.V1;

[ApiController]
public class SystemController : BaseApiController
{
    [AllowAnonymous]
    [HttpGet(ApiRoutesV1.System.ReleaseVersion)]
    public ActionResult<string> VersionId()
    {
        return  Ok("0.0.1");
    }
}