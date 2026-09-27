using VeterinariaArquitectura.Common;
using VeterinariaArquitectura.Models;
using VeterinariaArquitectura.Repositories.IRepository;
using VeterinariaArquitectura.Services.IService;
using VeterinariaArquitectura.ViewModels.Mascota;

namespace VeterinariaArquitectura.Services.Service
{
    public class MascotaService : IMascotaService
    {
        private readonly IMascotaRepository _repository;

        public MascotaService(IMascotaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<Mascota>> Crear(MascotaDTO mascotaDTO)
        {
            Mascota mascota = new Mascota()
            {
                Nombre = mascotaDTO.Nombre,
                FechaNacimiento = mascotaDTO.FechaNacimiento,
                NombrePropietario = mascotaDTO.NombrePropietario,
                NumeroPropietario = mascotaDTO.NumeroPropietario,
                CorreoPropietario = mascotaDTO.CorreoPropietario,
            };
            await _repository.Create(mascota);
            var creado = await _repository.Guardar();
            if (creado)
            {
                return new Response<Mascota>
                {
                    Exitoso = true,
                    Mensaje = "Mascota creada exitosamente",
                    Data = mascota
                };
            }
            return new Response<Mascota>
            {
                Mensaje = "La mascota no pudo ser creada"
            };


        }

        public async Task<Response<Mascota>> Editar(EditarMascotaDTO editarMascotaDTO)
        {
            var mascota = await _repository.Get(editarMascotaDTO.MascotaId);
            if (mascota == null)
            {

                return new Response<Mascota>
                {
                    Mensaje = "No se pudo obtener la mascota"

                };
            }
            mascota.NombrePropietario = editarMascotaDTO.NombrePropietario;
            mascota.NumeroPropietario = editarMascotaDTO.NumeroPropietario;
            mascota.CorreoPropietario = editarMascotaDTO.CorreoPropietario;
            mascota.Nombre = editarMascotaDTO.Nombre;

            _repository.Update(mascota);
            var editado = await _repository.Guardar();
            if (editado)
            {
                return new Response<Mascota>
                {
                    Exitoso = true,
                    Mensaje = "Mascota editada exitosamente",
                    Data = mascota
                };
            }
            return new Response<Mascota>
            {
                Mensaje = "La mascota no pudo ser editada"
            };

        }

        public async Task<Response> Eliminar(int mascotaId)
        {
            var mascota = await _repository.Get(mascotaId);
            if (mascota == null)
            {
                return new Response
                {
                    Mensaje = "La mascota no fue encontrada"
                };
            }
            _repository.Delete(mascota);
            var eliminado = await _repository.Guardar();
            if (eliminado)
            {
                return new Response
                {
                    Exitoso = true,
                    Mensaje = "Mascota eliminada exitosamente",
                };
            }
            return new Response
            {
                Mensaje = "La mascota no pudo ser eliminada"
            };
        }

        public async Task<Response<Mascota?>> Get(int mascotaId)
        {
            var mascota = await _repository.Get(mascotaId);
            return new Response<Mascota?>
            {
                Exitoso = true,
                Data = mascota
            };
        }

        public async Task<Response<EditarMascotaDTO>> GetEditar(int mascotaId)
        {
            var editarMascotaDto = await _repository.GetActualizar(mascotaId);
            if (editarMascotaDto == null)
            {
                return new Response<EditarMascotaDTO>
                {
                    Mensaje = "El registro no existe"
                };
            }

            return new Response<EditarMascotaDTO>
            {
                Exitoso = true,
                Data = editarMascotaDto
            };
        }

        public async Task<Response<ICollection<Mascota>>> Listar()
        {
            var lista = await _repository.GetAll();
            return new Response<ICollection<Mascota>>
            {
                Exitoso = true,
                Data = lista

            };
        }

    }
}
