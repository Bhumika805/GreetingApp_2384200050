using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using NLog;
using BusinessLayer.Interface;
using RepositoryLayer.Entity;

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

        ///<summary>
        ///Get Greet Message by Name
        ///</summary>
        ///<returns></returns>
        [HttpPost("GreetbyNameUC3")]
        public IActionResult GetGreetbyName(UserRequestModel request)
        {
            var message = _greetingBL.GetGreetingbyName(request);
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Greet message with Name",
                Data = message // Call Business Layer method
            };

            _logger.Info("Post method Executed with Name");
            return Ok(responseModel);

        }

        /// <summary>
        /// Handles the creation of a new greeting message.
        /// </summary>
        /// <param name="requestModel">The request containing the greeting message.</param>
        /// <returns>Returns a success response if the greeting is saved, or an error response if the input is invalid.</returns>
        [HttpPost("GreetingPostUC4")]
        public IActionResult SendGreeting(RequestModel requestModel)
        {
            ResponseModel<String> responseModel = new ResponseModel<string>();

            if (requestModel == null || string.IsNullOrWhiteSpace(requestModel.Value))
            {
                return BadRequest(new { Success = false, Message = "Invalid input. Message cannot be empty." });
            }

            var greeting = new Greeting { Message = requestModel.Value };
            var savedGreeting = _greetingBL.AddGreeting(greeting);


            responseModel.Success = true;
            responseModel.Message = "Greeting saved successfully.";
            responseModel.Data = savedGreeting.Message;
            _logger.Info("SendGreeting Method Executed Successfully");
            return Ok(responseModel);
        }

        ///<summary>
        ///Get Greeting by Id
        ///</summary>
        ///<param name = "id">Greeting ID</param>
        ///<returns>Greeting Message</returns>

        [HttpGet("GetGreetingByID_UC5/{id}")]
        public IActionResult GetGreetingById(int id)
        {
            var greetingmessage = _greetingBL.GetGreetingById(id);
            if (greetingmessage == null)
            {
                return NotFound(new { Success = false, Message = "Greeting not Found" });
            }

            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting received successfully",
                Data = greetingmessage // Call Business Layer method
            };
            _logger.Info("GetGreeting Method Executed Successfully");
            return Ok(responseModel);
        }

        ///<summary>
        ///Get All Greetings Data in a List in the Repository
        ///</summary>
        /// <returns>Returns a list of all stored greetings. 
        /// If no greetings are found, returns a 404 Not Found response.
        /// </returns>

        [HttpGet("GetGreetingList_UC6")]
        public IActionResult GetGreetingList()
        {
            var greetings = _greetingBL.GetGreetingList();
            if (greetings == null || greetings.Count == 0)
            {
                return NotFound(new {Success = false, Message = "No Greeting List"});
            }
            ResponseModel<List<Greeting>> responseModel = new ResponseModel<List<Greeting>>
            {
                Success = true,
                Message = "Greeting List Retrieved Successfully",
                Data = greetings
            };
            _logger.Info("GetGreeting Method Executed Successfully");
            return Ok(responseModel);
        }

        ///<summary>
        ///Patch Data - Edit Greetin message in the Reposiory 
        ///</summary>
        ///<returns>Returns a success response if the greeting is saved, or an error response if the input is invalid.
        ///</returns>

        [HttpPatch("UpdateGreeting_UC7/{id}")]
        public IActionResult UpdateGreetingMessage(int id, [FromBody] RequestModel requestModel)
        {
            var updatedGreeting = _greetingBL.UpdateGreetingMessage(id, requestModel.Value);

            if (updatedGreeting == null)
            {
                return NotFound(new { Success = false, Message = "Greeting not found" });
            }

            ResponseModel<Greeting> responseModel = new ResponseModel<Greeting>
            {
                Success = true,
                Message = "Greeting updated successfully",
                Data = updatedGreeting
            };

            _logger.Info("Patch method executed successfully");
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
                Data = $" Value: {requestModel.Value}"
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
