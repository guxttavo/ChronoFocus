namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly AppSettings _appSettings;

        public LoginController(IUsuarioRepository usuarioRepository, AppSettings appSettings)
        {
            _usuarioRepository = usuarioRepository;
            _appSettings = appSettings;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Authenticate(Usuario model)
        {
            var usuario = await _usuarioRepository.Get(model.Login, model.Senha);

            if (usuario == null)
                return NotFound("Usuário ou senha inválidos");

            var jwtToken = TokenService.GenerateToken(usuario, _appSettings.Chave.Segredo);

            Response.Cookies.Append("JwtToken", jwtToken);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("JwtToken");

            return RedirectToAction("Index", "Login");
        }
    }
}