using Microsoft.AspNetCore.Mvc;

namespace GateAPI.Controllers.Integracoes
{
    public class LPRController(
        ILogger<LPRController> logger
        ) : BaseController(logger)
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
