using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Bank.API.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {  
        private ClientService _service { get; set; }
        public ClientController()
        {
            _service = new ClientService();
        }

        [HttpGet]
        public async Task<ActionResult<Client>> GetClient(Guid clientId)
        {
            return Ok(await _service.GetClientAsync(clientId));
        }

        [HttpPost]
        public async Task<IActionResult> PostClient(Client client)
        {
            await _service.AddClientAsync(client);
            return Ok("Клиент успешно добавлен");
        }

        [HttpPut]
        public async Task<IActionResult> PutClient(Client client)
        {
            await _service.UpdateClientAsync(client);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(Guid clientId)
        {
            await _service.DeleteClientAsync(clientId);
            return Ok();
        }
    }
}

