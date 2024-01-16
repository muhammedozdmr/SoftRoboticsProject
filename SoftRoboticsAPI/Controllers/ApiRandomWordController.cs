using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using SoftRobotics.Business;
using SoftRobotics.Dto;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftRoboticsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiRandomWordController : Controller
    {
        private readonly RandomWordService _randomWordService = new RandomWordService();
        private IConnection _connection;

        [HttpGet("GetRabbit")]
        public IActionResult Get()
        {
            //_randomWordService.DirectExchange();
            _randomWordService.GenerateRabbit();
            //_connection.Close();
            return Ok();
        }
        // GET: /<controller
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var randomWords = _randomWordService.GetAll();
            return Ok(randomWords);
        }
        [HttpPost("Generate")]
        public IActionResult Generate()
        {
            _randomWordService.GenerateWord();
            return Ok();
        }
        [HttpPost("Delete")]
        public IActionResult Delete(RandomWordDto model)
        {
            var commandResult = _randomWordService.Delete(model);
            if (commandResult.IsSuccess)
            {
                return Ok(commandResult);
            }
            else
            {
                return NotFound(commandResult);
            }
        }
    }
}

