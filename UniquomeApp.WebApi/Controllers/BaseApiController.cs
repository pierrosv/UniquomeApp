using Microsoft.AspNetCore.Mvc;

namespace UniquomeApp.WebApi.Controllers;

public class BaseApiController : ControllerBase
{
    // private IMediator _mediator;
    // protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    // protected async Task<IActionResult> DownloadFile(string path)
    // {
    //     var memory = new MemoryStream();
    //     await using (var stream = new FileStream(path, FileMode.Open))
    //     {
    //         await stream.CopyToAsync(memory);
    //     }
    //     memory.Position = 0;
    //     return File(memory, WebTools.GetContentType(path), Path.GetFileName(path));
    // }
}