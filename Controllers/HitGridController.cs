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
    public class HitGridController : BaseController, IDisposable
    {
        List<AcerteGrid> objAcerteGridList = null;
        AcerteGrid objAcerteGrid = null;

        public HitGridController(IConfiguration configuration_) : base(configuration_)
        {
        }

        [HttpGet("{mail}/{temano}/{calord}")]
        public async Task<IActionResult> Get(string mail, int temAno, int calord)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_GRID_S");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@MAIL", mail);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@TEMANO", temAno);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@CALORD", calord);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlDataReader = await base.sqlCommand.ExecuteReaderAsync();

            if (base.sqlDataReader.HasRows)
            {
                this.objAcerteGridList = new List<AcerteGrid>();

                while (base.sqlDataReader.Read())
                {
                    this.objAcerteGrid = new AcerteGrid();

                    this.objAcerteGrid.userId = Convert.ToInt32(base.sqlDataReader["USERID"]);
                    this.objAcerteGrid.mail = Convert.ToString(base.sqlDataReader["MAIL"]);
                    this.objAcerteGrid.temAno = Convert.ToInt32(base.sqlDataReader["TEMANO"]);
                    this.objAcerteGrid.calOrd = Convert.ToInt32(base.sqlDataReader["CALORD"]);
                    this.objAcerteGrid.pilIdG1 = Convert.ToInt32(base.sqlDataReader["PILIDG1"]);
                    this.objAcerteGrid.pilIdG2 = Convert.ToInt32(base.sqlDataReader["PILIDG2"]);
                    this.objAcerteGrid.pilIdG3 = Convert.ToInt32(base.sqlDataReader["PILIDG3"]);
                    this.objAcerteGrid.pilIdP1 = Convert.ToInt32(base.sqlDataReader["PILIDP1"]);
                    this.objAcerteGrid.pilIdP2 = Convert.ToInt32(base.sqlDataReader["PILIDP2"]);
                    this.objAcerteGrid.pilIdP3 = Convert.ToInt32(base.sqlDataReader["PILIDP3"]);
                    this.objAcerteGrid.g1Pnt = Convert.ToInt32(base.sqlDataReader["G1PNT"]);
                    this.objAcerteGrid.g2Pnt = Convert.ToInt32(base.sqlDataReader["G2PNT"]);
                    this.objAcerteGrid.g3Pnt = Convert.ToInt32(base.sqlDataReader["G3PNT"]);
                    this.objAcerteGrid.p1Pnt = Convert.ToInt32(base.sqlDataReader["P1PNT"]);
                    this.objAcerteGrid.p2Pnt = Convert.ToInt32(base.sqlDataReader["P2PNT"]);
                    this.objAcerteGrid.p3Pnt = Convert.ToInt32(base.sqlDataReader["P3PNT"]);

                    this.objAcerteGridList.Add(this.objAcerteGrid);
                }
            }

            return new ObjectResult(this.objAcerteGridList);
        }

        [HttpPost()]
        public async Task<IActionResult> Post(AcerteGrid objAcerteGrid_)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_GRID_I");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@MAIL", objAcerteGrid_.mail);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@TEMANO", objAcerteGrid_.temAno);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@CALORD", objAcerteGrid_.calOrd);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@PILIDG1", objAcerteGrid_.pilIdG1);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@PILIDG2", objAcerteGrid_.pilIdG2);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@PILIDG3", objAcerteGrid_.pilIdG3);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@PILIDP1", objAcerteGrid_.pilIdP1);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@PILIDP2", objAcerteGrid_.pilIdP2);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@PILIDP3", objAcerteGrid_.pilIdP3);
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

        [HttpPut()]
        public async Task<IActionResult> Put(AcerteGrid objAcerteGrid_)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_GRID_U");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@MAIL", objAcerteGrid_.mail);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@TEMANO", objAcerteGrid_.temAno);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@CALORD", objAcerteGrid_.calOrd);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@PILIDG1", objAcerteGrid_.pilIdG1);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@PILIDG2", objAcerteGrid_.pilIdG2);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@PILIDG3", objAcerteGrid_.pilIdG3);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@PILIDP1", objAcerteGrid_.pilIdP1);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@PILIDP2", objAcerteGrid_.pilIdP2);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@PILIDP3", objAcerteGrid_.pilIdP3);
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
            this.objAcerteGridList = null;
            this.objAcerteGrid = null;

            base.Dispose();
        }
    }

    public class AcerteGrid
    {
        public int userId { get; set; }
        public string mail { get; set; }
        public int temAno { get; set; }
        public int calOrd { get; set; }
        public int pilIdG1 { get; set; }
        public int pilIdG2 { get; set; }
        public int pilIdG3 { get; set; }
        public int pilIdP1 { get; set; }
        public int pilIdP2 { get; set; }
        public int pilIdP3 { get; set; }
        public int g1Pnt { get; set; }
        public int g2Pnt { get; set; }
        public int g3Pnt { get; set; }
        public int p1Pnt { get; set; }
        public int p2Pnt { get; set; }
        public int p3Pnt { get; set; }
    }
}
