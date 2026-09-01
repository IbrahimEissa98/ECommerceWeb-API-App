using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ApiBaseController : ControllerBase
{
}
