using ApiEliteWebAcceso.Application.DTOs.Aplicacion;
using ApiEliteWebAcceso.Application.Response;


namespace ApiEliteWebAcceso.Application.Contracts
{
    public interface IAplicacionService
    {

        Task<Result<List<AplicativoDto>>> GetAplicaciones();
        Task<Result<AplicativoDto>> GetAplicacionID(int idAplicacion);


        Task<Result<AplicativoDto>> CreateAplicacion(AplicativoDto createAplicacion);
        Task<Result<bool>> UpdateAplicacion(AplicativoDto updateAplicacion);
        Task<Result<bool>> DeleteAplicacion(int idAplicacion);

    }
}
