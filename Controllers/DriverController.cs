using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace acerteapi.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : BaseController, IDisposable
    {
        public DriverController(IConfiguration configuration_) : base(configuration_)
        {
        }

        [HttpGet("{pilId}")]
        public async Task<IActionResult> Get(int pilId)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_FATO_DRIVER");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@PILID", pilId);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            return await base.ExecuteDataReader();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}