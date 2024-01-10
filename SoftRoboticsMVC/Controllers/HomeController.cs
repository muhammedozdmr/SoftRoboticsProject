using Microsoft.AspNetCore.Mvc;
using SoftRobotics.Business;
using SoftRobotics.Dto;
using SoftRoboticsMVC.Models;
using System.Diagnostics;
using X.PagedList;

namespace SoftRoboticsMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RandomWordService _randomWordService = new RandomWordService();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int pageNumber = 1)
        {
            var randomWords = _randomWordService.GetAll();
            var listWord = randomWords.ToPagedList(pageNumber, 10);
            return View(listWord);
        }

        public IActionResult RandomWordGenerator()
        {
            _randomWordService.GenerateWord();
            return RedirectToAction("Index");
        }

        //TODO: Kontrollü delete işleminde problem yaşanıyor. id ile değil model ile silme işlemi yapılıyor.
        public IActionResult Delete(int id)
        {
            var word = _randomWordService.GetById(id);
            if (word != null)
            {
                return View("Index");
            }
            else
            {
                TempData["ErrorMessage"] = $"{id} ID'li Kayıt bulunamadı";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Delete(RandomWordDto model)
        {
            var commandresult = _randomWordService.Delete(model);
            if (commandresult.IsSuccess)
            {
                TempData["SuccessMessage"] = commandresult.Message;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = commandresult.Message;
                return View(model);
            }
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
