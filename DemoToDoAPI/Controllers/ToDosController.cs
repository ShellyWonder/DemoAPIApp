
 using Microsoft.AspNetCore.Mvc;


namespace DemoToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        // GET: api/ToDos
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/ToDos/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/ToDos
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        //PUT = Update
        // PUT api/ToDos/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/ToDos/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
