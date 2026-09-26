using Microsoft.AspNetCore.Mvc;
using VeterinariaArquitectura.Models;
using VeterinariaArquitectura.Repositories.IRepository;
using VeterinariaArquitectura.ViewModels.Usuario;

namespace VeterinariaArquitectura.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        [HttpGet]
        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(UsuarioVM model)
        {
            if (model.Clave != model.ConfirmarClave)
            {
                ViewData["Mensaje"] = "Las contraseñas deben coincidir";
                return View();
            }

            Usuario usuario = new Usuario()
            {
                NombreCompleto = model.NombreCompleto,
                Correo = model.Correo,
                Clave = model.Clave,
                TipoDocumento = model.TipoDocumento,
                NumeroDocumento = model.NumeroDocumento,
            };

            var usuarioCreado = await _usuarioRepository.Create(usuario);
            if (usuarioCreado)
            {
                return RedirectToAction("Login", "Usuario");
            }

            ViewData["Mensaje"] = "No se puedo crear el usuario";
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var usuario = await _usuarioRepository.Existe(model.Correo, model.Clave);
            if (usuario == null)
            {
                ViewData["Mensaje"] = "Usuario o contraseña incorrecta";
                return View();

            }
            return RedirectToAction("Index", "Home");
        }
    }
}
