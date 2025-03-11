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
    public class SiteSeasonController : BaseController, IDisposable
    {
        public SiteSeasonController(IConfiguration configuration_) : base(configuration_)
        {
        }

        [HttpGet("{catid}")]
        public async Task<IActionResult> Get(int catid)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_SITE_SEASON_C");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@CATID", catid);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            return await base.ExecuteDataReader();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}