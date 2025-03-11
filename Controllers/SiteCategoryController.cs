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
    public class SiteCategoryController : BaseController, IDisposable
    {
        public SiteCategoryController(IConfiguration configuration_) : base(configuration_)
        {
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            base.sqlCommand = new SqlCommand("STP_AOG_SITE_CATEGORY_C");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = CommandType.StoredProcedure;

            return await base.ExecuteDataReader();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}