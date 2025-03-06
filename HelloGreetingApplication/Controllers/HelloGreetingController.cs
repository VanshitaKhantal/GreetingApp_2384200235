using Microsoft.AspNetCore.Mvc;
using Middleware.GlobalExceptionHandler;
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

        ///// <summary>
        ///// Ilogger instance for capturing logs
        ///// </summary>
        //private Microsoft.Extensions.Logging.ILogger _logger;

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
                var errorResponse = ExceptionHandler.CreateErrorResponse(ex, logger);
                return StatusCode(500, new { Success = false, Message = "An error occurred while saving the greeting" });
            }
        }

        /// <summary>
        /// Retrieves a greeting message by its ID.
        /// </summary>
        /// <param name="id">The unique ID of the greeting.</param>
        /// <returns>The greeting message if found.</returns>
        [HttpGet]
        [Route("get-greeting-by-id")]
        public IActionResult GetGreetingById(int id)
        {
            try
            {
                var greeting = _greetingBL.GetGreetingById(id);
                if (greeting == null)
                {
                    return NotFound(new { Success = false, Message = "Greeting not found" });
                }

                return Ok(new { Success = true, Message = "Greeting retrieved successfully", Data = greeting });
            }
            catch (Exception ex)
            {
                var errorResponse = ExceptionHandler.CreateErrorResponse(ex, logger);
                return StatusCode(500, new { Success = false, Message = "An error occurred while retrieving the greeting" });
            }
        }

        [HttpGet]
        [Route("get-all-greetings")]
        public IActionResult GetAllGreetings()
        {
            try
            {
                var greetings = _greetingBL.GetAllGreetings();
                if (greetings == null || greetings.Count == 0)
                {
                    return NotFound(new { Success = false, Message = "No greetings found" });
                }

                return Ok(new { Success = true, Message = "Greetings retrieved successfully", Data = greetings });
            }
            catch (Exception ex)
            {
                var errorResponse = ExceptionHandler.CreateErrorResponse(ex, logger);
                return StatusCode(500, new { Success = false, Message = "An error occurred while retrieving the greetings" });
            }
        }

        /// <summary>
        /// Updates an existing greeting message.
        /// </summary>
        /// <param name="id">The ID of the greeting.</param>
        /// <param name="newMessage">The new greeting message.</param>
        /// <returns>Success or failure message.</returns>
        [HttpPut]
        [Route("updategreeting")]
        public IActionResult UpdateGreeting(int id, string newMessage)
        {
            try
            {
                var isUpdated = _greetingBL.UpdateGreeting(id, newMessage);
                if (!isUpdated)
                {
                    return NotFound(new { Success = false, Message = "Greeting not found" });
                }

                return Ok(new { Success = true, Message = "Greeting updated successfully" });
            }
            catch (Exception ex)
            {
                var errorResponse = ExceptionHandler.CreateErrorResponse(ex, logger);
                return StatusCode(500, new { Success = false, Message = "An error occurred while updating the greeting" });
            }
        }

        /// <summary>
        /// Deletes a greeting message by its ID.
        /// </summary>
        /// <param name="id">The ID of the greeting to delete.</param>
        /// <returns>Success or failure message.</returns>
        [HttpDelete]
        [Route("delete-greeting-by-id")]
        public IActionResult DeleteGreetingById(int id)
        {
            try
            {
                var isDeleted = _greetingBL.DeleteGreeting(id);
                if (!isDeleted)
                {
                    return NotFound(new { Success = false, Message = "Greeting not found" });
                }

                return Ok(new { Success = true, Message = "Greeting deleted successfully" });
            }
            catch (Exception ex)
            {
                var errorResponse = ExceptionHandler.CreateErrorResponse(ex, logger);
                return StatusCode(500, new { Success = false, Message = "An error occurred while deleting the greeting" });
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
            try
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
            catch (Exception ex)
            {
                var errorResponse = ExceptionHandler.CreateErrorResponse(ex, logger);
                return StatusCode(500, new { Success = false, Message = "An error occurred while deleting the greeting" });
            }
        }


        /// <summary>
        /// Get Method to get the greeting message
        /// </summary>
        /// <returns>"Hello, World!"</returns>
        [HttpGet]
        [Route("get-greeting")]
        public IActionResult Get()
        {
            try
            {
                logger.Info("GET request received for greeting.");

                ResponseModel<string> responseModel = new ResponseModel<string>();
                responseModel.Success = true;
                responseModel.Message = "Hello to Greeting App API Endpoint";
                responseModel.Data = "Hello, World!";
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                var errorResponse = ExceptionHandler.CreateErrorResponse(ex, logger);
                return StatusCode(500, new { Success = false, Message = "An error occurred while deleting the greeting" });
            }
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
            try
            {
                logger.Info($"POST request received with Key: {requestModel.Key}, Value: {requestModel.Value}");

                ResponseModel<string> responseModel = new ResponseModel<string>();
                responseModel.Success = true;
                responseModel.Message = "Request received successfully";
                responseModel.Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}";
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                var errorResponse = ExceptionHandler.CreateErrorResponse(ex, logger);
                return StatusCode(500, new { Success = false, Message = "An error occurred while deleting the greeting" });
            }
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
            try
            {

                logger.Info($"PUT request received. Updating greeting to: {requestModel.Value}");

                ResponseModel<string> responseModel = new ResponseModel<string>();
                responseModel.Success = true;
                responseModel.Message = "Greeting updated successfully";
                responseModel.Data = requestModel.Value;
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                var errorResponse = ExceptionHandler.CreateErrorResponse(ex, logger);
                return StatusCode(500, new { Success = false, Message = "An error occurred while deleting the greeting" });
            }
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
            try
            {
                logger.Info($"PATCH request received. Modifying greeting with: {requestModel.Value}");

                ResponseModel<string> responseModel = new ResponseModel<string>();
                responseModel.Success = true;
                responseModel.Message = "Greeting updated successfully";
                responseModel.Data = requestModel.Value;
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                var errorResponse = ExceptionHandler.CreateErrorResponse(ex, logger);
                return StatusCode(500, new { Success = false, Message = "An error occurred while deleting the greeting" });
            }
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
            try
            {
                logger.Info($"DELETE request received. Removing greeting for key: {requestModel.Key}");

                ResponseModel<string> responseModel = new ResponseModel<string>();
                responseModel.Success = true;
                responseModel.Message = "Greeting deleting successfully";
                responseModel.Data = string.Empty;
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                var errorResponse = ExceptionHandler.CreateErrorResponse(ex, logger);
                return StatusCode(500, new { Success = false, Message = "An error occurred while deleting the greeting" });
            }

        }
    }
}





