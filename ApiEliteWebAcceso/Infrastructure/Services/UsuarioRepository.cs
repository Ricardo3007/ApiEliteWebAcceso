using ApiEliteWebAcceso.Application.DTOs.Usuario;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using Dapper;
using System.Data;

namespace ApiEliteWebAcceso.Infrastructure.Services
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _dbConnection;

        public UsuarioRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<UsuarioDto> CreateUsuario(UsuarioDto createAplicativo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUsuario(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ACC_PERMISO_USUARIO_EMPRESA>> GetPermisoUsuarioEmpresaID(int idUsuario ,int idGrupoEmpresa,bool isSuperAdmin)
        {

            var sqlQuery = @"
                             SELECT 
                                    EMP.PK_EMPRESA_C, 
                                    EMP.NOMBRE_EMPRESA_C, 
	                                GR.PK_GRUPO_EMPRESA_C,
	                                GR.NOMBRE_GRUPO_C,
                                    CASE 
                                        WHEN MAX(PER.PK_PERMISO_USUARIO_C) IS NULL THEN 0 
                                        ELSE 1 
                                    END AS TIENE_PERMISO -- INDICA SI EL USUARIO TIENE EL PERMISO (1) O NO (0)
                                FROM ACC_EMPRESA EMP 
		                                INNER JOIN ACC_GRUPO_EMPRESAS GR ON GR.PK_GRUPO_EMPRESA_C = EMP.FK_GRUPO_EMPRESA_C
		                                LEFT JOIN ACC_PERMISO_USUARIO PER 
                                    ON EMP.PK_EMPRESA_C = PER.FK_EMPRESA_C 
                                    AND PER.FK_USUARIO_C = @PK_USUARIO_C
                                WHERE 1 = 1
                                        AND (@isSuperAdmin = 1 OR EMP.FK_GRUPO_EMPRESA_C = @PK_GRUPO_EMPRESA_C)
                                GROUP BY EMP.PK_EMPRESA_C, EMP.NOMBRE_EMPRESA_C,GR.PK_GRUPO_EMPRESA_C,GR.NOMBRE_GRUPO_C
                                ORDER BY EMP.NOMBRE_EMPRESA_C";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                PK_USUARIO_C = idUsuario, // ID del permiso de empresa a buscar
                isSuperAdmin = isSuperAdmin ? 1 : 0,
                PK_GRUPO_EMPRESA_C = idGrupoEmpresa
            };

            // Ejecutar la consulta y obtener el registro
            var permisoMenu = await _dbConnection.QueryAsync<ACC_PERMISO_USUARIO_EMPRESA>(sqlQuery, parameters);

            // Convertir el resultado a una lista y retornarla
            return permisoMenu.ToList();
        }

        public async Task<List<ACC_PERMISO_USUARIO_DETALLE>> GetPermisoUsuarioID(int idUsuario, int idGrupoEmpresa)
        {
            // Crear la consulta SQL para obtener todos los registros
            var sqlQuery = @"
                            SELECT PK_PERMISO_USUARIO_C,FK_USUARIO_C, FK_OPCION_MENU_C,MENU.DESCRIPCION_C,
		                            FK_EMPRESA_C,EMP.NOMBRE_EMPRESA_C,EMP.FK_GRUPO_EMPRESA_C,APL.INICIALES_APLICATIVO_C,
		                            APL.NOMBRE_APLICATIVO_C,APL.ORDEN_C
                            FROM            ACC_PERMISO_USUARIO PERUSU
				                            INNER JOIN ACC_MENU_ELITE MENU ON MENU.PK_OPCION_MENU_C = PERUSU.FK_OPCION_MENU_C 
				                            INNER JOIN ACC_EMPRESA  EMP ON EMP.PK_EMPRESA_C = PERUSU.FK_EMPRESA_C 
				                            INNER JOIN ACC_APLICACION APL ON APL.PK_APLICATIVO_C = MENU.FK_APLICATIVO_C 
                            WHERE FK_USUARIO_C = @PK_USUARIO_C AND FK_GRUPO_EMPRESA_C = @FK_GRUPO_EMPRESA_C ";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                PK_USUARIO_C = idUsuario, // ID del permiso de empresa a buscar
                FK_GRUPO_EMPRESA_C = idGrupoEmpresa, // ID del permiso de empresa a buscar
            };

            // Ejecutar la consulta y obtener el registro
            var permisoMenu = await _dbConnection.QueryAsync<ACC_PERMISO_USUARIO_DETALLE>(sqlQuery, parameters);

            // Convertir el resultado a una lista y retornarla
            return permisoMenu.ToList();
        }

        public async Task<List<ACC_USUARIO>> GetUsuario()
        {
            // Crear la consulta SQL para obtener todos los registros
            var sqlQuery = @"
                            SELECT [PK_USUARIO_C]
                                  ,[USUARIO_C]
                                  ,[NOMBRE_USUARIO_C]
                                  ,[FK_TDI_C]
                                  ,[ID_USUARIO_C]
                                  ,[PASSWORD_C]
                                  ,[MAIL_USUARIO_C]
                                  ,[ESTADO_C]
                                  ,[TIPO_USUARIO_C]
                              FROM [dbo].[ACC_USUARIO]";

            // Ejecutar la consulta y mapear los resultados a una lista de ACC_USUARIO
            var usuario = await _dbConnection.QueryAsync<ACC_USUARIO>(sqlQuery);

            // Convertir el resultado a una lista y retornarla
            return usuario.ToList();
        }

        public async Task<ACC_USUARIO> GetUsuarioID(int idUsuario)
        {
            // Crear la consulta SQL para obtener todos los registros
            var sqlQuery = @"
                            SELECT [PK_USUARIO_C]
                                  ,[USUARIO_C]
                                  ,[NOMBRE_USUARIO_C]
                                  ,[FK_TDI_C]
                                  ,[ID_USUARIO_C]
                                  ,[PASSWORD_C]
                                  ,[MAIL_USUARIO_C]
                                  ,[ESTADO_C]
                                  ,[TIPO_USUARIO_C]
                              FROM [dbo].[ACC_USUARIO]  WHERE PK_USUARIO_C = @PK_USUARIO_C";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                PK_USUARIO_C = idUsuario // ID del permiso de empresa a buscar
            };

            // Ejecutar la consulta y obtener el registro
            var usuario = await _dbConnection.QueryFirstOrDefaultAsync<ACC_USUARIO>(sqlQuery, parameters);

            // Retornar el registro encontrado (puede ser null si no se encuentra)
            return usuario;
        }




        public Task<bool> UpdateUsuario(UsuarioDto updateAplicativo)
        {
            throw new NotImplementedException();
        }
    }
}
