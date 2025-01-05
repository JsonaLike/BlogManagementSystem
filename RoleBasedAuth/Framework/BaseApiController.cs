using Microsoft.AspNetCore.Mvc;
using MT.Innovation.Shared.Utils;

namespace MT.Innovation.WebApiAdmin.Framework;

[ApiController]
public abstract class BaseApiController : ControllerBase
{

    [NonAction]
    protected PagedResult<T> PagedResult<T>(PagedList<T> pagedList) where T : class
    {
        return new PagedResult<T>(pagedList);
    }

}




