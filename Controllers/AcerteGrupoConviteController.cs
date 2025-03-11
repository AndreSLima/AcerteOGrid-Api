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
    public class AcerteGrupoConviteController : BaseController, IDisposable
    {
        public AcerteGrupoConviteController(IConfiguration configuration_) : base(configuration_)
        {
        }

        [HttpGet("{usuuni}/{sent}")]
        public async Task<IActionResult> Get(Guid usuUni, bool sent)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_GRUPO_CONVITE_S");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@USUUNI", usuUni);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@SENT", sent);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            return await base.ExecuteDataReader();
        }

        [HttpPost()]
        public async Task<IActionResult> Post(AcerteGrupoConviteFilter objAcerteGrupoConviteFilter_)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_GRUPO_CONVITE_I");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@GRUUNI", objAcerteGrupoConviteFilter_.GRUUNI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@GRUMAI", objAcerteGrupoConviteFilter_.GRUMAI);
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
        public async Task<IActionResult> Delete(AcerteGrupoConviteFilter objAcerteGrupoConviteFilter_)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_GRUPO_CONVITE_D");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@GRUUNI", objAcerteGrupoConviteFilter_.GRUUNI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@GRUMAI", objAcerteGrupoConviteFilter_.GRUMAI);
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

    public class AcerteGrupoConviteFilter
    {
        public Guid GRUUNI { get; set; }
        public string GRUMAI { get; set; }
        public Guid USUUNI { get; set; }
    }
}