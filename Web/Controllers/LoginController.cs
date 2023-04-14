namespace Web.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly AppSettings _appSettings;
        public LoginController(
            IUsuarioRepository usuarioRepository,
            AppSettings appSettings
        )
        {
            _usuarioRepository = usuarioRepository;
            _appSettings = appSettings;
        }

        [HttpGet("index")]
        public IActionResult Index() => View();

        [HttpPost("auth")]
        public async Task<ActionResult> Authenticate(Usuario model)
        {
            var usuario = await _usuarioRepository.Get(model.Login, model.Senha);

            if (usuario == null)
                return NotFound("Usuário ou senha inválidos");
                
            var token = TokenService.GenerateToken(usuario, _appSettings.Chave.Segredo);
            
            return View("../Home/Index");
        }
    }
}
