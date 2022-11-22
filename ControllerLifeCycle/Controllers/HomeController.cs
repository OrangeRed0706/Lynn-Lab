using ControllerLifeCycle.Models;
using ControllerLifeCycle.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ControllerLifeCycle.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeController _this;
        private readonly ILogger<HomeController> _logger;
        private readonly HelloWorld _helloWorld;

        public HomeController(ILogger<HomeController> logger, HelloWorld helloWorld)
        {
            _logger = logger;
            _helloWorld = helloWorld;
            _logger.LogInformation("HomeController Constructor");
            _helloWorld.HelloWorldPrint();
            _this = this;
        }

        public IActionResult Index()
        {
            Console.WriteLine(_this.GetHashCode());
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