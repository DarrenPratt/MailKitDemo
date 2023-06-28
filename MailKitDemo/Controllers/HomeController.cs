using MailKitDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MailKitLib;
using MailKitLib.Access;

namespace MailKitDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISMTPAccess _smtp;


        public HomeController(ILogger<HomeController> logger, ISMTPAccess smtp)
        {
            _logger = logger;
            _smtp = smtp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {

            //var emailSender = new EmailSender();
            //emailSender.SendEmail();

            string To = "someone@somewhere.com";
            _smtp.SendEmail(To);



            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}