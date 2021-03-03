using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueueIbm;

namespace Queue_IbmMq_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueController : Controller
    {
        private readonly IIbmQueue queue;

        public QueueController(IIbmQueue queue)
        {
            this.queue = queue;
        }

        [HttpPost("WriteMenssage")]
        public ActionResult WriteMenssage(string menssage)
        {
            queue.Write(menssage);

            return Ok();
        }

        [HttpGet("ReadOneMenssage")]
        public ActionResult ReadOneMenssage()
        {
            var result = queue.ReadOneMensage();

            return Ok(result);
        }
        
        [HttpGet("ReadManyMenssage")]
        public ActionResult ReadManyMenssage()
        {
            var result = queue.ReadManyMensage();

            return Ok(result);
        }
    }
}
