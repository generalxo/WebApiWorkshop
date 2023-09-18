using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using WebApiWorkshop.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiWorkshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Test Users
        private User[] users = new User[]
        {
            new User("Peter", "Panda", 1),
            new User("John", "Doe", 2),
            new User("Jane", "Doe", 3)
        };

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return users;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(NotFoundResult), Description = "No user with that id was found")]
        public ActionResult<User> Get(int id)
        {
            foreach (var item in users)
            {
                if (id == item.Id)
                {
                    return item;
                }
            }

            return StatusCode((int)HttpStatusCode.NotFound);
        }

        // POST api/<UserController> 
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResult), Description = "Invalid Body for User")]
        public ActionResult<User> Post([FromBody] User newUser)
        {
            if (newUser.FirstName.Length > 0 && newUser.LastName.Length > 0 && newUser.Id > 0)
            {
                return newUser;
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResult), Description = "Invalid Body for User")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(NotFoundResult), Description = "No user with that id was found")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(int), Description = "User was updated")]
        public ActionResult<User> Put([FromBody] User newUser)
        { // This putmethod has some logic issues since we are not using a database, but it is for demonstration purposes
            if (newUser.FirstName.Length > 0 && newUser.LastName.Length > 0)
            {
                foreach (var item in users)
                {
                    if (item.Id == newUser.Id)
                    {
                        users[newUser.Id].FirstName = newUser.FirstName;
                        users[newUser.Id].LastName = newUser.LastName;
                        return StatusCode((int)HttpStatusCode.OK, users[newUser.Id]);
                    }
                }
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            return StatusCode((int)HttpStatusCode.BadRequest);

        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(NotFoundResult), Description = "No user with that id was found")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(User), Description = "User was deleted")]
        public ActionResult<User> Delete(int id)
        {// This putmethod has some logic issues since we are not using a database, but it is for demonstration purposes
            foreach (var item in users)
            {
                if (item.Id == id)
                {
                    item.FirstName = "We would remove";
                    item.LastName = "this item from db";
                    return StatusCode((int)HttpStatusCode.OK);
                }
            }
            return StatusCode((int)HttpStatusCode.NotFound);
        }
    }
}
