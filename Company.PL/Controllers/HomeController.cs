using System.Diagnostics;
using System.Text;
using Company.PL.Models;
using Company.PL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Services.IServiceScope serviceScope01;
        private readonly Services.IServiceScope serviceScope02;
        private readonly IServiceTransient serviceTransient01;
        private readonly IServiceTransient serviceTransient02;
        private readonly ISeriveSingleton seriveSingleton01;
        private readonly ISeriveSingleton seriveSingleton02;

        public HomeController(
            ILogger<HomeController> logger,
            Services.IServiceScope serviceScope01,
            Services.IServiceScope serviceScope02,
            IServiceTransient serviceTransient01,
            IServiceTransient serviceTransient02,
            ISeriveSingleton seriveSingleton01,
            ISeriveSingleton seriveSingleton02
            )
        {
            _logger = logger;
            this.serviceScope01 = serviceScope01;
            this.serviceScope02 = serviceScope02;
            this.serviceTransient01 = serviceTransient01;
            this.serviceTransient02 = serviceTransient02;
            this.seriveSingleton01 = seriveSingleton01;
            this.seriveSingleton02 = seriveSingleton02;
        }

        public string TestLifeTime()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ServiceScope 01 : " + serviceScope01.getGuid() + "\n");
            sb.AppendLine("ServiceScope 02 : " + serviceScope02.getGuid() + "\n\n");
            sb.AppendLine("ServiceTransient 01 : " + serviceTransient01.getGuid() + "\n");
            sb.AppendLine("ServiceTransient 02 : " + serviceTransient02.getGuid() + "\n\n");
            sb.AppendLine("SeriveSingleton 01 : " + seriveSingleton01.getGuid() + "\n");
            sb.AppendLine("SeriveSingleton 02 : " + seriveSingleton02.getGuid() + "\n\n");

            return sb.ToString();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
