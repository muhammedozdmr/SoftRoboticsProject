using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using SoftRobotics.Business;
using SoftRobotics.Business.Repository;
using SoftRobotics.DataAccess;
using SoftRobotics.Dto;

namespace SoftRobotics.RepositoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIRepositoryController : Controller
    {
        private readonly SoftRoboticsContext _context = new SoftRoboticsContext();
        private readonly RabbitRepository _repository = new RabbitRepository(dbContext: _context);
        private IConnection _connection;
        [HttpGet("GetRabbitRepository")]
        public IActionResult GetRabbitRepository()
        {
            _repository.DirectExchange();
            return Ok();
        }
        [HttpGet("IndexRepository")]
        public IActionResult IndexRepository()
        {
            var randomwords = _repository.GetAll();
            return Ok(randomwords);
        }
        public IActionResult Delete(RandomWordDto model)
        {
            var commandResult = _repository.Delete(model);
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
