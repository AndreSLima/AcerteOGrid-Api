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
    public class SeasonCalendarController : BaseController, IDisposable
    {
        public SeasonCalendarController(IConfiguration configuration_) : base(configuration_)
        {
        }

        [HttpGet("{temAno}")]
        public async Task<IActionResult> Get(int temAno)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_CALENDAR_SEASON");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@TEMANO", temAno);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            return await base.ExecuteDataReader();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}