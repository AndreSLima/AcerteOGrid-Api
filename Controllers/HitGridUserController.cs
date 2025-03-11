using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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
    public class HitGridUserController : BaseController, IDisposable
    {
        //List<AcerteGridUsuario> objAcerteGridUsuarioList = null;
        //AcerteGridUsuario objAcerteGridUsuario = null;

        public HitGridUserController(IConfiguration configuration_) : base(configuration_)
        {
        }

        [HttpPost()]
        public async Task<IActionResult> Post(AcerteGridUsuario objAcerteGridUsuario_)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_GRID_USER_I");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@MAIL", objAcerteGridUsuario_.mail);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@NOME", objAcerteGridUsuario_.nome);
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
            //this.objAcerteGridUsuarioList = null;
            //this.objAcerteGridUsuario = null;

            base.Dispose();
        }
    }

    public class AcerteGridUsuario
    {
        public int userId { get; set; }
        public string mail { get; set; }
        public string nome { get; set; }
    }
}