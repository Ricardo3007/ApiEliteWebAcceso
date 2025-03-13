using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.DTOs.Empresa;
using ApiEliteWebAcceso.Application.DTOs.Login;
using ApiEliteWebAcceso.Application.DTOs.Menu;
using ApiEliteWebAcceso.Application.Resource;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using ApiEliteWebAcceso.Infrastructure.Services;

namespace ApiEliteWebAcceso.Application.Services
{
    public class EmpresaService : IEmpresaService
    {

        private readonly IConfiguration _config;
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IConfiguration config, IEmpresaRepository authRepository)
        {
            _config = config;
            _empresaRepository = authRepository;
        }

        public async Task<Result<GrupoEmpresaDto>> CreateGrupoEmpresa(GrupoEmpresaDto createGrupoEmpresa)
        {
            // Llamar al método del repositorio para crear el grupo de empresa
            var newGrupoEmpresa = await _empresaRepository.CreateGrupoEmpresa(createGrupoEmpresa);

            // Mapear el resultado a GrupoEmpresaDto
            var result = new GrupoEmpresaDto
            {
                idGrupoEmpresaDTO = newGrupoEmpresa.idGrupoEmpresaDTO,
                nombreGrupoDTO = newGrupoEmpresa.nombreGrupoDTO,
                estadoDTO = newGrupoEmpresa.estadoDTO
            };

            return Result<GrupoEmpresaDto>.Success(result);
        }

        public async Task<Result<bool>> DeleteGrupoEmpresa(int idGrupoEmpresa)
        {
            var isDeleted = await _empresaRepository.DeleteGrupoEmpresa(idGrupoEmpresa);
            // Devolver el ID del grupo de empresa eliminado si se eliminó correctamente, de lo contrario devolver -1
            return  Result<bool>.Success(isDeleted);
        }

        public async Task<Result<List<GrupoEmpresaDto>>> GetGrupoEmpresa()
        {
            try
            {


                List<ACC_GRUPO_EMPRESAS> grupoEmpresa = await _empresaRepository.GetGrupoEmpresa();

                List<GrupoEmpresaDto> grupoEmpresaDto = new();

                foreach (var group in grupoEmpresa)
                {
                    // Crear el elemento raíz para cada NombreAplicativo
                    var rootItem = new GrupoEmpresaDto
                    {
                        idGrupoEmpresaDTO = group.PK_GRUPO_EMPRESA_C, // Nombre del aplicativo como raíz
                        nombreGrupoDTO = group.NOMBRE_GRUPO_C, // Ícono genérico para aplicativos
                        estadoDTO = group.ESTADO_C
                    };

                    grupoEmpresaDto.Add(rootItem);

                }
                    return Result<List<GrupoEmpresaDto>>.Success(grupoEmpresaDto);
            }
            catch (Exception ex)
            {
                return Result<List<GrupoEmpresaDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<GrupoEmpresaDto>> GetGrupoEmpresaID(int idEmpresa)
        {
            try
            {


                ACC_GRUPO_EMPRESAS grupoEmpresa = await _empresaRepository.GetGrupoEmpresaID(idEmpresa);

                GrupoEmpresaDto grupoEmpresaDto =  new GrupoEmpresaDto
                {
                    idGrupoEmpresaDTO = grupoEmpresa.PK_GRUPO_EMPRESA_C, // Nombre del aplicativo como raíz
                    nombreGrupoDTO = grupoEmpresa.NOMBRE_GRUPO_C, // Ícono genérico para aplicativos
                    estadoDTO = grupoEmpresa.ESTADO_C
                };
                


                return Result<GrupoEmpresaDto>.Success(grupoEmpresaDto);
            }
            catch (Exception ex)
            {
                return Result<GrupoEmpresaDto>.Failure(ex.Message);
            }
        }

        public async Task<Result<GrupoEmpresaDto>> UpdateGrupoEmpresa(GrupoEmpresaDto updateGrupoEmpresa)
        {
            // Llamar al método del repositorio para actualizar el grupo de empresa
            var updatedGrupoEmpresa = await _empresaRepository.UpdateGrupoEmpresa(updateGrupoEmpresa);

            // Mapear el resultado a GrupoEmpresaDto
            var result = new GrupoEmpresaDto
            {
                idGrupoEmpresaDTO = updatedGrupoEmpresa.idGrupoEmpresaDTO,
                nombreGrupoDTO = updatedGrupoEmpresa.nombreGrupoDTO,
                estadoDTO = updatedGrupoEmpresa.estadoDTO
            };

            return Result<GrupoEmpresaDto>.Success(result);  
        }
    }
}
