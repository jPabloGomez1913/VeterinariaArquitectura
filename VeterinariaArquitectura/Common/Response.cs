namespace VeterinariaArquitectura.Common
{

    public class Response
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; } = string.Empty;

        public static Response Respuesta(string mensaje = "Operación exitosa")
        {
            return new Response
            {
                Exitoso = false,
                Mensaje = mensaje,
            };
        }
    }
    public class Response<T> : Response
    {
        public T? Data { get; set; }

        public static Response<T> Respuesta(T datos, string mensaje = "Operación exitosa")
        {
            return new Response<T>
            {
                Exitoso = false,
                Mensaje = mensaje,
                Data = datos
            };
        }
    }


}
