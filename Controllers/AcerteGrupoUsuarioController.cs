using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

namespace acerteapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AcerteGrupoUsuarioController : BaseController, IDisposable
    {
        public AcerteGrupoUsuarioController(IConfiguration configuration_) : base(configuration_)
        {
        }

        [HttpGet("{usuuni}")]
        public async Task<IActionResult> Get(Guid usuUni)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_GRUPO_USUARIO_S");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@USUUNI", usuUni);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            return await base.ExecuteDataReader();
        }

        [HttpPost()]
        public async Task<IActionResult> Post(AcerteGrupoUsuarioFilter objAcerteGrupoUsuarioFilter_)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_GRUPO_USUARIO_I");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@GRUUNI", objAcerteGrupoUsuarioFilter_.GRUUNI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USUUNI", objAcerteGrupoUsuarioFilter_.USUUNI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USUOWN", objAcerteGrupoUsuarioFilter_.USUOWN);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@ADDID", objAcerteGrupoUsuarioFilter_.ADDID);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            try
            {
                await base.sqlCommand.ExecuteNonQueryAsync();

                return new OkResult();
            }
            catch (Exception _objException)
            {
                return new BadRequestObjectResult(_objException.Message);
            }
        }

        [HttpDelete()]
        public async Task<IActionResult> Delete(AcerteGrupoUsuarioFilter objAcerteGrupoUsuarioFilter_)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_GRUPO_USUARIO_D");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@GRUUNI", objAcerteGrupoUsuarioFilter_.GRUUNI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USUUNI", objAcerteGrupoUsuarioFilter_.USUUNI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            try
            {
                await base.sqlCommand.ExecuteNonQueryAsync();

                return new OkResult();
            }
            catch (Exception _objException)
            {
                return new BadRequestObjectResult(_objException.Message);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }

    public class AcerteGrupoUsuarioFilter
    {
        public Guid GRUUNI { get; set; }
        public Guid USUUNI { get; set; }
        public bool USUOWN { get; set; }
        public bool ADDID { get; set; }
    }
}