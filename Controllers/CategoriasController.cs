using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TopSecretNicaAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly string _connectionString;

        public CategoriasController()
        {
        }
    }
}
