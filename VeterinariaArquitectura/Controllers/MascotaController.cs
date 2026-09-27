using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinariaArquitectura.Models;
using VeterinariaArquitectura.Repositories.IRepository;
using VeterinariaArquitectura.ViewModels.Mascota;

namespace VeterinariaArquitectura.Controllers
{
    public class MascotaController : Controller
    {
        private readonly IMascotaRepository _mascotaRepository;

        public MascotaController(IMascotaRepository mascotaRepository)
        {
            _mascotaRepository = mascotaRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult RegistrarMascota()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarMascota(MascotaVM model)
        {
            Mascota mascota = new Mascota()
            {
                Nombre = model.Nombre,
                FechaNacimiento = model.FechaNacimiento,
                NombrePropietario = model.NombrePropietario,
                NumeroPropietario = model.NumeroPropietario,
                CorreoPropietario = model.CorreoPropietario,
            };

            var usuarioCreado = await _mascotaRepository.Create(mascota);
            if (usuarioCreado)
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
            var mascotas = await _mascotaRepository.GetAll();
            return View(mascotas);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Actualizar(int id)
        {
            var mascota = await _mascotaRepository.Get(id);
            if (mascota == null)
            {
                return RedirectToAction("Lista", "Mascota");
            }

            return View(mascota);
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(Mascota model)
        {
            var actualizado = await _mascotaRepository.Update(model);
            if (actualizado)
            {
                return RedirectToAction("Lista", "Mascota");
            }

            ViewData["Mensaje"] = "No se pudo actualizar la mascota";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var mascota = await _mascotaRepository.Get(id);
            if (mascota == null)
            {
                return RedirectToAction("Lista", "Mascota");
            }
            await _mascotaRepository.Delete(mascota);
            return RedirectToAction("Lista", "Mascota");
        }
    }
}