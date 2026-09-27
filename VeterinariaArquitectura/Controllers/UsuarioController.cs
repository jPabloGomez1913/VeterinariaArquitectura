using Microsoft.AspNetCore.Mvc;
using VeterinariaArquitectura.Models;
using VeterinariaArquitectura.Repositories.IRepository;
using VeterinariaArquitectura.ViewModels.Usuario;
using Microsoft.AspNetCore.Authorization;

namespace VeterinariaArquitectura.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Lista() => View(await _usuarioRepository.GetAll());

        [Authorize]
        [HttpGet]
        public IActionResult Crear() => View(new UsuarioVM());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(UsuarioVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var usuario = new Usuario
            {
                NombreCompleto = model.NombreCompleto,
                Correo = model.Correo,
                NumeroDocumento = model.NumeroDocumento,
                TipoDocumento = model.TipoDocumento,
                Clave = model.Clave
            };

            if (await _usuarioRepository.Create(usuario))
            {
                TempData["Mensaje"] = "Usuario creado correctamente.";
                return RedirectToAction(nameof(Lista));
            }

            ModelState.AddModelError(string.Empty, "No se pudo crear el usuario.");
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Actualizar(int id)
        {
            var usuario = await _usuarioRepository.Get(id);
            if (usuario == null) return NotFound();

            return View(new UsuarioEdicionVM
            {
                UsuarioId = usuario.UsuarioId,
                NombreCompleto = usuario.NombreCompleto,
                Correo = usuario.Correo,
                NumeroDocumento = usuario.NumeroDocumento,
                TipoDocumento = usuario.TipoDocumento
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Actualizar(UsuarioEdicionVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var usuario = await _usuarioRepository.Get(model.UsuarioId);
            if (usuario == null) return NotFound();

            usuario.NombreCompleto = model.NombreCompleto;
            usuario.Correo = model.Correo;
            usuario.NumeroDocumento = model.NumeroDocumento;
            usuario.TipoDocumento = model.TipoDocumento;
            if (!string.IsNullOrWhiteSpace(model.Clave)) usuario.Clave = model.Clave;

            if (await _usuarioRepository.Update(usuario))
            {
                TempData["Mensaje"] = "Usuario actualizado correctamente.";
                return RedirectToAction(nameof(Lista));
            }

            ModelState.AddModelError(string.Empty, "No se pudo actualizar el usuario.");
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuario = await _usuarioRepository.Get(id);
            return usuario == null ? NotFound() : View(usuario);
        }

        [HttpPost, ActionName(nameof(Eliminar))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var usuario = await _usuarioRepository.Get(id);
            if (usuario == null) return NotFound();

            TempData["Mensaje"] = await _usuarioRepository.Delete(usuario)
                ? "Usuario eliminado correctamente."
                : "No se pudo eliminar el usuario.";
            return RedirectToAction(nameof(Lista));
        }

        // Se conserva la ruta anterior para enlaces existentes.
        [HttpGet]
        public IActionResult Registrarse() => RedirectToAction(nameof(Crear));
    }
}
