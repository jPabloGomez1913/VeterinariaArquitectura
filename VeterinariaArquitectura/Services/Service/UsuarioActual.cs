using System.Security.Claims;
using VeterinariaArquitectura.Services.IService;

namespace VeterinariaArquitectura.Services.Service
{
    public class UsuarioActual : IUsuarioActual
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioActual(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? Identificador
        {
            get
            {
                var usuario = _httpContextAccessor.HttpContext?.User;
                if (usuario?.Identity?.IsAuthenticated != true)
                {
                    return null;
                }

                return usuario.FindFirst(ClaimTypes.Email)?.Value
                    ?? usuario.Identity.Name
                    ?? usuario.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }
    }
}
