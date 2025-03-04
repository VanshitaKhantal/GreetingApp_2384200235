using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entity;
using ModelLayer.Model;
using NLog;

namespace HelloGreetingApplication.Controllers
{
    /// <summary>
    /// Class Providing API for HelloGreeting
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HelloGreetingController : ControllerBase
    {
        private readonly IGreetingBL _greetingBL;

        /// <summary>
        /// Logger instance for capturing logs
        /// </summary>
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor to initialize the controller with Greeting Business Logic Layer.
        /// </summary>
        /// <param name="greetingBL">Instance of IGreetingBL for handling greetings.</param>
        public HelloGreetingController(IGreetingBL greetingBL)
        {
            _greetingBL = greetingBL;
        }

        /// <summary>
        /// Add Post method to get greeting message
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns>greeting message</returns>
        [HttpPost]
        [Route("save-greeting")]
        public IActionResult SaveGreeting(UserEntity userEntity)
        {
            try
            {
                _greetingBL.SaveGreeting(userEntity.Message);
                return Ok(new { Success = true, Message = "Greeting saved successfully" });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error saving greeting");
                return StatusCode(500, new { Success = false, Message = "An error occurred while saving the greeting" });
            }
        }

        /// <summary>
        /// Default GET method to retrieve a greeting message.
        /// </summary>
        /// <returns>JSON response with a greeting message.</returns>
        [HttpGet]
        [Route("get-personalized-greeting")]
        public IActionResult GetPersonalizedGreeting(string? firstName, string? lastName)
        {
            logger.Info($"GET request received for personalized greeting with FirstName: {firstName}, LastName: {lastName}");

            string message = _greetingBL.GetGreeting(firstName, lastName);

            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Greeting generated successfully",
                Data = message
            };

            return Ok(responseModel);
        }


        /// <summary>
        /// Get Method to get the greeting message
        /// </summary>
        /// <returns>"Hello, World!"</returns>
        [HttpGet]
        [Route("get-greeting")]
        public IActionResult Get()
        {
            logger.Info("GET request received for greeting.");

            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Hello to Greeting App API Endpoint";
            responseModel.Data = "Hello, World!";
            return Ok(responseModel);
        }

        /// <summary>
        /// Post method to receive request successfully
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>response model</returns>
        [HttpPost]
        [Route("post-greeting")]
        public IActionResult Post(RequestModel requestModel)
        {
            logger.Info($"POST request received with Key: {requestModel.Key}, Value: {requestModel.Value}");

            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Request received successfully";
            responseModel.Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}";
            return Ok(responseModel);
        }

        /// <summary>
        /// Put method to receive request successfully
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>response model</returns>
        [HttpPut]
        [Route("update-greeting")]
        public IActionResult Put(RequestModel requestModel)
        {
            logger.Info($"PUT request received. Updating greeting to: {requestModel.Value}");

            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Greeting updated successfully";
            responseModel.Data = requestModel.Value;
            return Ok(responseModel);
        }

        /// <summary>
        /// Patch method to receive request successfully
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>response model</returns>
        [HttpPatch]
        [Route("modify-greeting")]
        public IActionResult Patch(RequestModel requestModel)
        {
            logger.Info($"PATCH request received. Modifying greeting with: {requestModel.Value}");

            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Greeting updated successfully";
            responseModel.Data = requestModel.Value;
            return Ok(responseModel);
        }

        /// <summary>
        /// Delete Method to remove the greeting message
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>response model</returns>
        [HttpDelete]
        [Route("delete-greeting")]
        public IActionResult Delete(RequestModel requestModel)
        {
            logger.Info($"DELETE request received. Removing greeting for key: {requestModel.Key}");

            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Greeting deleting successfully";
            responseModel.Data = string.Empty;
            return Ok(responseModel);

        }
    }
}





