using System.Net;

namespace Sales.WEB.Repository
{
    public class HttpResponseMessages<T>
    {
        public HttpResponseMessages(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Error = error;
            Response = response;
            HttpResponseMessage = httpResponseMessage;
        }

        public bool Error { get; set; }
        public T? Response { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }


        public async Task<string?> GetErrorMessageAsync()
        {
            if (!Error)
            {
                return null;
            }

            var statusCode = HttpResponseMessage.StatusCode;
            if (statusCode == HttpStatusCode.NotFound)
            {
                return "Recurso no encontrado";
            }
            else if (statusCode == HttpStatusCode.BadRequest)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            else if (statusCode == HttpStatusCode.Unauthorized)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            else if (statusCode == HttpStatusCode.Forbidden)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            else if(statusCode == HttpStatusCode.Conflict)
            {
                return "Ya existe un recurso con este nombre";
            }

            return await HttpResponseMessage.Content.ReadAsStringAsync();
        }
    }
}

