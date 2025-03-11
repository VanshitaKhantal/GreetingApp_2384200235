using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using Middleware.GlobalExceptionHandler;
using ModelLayer.Model;
using NLog;
using RepositoryLayer.Entity;

namespace HelloGreetingApplication.Controllers
{
    /// <summary>
    /// API Controller for managing greetings.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HelloGreetingController : ControllerBase
    {
        private readonly IGreetingBL _greetingBL;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor to initialize the controller with Greeting Business Logic Layer.
        /// </summary>
        /// <param name="greetingBL">Instance of IGreetingBL for handling greetings.</param>
        public HelloGreetingController(IGreetingBL greetingBL)
        {
            _greetingBL = greetingBL;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveGreeting(int userId, [FromBody] GreetingEntity greeting)
        {
            if (greeting == null || string.IsNullOrWhiteSpace(greeting.Message))
                return BadRequest(new { Success = false, Message = "Invalid input data" });

            try
            {
                bool isSaved = await _greetingBL.SaveGreeting(userId, greeting.Message); // Use await here
                if (!isSaved)
                    return NotFound(new { Success = false, Message = "User not found" });

                return Ok(new { Success = true, Message = "Greeting saved successfully" });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error saving greeting.");
                return StatusCode(500, new { Success = false, Message = "An error occurred while saving the greeting" });
            }
        }



        /// <summary>
        /// Retrieve a greeting by ID.
        /// </summary>
        [HttpGet("get/{id}")]
        public IActionResult GetGreetingById(int id)
        {
            try
            {
                var greeting = _greetingBL.GetGreetingsByUserId(id);
                if (greeting == null)
                    return NotFound(new { Success = false, Message = "Greeting not found" });

                return Ok(new { Success = true, Message = "Greeting retrieved successfully", Data = greeting });
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error retrieving greeting for ID: {id}");
                return StatusCode(500, ExceptionHandler.CreateErrorResponse(ex, logger));
            }
        }

        /// <summary>
        /// Retrieve all greetings.
        /// </summary>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllGreetings()
        {
            try
            {
                var greetings = await _greetingBL.GetAllGreetings(); // ? Await the async method

                if (greetings == null || !greetings.Any()) // ? Use Any() instead of Count == 0
                    return NotFound(new { Success = false, Message = "No greetings found" });

                return Ok(new { Success = true, Message = "Greetings retrieved successfully", Data = greetings });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error retrieving all greetings.");
                return StatusCode(500, ExceptionHandler.CreateErrorResponse(ex, logger));
            }
        }


        /// <summary>
        /// Update an existing greeting.
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateGreeting(int id, string newMessage)
        {
            try
            {
                var isUpdated = await _greetingBL.UpdateGreeting(id, newMessage); // ? Use await

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
        /// Delete a greeting by ID.
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteGreetingById(int id)
        {
            try
            {
                bool isDeleted = await _greetingBL.DeleteGreeting(id); // ? Use await for async method

                if (!isDeleted)
                    return NotFound(new { Success = false, Message = "Greeting not found" });

                return Ok(new { Success = true, Message = "Greeting deleted successfully" });
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error deleting greeting for ID: {id}");
                return StatusCode(500, ExceptionHandler.CreateErrorResponse(ex, logger));
            }
        }


        /// <summary>
        /// Get a personalized greeting.
        /// </summary>
        [HttpGet("personalized")]
        public IActionResult GetPersonalizedGreeting([FromQuery] string? firstName, [FromQuery] string? lastName)
        {
            try
            {
                logger.Info($"Generating personalized greeting for {firstName} {lastName}");
                string message = _greetingBL.GetGreeting(firstName, lastName);

                return Ok(new ResponseModel<string>
                {
                    Success = true,
                    Message = "Greeting generated successfully",
                    Data = message
                });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error generating personalized greeting.");
                return StatusCode(500, ExceptionHandler.CreateErrorResponse(ex, logger));
            }
        }

        /// <summary>
        /// Default greeting.
        /// </summary>
        [HttpGet("default")]
        public IActionResult GetDefaultGreeting()
        {
            try
            {
                return Ok(new ResponseModel<string>
                {
                    Success = true,
                    Message = "Hello to Greeting App API Endpoint",
                    Data = "Hello, World!"
                });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error getting default greeting.");
                return StatusCode(500, ExceptionHandler.CreateErrorResponse(ex, logger));
            }
        }

        /// <summary>
        /// Process a generic request.
        /// </summary>
        [HttpPost("process")]
        public IActionResult Post([FromBody] RequestModel requestModel)
        {
            try
            {
                logger.Info($"Processing request: {requestModel.Key} - {requestModel.Value}");
                return Ok(new ResponseModel<string>
                {
                    Success = true,
                    Message = "Request received successfully",
                    Data = $"{requestModel.Key}: {requestModel.Value}"
                });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error processing request.");
                return StatusCode(500, ExceptionHandler.CreateErrorResponse(ex, logger));
            }
        }

        /// <summary>
        /// Modify a greeting using PATCH.
        /// </summary>
        [HttpPatch("modify")]
        public IActionResult Patch([FromBody] RequestModel requestModel)
        {
            try
            {
                logger.Info($"Modifying greeting with: {requestModel.Value}");
                return Ok(new ResponseModel<string>
                {
                    Success = true,
                    Message = "Greeting modified successfully",
                    Data = requestModel.Value
                });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error modifying greeting.");
                return StatusCode(500, ExceptionHandler.CreateErrorResponse(ex, logger));
            }
        }

        /// <summary>
        /// Delete a greeting using DELETE.
        /// </summary>
        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] RequestModel requestModel)
        {
            try
            {
                logger.Info($"Deleting greeting for key: {requestModel.Key}");
                return Ok(new ResponseModel<string>
                {
                    Success = true,
                    Message = "Greeting deleted successfully",
                    Data = string.Empty
                });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error deleting greeting.");
                return StatusCode(500, ExceptionHandler.CreateErrorResponse(ex, logger));
            }
        }
    }
}
