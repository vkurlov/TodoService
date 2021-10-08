using Microsoft.AspNetCore.Mvc;
using Todo.Api.Filters;

namespace Todo.Api.Controllers
{
    /// <summary>
    /// Represents base API controller
    /// </summary>
    [ApiController]
    [CatchExceptionFilter]
    public abstract class BaseController : ControllerBase
    {
    }
}
