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
    public class RankingReportController : BaseController, IDisposable
    {
        public RankingReportController(IConfiguration configuration_) : base(configuration_)
        {
        }

        [HttpPatch()]
        public async Task<IActionResult> Patch(FiltroRankingReport objFiltroRankingReport_)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTEO_GRID_PODIO_RANKING_R");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@CATID", objFiltroRankingReport_.CATID);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@TEMANO", objFiltroRankingReport_.TEMANO);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@CALORD", objFiltroRankingReport_.CALORD);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@CALUNI", objFiltroRankingReport_.CALUNI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@TEMID", objFiltroRankingReport_.TEMID);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            return await base.ExecuteDataReader();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }

    public class FiltroRankingReport
    {
        public int? CATID { get; set; }
        public int? TEMANO { get; set; }
        public int? CALORD { get; set; }
        public Guid? CALUNI { get; set; }
        public int? TEMID { get; set; }
    }
}