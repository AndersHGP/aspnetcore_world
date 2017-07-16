using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApplication1.Model;
using WebApplication1.Services;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IWorldRepository _worldRepository;
        private ILogger<AppController> _logger;

        public AppController(IMailService mailService, IConfigurationRoot config, IWorldRepository worldRepository, ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _worldRepository = worldRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var data = _worldRepository.GetAll<Trip>();

                return View(data);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Unknown fault caught: {ex}");
                return Redirect("/error");
            }

        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if(model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("Email", "We don't support AOL addresses.");
            }

            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "The subject", model.Message);

                ViewBag.UserMessage = "Message sent successfully!";
                
                ModelState.Clear();
            }

            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}