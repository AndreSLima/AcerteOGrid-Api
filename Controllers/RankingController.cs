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
    public class RankingController : BaseController, IDisposable
    {
        public RankingController(IConfiguration configuration_) : base(configuration_)
        {
        }

        [HttpPatch()]
        public async Task<IActionResult> Patch(FiltroRanking objFiltroRanking_)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTEO_RANKING_S");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@CATID", objFiltroRanking_.CATID);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@TEMANO", objFiltroRanking_.TEMANO);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@CALORD", objFiltroRanking_.CALORD);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@ROUND_OR_SEASON", objFiltroRanking_.ROUND_OR_SEASON);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            return await base.ExecuteDataReader();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }

    public class FiltroRanking
    {
        public int? CATID { get; set; }
        public int? TEMANO { get; set; }
        public int? CALORD { get; set; }
        public bool ROUND_OR_SEASON { get; set; }
    }
}