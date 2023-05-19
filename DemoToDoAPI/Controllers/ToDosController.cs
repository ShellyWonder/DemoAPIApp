
using DemoAPIClassLibrary.Models;
using DemoAPIClassLibrary.SQLDataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DemoToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly IToDoData _toDoData;
        


        public ToDosController(IToDoData data)
        {
            _toDoData = data;
        }
        // GET: api/ToDos
        [HttpGet]
        public async Task<ActionResult<List<ToDoModel>>> Get()
        {
            var userIdText = User.Claims.FirstOrDefault(c =>
                               c.Type == ClaimTypes.NameIdentifier)?.Value;
           
            if (userIdText == null)
            {
                return Unauthorized();  
            }


           var userId = int.Parse(userIdText);
            
          var output =  await _toDoData.GetAllAssigned(userId);
            return Ok(output);
        }

        // GET api/ToDos/5
        [HttpGet("{todoid}")]
        public async Task<ActionResult<ToDoModel>>Get(int todoId)
        {
            var userIdText = User.Claims.FirstOrDefault(c =>
                               c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userIdText == null)
            {
                return Unauthorized();
            }


            var userId = int.Parse(userIdText);
            var output = await _toDoData.GetOneAssigned(userId, todoId);
            return Ok(output);
        }

        // POST api/ToDos
        [HttpPost]
        public async Task<ActionResult<ToDoModel>> Post([FromBody] string task)
        {
            var userIdText = User.Claims.FirstOrDefault(c =>
                              c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userIdText == null)
            {
                return Unauthorized();
            }


            var userId = int.Parse(userIdText);
            var output = await _toDoData.Create(userId, task);
            return Ok(output);
        }
        //PUT = Update
        // PUT api/ToDos/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }
        //PUT = Update
        // PUT api/ToDos/5/Complete
        [HttpPut("{id}/Complete")]
        public IActionResult Complete(int id)
        {
            throw new NotImplementedException();
        }

        // DELETE api/ToDos/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
