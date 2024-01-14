using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoftRobotics.Business;
using SoftRobotics.Dto;
using SoftRoboticsMVC.Models;
using System.Diagnostics;
using System.Text;
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

        #region No Api Just Controller
        //public IActionResult Index(int pageNumber = 1)
        //{
        //    var randomWords = _randomWordService.GetAll();
        //    var listWord = randomWords.ToPagedList(pageNumber, 10);
        //    return View(listWord);
        //}

        //public IActionResult RandomWordGenerator()
        //{
        //    _randomWordService.GenerateWord();
        //    return RedirectToAction("Index");
        //}

        //TODO: Kontrollü delete işleminde problem yaşanıyor. id ile değil model ile silme işlemi yapılıyor.
        //public IActionResult Delete(int id)
        //{
        //    var word = _randomWordService.GetById(id);
        //    if (word != null)
        //    {
        //        return View("Index");
        //    }
        //    else
        //    {
        //        TempData["ErrorMessage"] = $"{id} ID'li Kayıt bulunamadı";
        //        return RedirectToAction("Index");
        //    }
        //}

        //[HttpPost]
        //public IActionResult Delete(RandomWordDto model)
        //{
        //    var commandresult = _randomWordService.Delete(model);
        //    if (commandresult.IsSuccess)
        //    {
        //        TempData["SuccessMessage"] = commandresult.Message;
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        ViewBag.ErrorMessage = commandresult.Message;
        //        return RedirectToAction("Index");
        //    }
        //}
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion


        #region With Api
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7058/");
            HttpResponseMessage response = await client.GetAsync("api/ApiRandomWord/Index/");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                List<RandomWordDto> randomWords = JsonConvert.DeserializeObject<List<RandomWordDto>>(result);
                var listWord = randomWords.ToPagedList(pageNumber, 10);
                return View(listWord);
            }
            else
            {
                TempData["ErrorMessage"] = await response.Content.ReadAsStringAsync();
                return View();
            }
        }

        [HttpPost("RandomWordGenerator")]
        public async Task<IActionResult> RandomWordGenerator()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7058/");
            HttpResponseMessage response = await client.PostAsync("api/ApiRandomWord/Generate/",null);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Kelime üretildi.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Kelime üretilemedi";
                return RedirectToAction("Index");
            }
        }

        [HttpPost("DeleteWord")]
        public async Task<IActionResult> DeleteWord(RandomWordDto model)
        {
            #region Model Kontrol
            //if (!ModelState.IsValid)
            //{
            //    foreach(var modelstate in ViewData.ModelState.Values)
            //    {
            //        foreach(var error in modelstate.Errors)
            //        {
            //            Console.WriteLine(error.ErrorMessage);
            //            TempData["ErrorMessage"] = error.ErrorMessage;
            //        }
            //    }
            //    return RedirectToAction("Index");
            //}
            #endregion
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7058/");
            //var jsonContent = new StringContent(JsonConvert.SerializeObject(new { model.Id}), Encoding.UTF8, "application/json");
            //HttpResponseMessage response = await client.PostAsync("api/ApiRandomWord/Delete", jsonContent);
            HttpResponseMessage response = await client.PostAsJsonAsync("api/ApiRandomWord/Delete", new { model.Id });
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                if (result != null)
                {
                    TempData["SuccessMessage"] = $"{model.Id} ID'li kelime silindi";
                }
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = $"{model.Id} ID'li kelime silinmedi";
                //ViewBag.ErrorMessage = "Silinmedi";
                return RedirectToAction("Index");
            }
        }
        #endregion

    }
}
