using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEliteWebAcceso.Application.Response
{
    public class Result<T>
    {
        public bool Flag { get; set; }              //Bandera que define si la validación fue exitosa o no.
        public int Status { get; set; }             //Tipo de estado para la respuesta: 200=OK, 400=BadRequest, 404=NotFound, 500=InternalServerFailure
        public string Message { get; set; }         //Mensaje de error o exito
        public List<string> Failures { get; set; }  //Mensaje de error o exito
        public T Data { get; set; }                 //Cualquier tipo de información

        public Result(int status, bool flag, string message, List<string> errors, T data)
        {
            Status = status;
            Flag = flag;
            Message = message;
            Failures = errors;
            Data = data;
        }

        /// <summary>
        /// Crea una instancia de <see cref="Result{T}"/> con un estado de éxito (200 OK).
        /// </summary>
        /// <param name="data">Los datos a incluir en la respuesta.</param>
        /// <param name="message">El mensaje de éxito. El valor predeterminado es "Operation successful".</param>
        /// <returns>Una instancia de <see cref="Result{T}"/> con el estado de éxito.</returns>
        /// <author>Ricardo Ferrer</author>
        public static Result<T> Success(T data, string message = "Operation successful")
        {
            return new Result<T>(200, true, message, new List<string>(), data);
        }

        /// <summary>
        /// Crea una instancia de <see cref="Result{T}"/> con un estado de "No Content" (204 No Content).
        /// </summary>
        /// <param name="message">El mensaje de la respuesta. El valor predeterminado es "No content".</param>
        /// <returns>Una instancia de <see cref="Result{T}"/> con el estado "No Content".</returns>
        /// <author>Ricardo Ferrer</author>
        public static Result<T> NotContent(string message = "No content")
        {
            return new Result<T>(204, true, message, new List<string>(), default);
        }

        /// <summary>
        /// Crea una instancia de <see cref="Result{T}"/> con un estado de error especificado.
        /// </summary>
        /// <param name="message">El mensaje de error.</param>
        /// <param name="status">El código de estado HTTP del error. El valor predeterminado es 500 (Internal Server Failure).</param>
        /// <returns>Una instancia de <see cref="Result{T}"/> con el estado de error.</returns>
        /// <author>Ricardo Ferrer</author>
        public static Result<T> Failure(string message, int status = 500)
        {
            return new Result<T>(status, false, message, new List<string>(), default);
        }

        /// <summary>
        /// Crea una instancia de <see cref="Result{T}"/> con un estado de "Not Found" (404 Not Found).
        /// </summary>
        /// <param name="message">El mensaje de error. El valor predeterminado es "Resource not found".</param>
        /// <returns>Una instancia de <see cref="Result{T}"/> con el estado "Not Found".</returns>
        /// <author>Ricardo Ferrer</author>
        public static Result<T> NotFound(string message = "Resource not found")
        {
            return new Result<T>(404, false, message, new List<string>(), default);
        }

        /// <summary>
        /// Crea una instancia de <see cref="Result{T}"/> con un estado de "Bad Request" (400 Bad Request).
        /// </summary>
        /// <param name="message">El mensaje de error. El valor predeterminado es "Bad request".</param>
        /// <returns>Una instancia de <see cref="Result{T}"/> con el estado "Bad Request".</returns>
        /// <author>Ricardo Ferrer</author>
        public static Result<T> BadRequest(List<string> errors, string message = "Bad request")
        {
            return new Result<T>(400, false, message, errors, default);
        }



        /// <summary>
        /// GetHttpResponse
        /// </summary>
        /// <param name=""></param>
        /// <author>Ricardo Ferrer</author>
        /// <returns></returns>
        public IActionResult GetHttpResponse()
        {
            return Status switch
            {
                200 => new OkObjectResult(new { result = Data, IsSuccessful = Flag, Message, Failures }),
                201 => new CreatedResult("", new { result = Data, IsSuccessful = Flag, Message, Failures }),
                204 => new NoContentResult(),
                401 => new UnauthorizedResult(),
                404 => new NotFoundObjectResult(new { Data, IsSuccessful = Flag, Message, Failures }),
                500 => new ObjectResult(new { Message, Failures }) { StatusCode = StatusCodes.Status500InternalServerError },
                _ => new BadRequestObjectResult(new { Data, IsSuccessful = Flag, Message, Failures })
            };
        }
    }
}
