using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using NLog;
using BusinessLayer.Interface;

namespace HelloGreetingApplication.Controllers
{
    /// <summary>
    /// Class Providing API for HelloGreeting
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HelloGreetingController : ControllerBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IGreetingBL _greetingBL;

        public HelloGreetingController(IGreetingBL greetingBL) // Use Dependency Injection
        {
            _greetingBL = greetingBL;
        }

        [HttpGet("GetGreetingUC1")]

        public IActionResult Get()
        {
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "API Endpoint Hit",
                Data = "UC1 Get Method call"
            };

            _logger.Info("Get Method Executed");
            return Ok(responseModel);
        }

        /// <summary>
        /// Get method to get the Greeting Message
        /// </summary>
        /// <returns>"Hello World"</returns>
        [HttpGet("GetGreetUC2")]

        public IActionResult GetGreet()
        {
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "API Endpoint Hit",
                Data = _greetingBL.GetGreetingMessage() // Call Business Layer method
            };

            _logger.Info("Get Method Executed");
            return Ok(responseModel);
        }

        /// <summary>
        /// Post method to accept a custom greeting message
        /// </summary>
        /// <param name="requestModel">Greeting message from user</param>
        /// <returns>Confirmation response</returns>
        [HttpPost]
        [Route("GreetingPost")]
        public IActionResult Post(RequestModel requestModel)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "API Endpoint Hit",
                Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}"
            };

            _logger.Info("Post Method Executed");
            return Ok(responseModel);
        }

        /// <summary>
        /// Put method to update a greeting message
        /// </summary>
        [HttpPut]
        [Route("GreetingPut")]
        public IActionResult Put(RequestModel requestModel)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "API Endpoint Hit in Put Method",
                Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}"
            };

            _logger.Info("Put Method Executed");
            return Ok(responseModel);
        }

        /// <summary>
        /// Patch method to partially update a greeting message
        /// </summary>
        [HttpPatch]
        [Route("GreetingPatch")]
        public IActionResult Patch(RequestModel requestModel)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "API Endpoint Hit",
                Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}"
            };

            _logger.Info("Patch Method Executed");
            return Ok(responseModel);
        }

        /// <summary>
        /// Delete method to remove a greeting message
        /// </summary>
        [HttpDelete]
        [Route("GreetingDelete")]
        public IActionResult Delete()
        {
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "API Endpoint Hit",
                Data = null
            };

            _logger.Info("Delete Method Executed");
            return Ok(responseModel);
        }
    }
}
