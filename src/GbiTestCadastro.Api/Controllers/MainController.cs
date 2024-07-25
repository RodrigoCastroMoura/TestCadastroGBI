using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace GbiTestCadastro.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class MainController : ControllerBase
    {
        [Route("/")]
        [Route("/docs")]
        [Route("/swagger")]
        public IActionResult Index() =>
            new RedirectResult("~/swagger");
    }
}
