using System.Collections.Generic; 
using Microsoft.AspNetCore.Mvc;   
using DAPM.ClientApi.Models;   
using DAPM.ClientApi.Services.Interfaces; 

namespace DAPM.ClientApi.Controllers 
{
    // Defining a new API Controller named `UserController`.
    [Route("api/[controller]")]  // Specifies the base route for the controller as `api/User`.
    [ApiController]              // Marks this class as an API controller to enable HTTP routing and response formatting.
    public class UserController : ControllerBase
    {
        // Private read-only field for the user service, used for handling user data operations.
        private readonly IUserService _userService;

        // Constructor to inject the `IUserService` dependency into the controller.
        public UserController(IUserService userService)
        {
            _userService = userService; // Assign the injected service to the local field.
        }

        // HTTP GET endpoint to retrieve a user by their ID.
        // Example usage: GET `api/User/1`
        [HttpGet("{userId}")]  // Binds the `userId` from the URL to this method's parameter.
        public ActionResult<User> GetUserById(int userId)
        {
            // Call the service to get a user by the provided ID.
            var user = _userService.GetUserById(userId);

            // If no user is found, return a 404 Not Found response.
            if (user == null) 
                return NotFound("User not found");

            // Return the user data along with a 200 OK response.
            return Ok(user);
        }

        // HTTP GET endpoint to retrieve all users.
        // Example usage: GET `api/User`
        [HttpGet] // No additional route parameters; this method is called when the base route `api/User` is accessed.
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            // Call the service to get all users.
            var users = _userService.GetAllUsers();

            // Return the list of users along with a 200 OK response.
            return Ok(users);
        }
    }
}
