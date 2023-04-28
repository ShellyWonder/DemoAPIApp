
using DemoAPIClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;


namespace DemoToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        // GET: api/ToDos
        [HttpGet]
        public ActionResult<IEnumerable<ToDoModel>> Get()
        {
            return Ok();
            throw new NotImplementedException();
        }

        // GET api/ToDos/5
        [HttpGet("{id}")]
        public ActionResult<ToDoModel>Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/ToDos
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            throw new NotImplementedException();
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
