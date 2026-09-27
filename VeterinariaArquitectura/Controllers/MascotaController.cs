using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinariaArquitectura.Services.IService;
using VeterinariaArquitectura.ViewModels.Mascota;

namespace VeterinariaArquitectura.Controllers
{
    public class MascotaController : Controller
    {
        private readonly IMascotaService _mascotaService;

        public MascotaController(IMascotaService mascotaService)
        {
            _mascotaService = mascotaService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult RegistrarMascota()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarMascota(MascotaDTO model)
        {

            var usuarioCreado = await _mascotaService.Crear(model);
            if (usuarioCreado.Exitoso)
            {
                return RedirectToAction("Lista", "Mascota");
            }

            ViewData["Mensaje"] = "No se puedo crear la mascota";
            return View();
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var mascotas = await _mascotaService.Listar();
            return View(mascotas.Data);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Actualizar(int id)
        {
            var mascota = await _mascotaService.GetEditar(id);
            if (!mascota.Exitoso)
            {
                return RedirectToAction("Lista", "Mascota");
            }

            return View(mascota.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(EditarMascotaDTO model)
        {


            var actualizado = await _mascotaService.Editar(model);
            if (actualizado.Exitoso)
            {
                return RedirectToAction("Lista", "Mascota");
            }

            ViewData["Mensaje"] = "No se pudo actualizar la mascota";
            return View(actualizado.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {

            await _mascotaService.Eliminar(id);
            return RedirectToAction("Lista", "Mascota");
        }
    }
}