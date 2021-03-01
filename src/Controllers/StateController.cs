using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MoodStateApi.Models;
using MoodStateApi.Services;

namespace MoodStateApi.Controllers {
    [Route("state")]
    [Produces("application/json")]
    [EnableCors("DefaultPolicy")]
    [ApiController]
    public class StateController : ControllerBase {
        private readonly IService service;

        public StateController(IService service) {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<string> Get() {
            return Ok(service.Get());
        }

        [HttpPost]
        public ActionResult<string> Update([FromBody] RequestModel state) {
            return Ok(service.Update(state.State));
        }

        [HttpOptions]
        public ActionResult Options() {
            return Ok();
        }
    }
}