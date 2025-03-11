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
    public class AcerteGrupoController : BaseController, IDisposable
    {
        public AcerteGrupoController(IConfiguration configuration_) : base(configuration_)
        {
        }

        [HttpGet("{usuuni}")]
        public async Task<IActionResult> Get(Guid usuUni)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_GRUPO_S");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@USUUNI", usuUni);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            return await base.ExecuteDataReader();
        }


        [HttpPost()]
        public async Task<IActionResult> Post(AcerteGrupoFilter objAcerteGrupoFilter_)
        {
            Guid _gruUni;

            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_GRUPO_I");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@GRUUNI", objAcerteGrupoFilter_.GRUUNI);
            base.sqlParameter.Direction = ParameterDirection.Output;
            base.sqlParameter.Size = int.MaxValue;
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USUUNI", objAcerteGrupoFilter_.USUUNI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@GRUNOM", objAcerteGrupoFilter_.GRUNOM);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@GRUPUB", objAcerteGrupoFilter_.GRUPUB);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            try
            {
                await base.sqlCommand.ExecuteNonQueryAsync();

                if (Guid.TryParse(Convert.ToString(base.sqlCommand.Parameters["@GRUUNI"].Value), out _gruUni))
                {
                    objAcerteGrupoFilter_.GRUUNI = _gruUni;

                    return new ObjectResult(objAcerteGrupoFilter_);
                }
                else
                {
                    return new BadRequestObjectResult("Não foi possível criar o grupo.");
                }
            }
            catch (Exception _objException)
            {
                return new BadRequestObjectResult(_objException.Message);
            }
        }

        [HttpPatch()]
        public async Task<IActionResult> Patch(AcerteGrupoPatch objAcerteGrupoPatch_)
        {
            if (objAcerteGrupoPatch_.GRUMAN == 0)
            {
                return await ManutNome(objAcerteGrupoPatch_);
            }
            else
            {
                return await ManutPublico(objAcerteGrupoPatch_);
            }
        }

        private async Task<IActionResult> ManutNome(AcerteGrupoPatch objAcerteGrupoPatch_)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_GRUPO_GRUNOM_U");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@GRUUNI", objAcerteGrupoPatch_.GRUUNI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@GRUNOM", objAcerteGrupoPatch_.GRUNOM);
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

        private async Task<IActionResult> ManutPublico(AcerteGrupoPatch objAcerteGrupoPatch_)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_GRUPO_GRUPUB_U");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@GRUUNI", objAcerteGrupoPatch_.GRUUNI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@GRUPUB", objAcerteGrupoPatch_.GRUPUB);
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

    public class AcerteGrupoFilter
    {
        public Guid? GRUUNI { get; set; }
        public Guid USUUNI { get; set; }
        public string GRUNOM { get; set; }
        public bool GRUPUB { get; set; }
    }

    public class AcerteGrupoPatch
    {
        public int GRUMAN { get; set; }
        public Guid GRUUNI { get; set; }
        public string GRUNOM { get; set; }
        public bool GRUPUB { get; set; }
    }
}