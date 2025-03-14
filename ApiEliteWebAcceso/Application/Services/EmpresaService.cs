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

        public async Task<Result<EmpresaDto>> CreateEmpresa(EmpresaDto createGrupoEmpresa)
        {
            return Result<EmpresaDto>.Success(await _empresaRepository.CreateEmpresa(createGrupoEmpresa));
        }


        public async Task<Result<bool>> DeleteEmpresa(int idEmpresa)
        {
            return Result<bool>.Success(await _empresaRepository.DeleteEmpresa(idEmpresa));
        }


        public async Task<Result<bool>> UpdateEmpresa(EmpresaDto updateEmpresa)
        {
            return Result<bool>.Success(await _empresaRepository.UpdateEmpresa(updateEmpresa));
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

        public async Task<Result<List<EmpresaDto>>> GetEmpresa()
        {
            try
            {
                // Obtener la lista de empresas desde el repositorio
                var empresas = await _empresaRepository.GetEmpresa();

                // Mapear la lista de ACC_EMPRESA a EmpresaDto
                var empresasDto = empresas.Select(e => new EmpresaDto
                {
                    idEmpresaDTO = e.PK_EMPRESA_C,
                    nombreDTO = e.NOMBRE_EMPRESA_C,
                    grupoEmpresaDTO = e.FK_GRUPO_EMPRESA_C,
                    idEmpresaC_DTO = e.ID_EMPRESA_C,
                    logoDTO = e.LOGO_EMPRESA_C,
                    nombreBdDTO = e.NOMBRE_BD_C,
                    servidorBdDTO = e.SERVIDOR_BD_C,
                    usuarioBdDTO = e.USUARIO_BD_C,
                    passwordBdDTO = e.PASSWORD_BD_C,
                    estadoDTO = e.ESTADO_C,
                    cadenaConexionDTO = $"Server={e.SERVIDOR_BD_C};Database={e.NOMBRE_BD_C};User Id={e.USUARIO_BD_C};Password={e.PASSWORD_BD_C};"
                }).ToList();

                return Result<List<EmpresaDto>>.Success(empresasDto);
            }
            catch (Exception ex)
            {
                return Result<List<EmpresaDto>>.Failure(ex.Message);
            }
        }


        public async Task<Result<EmpresaDto>> GetEmpresaID(int idEmpresa)
        {
            try
            {
                var empresa = await _empresaRepository.GetEmpresaID(idEmpresa);

                if (empresa == null)
                {
                    return Result<EmpresaDto>.Failure("Empresa no encontrada.");
                }

                var empresaDto = new EmpresaDto
                {
                    idEmpresaDTO = empresa.PK_EMPRESA_C,
                    nombreDTO = empresa.NOMBRE_EMPRESA_C,
                    grupoEmpresaDTO = empresa.FK_GRUPO_EMPRESA_C,
                    idEmpresaC_DTO = empresa.ID_EMPRESA_C,
                    logoDTO = empresa.LOGO_EMPRESA_C,
                    nombreBdDTO = empresa.NOMBRE_BD_C,
                    servidorBdDTO = empresa.SERVIDOR_BD_C,
                    usuarioBdDTO = empresa.USUARIO_BD_C,
                    passwordBdDTO = empresa.PASSWORD_BD_C,
                    estadoDTO = empresa.ESTADO_C,
                    cadenaConexionDTO = $"Server={empresa.SERVIDOR_BD_C};Database={empresa.NOMBRE_BD_C};User Id={empresa.USUARIO_BD_C};Password={empresa.PASSWORD_BD_C};"
                };

                return Result<EmpresaDto>.Success(empresaDto);
            }
            catch (Exception ex)
            {
                return Result<EmpresaDto>.Failure(ex.Message);
            }
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
