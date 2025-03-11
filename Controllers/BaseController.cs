using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Data.SqlTypes;
using System.Data.SqlClient;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

namespace acerteapi.Controllers
{
    public abstract class BaseController : IDisposable
    {
        protected string currentConection = null;
        protected string stringConnection = null;

        protected StringBuilder stringBuilder = null;

        protected SqlConnection sqlConnection = null;
        protected SqlCommand sqlCommand = null;
        protected SqlDataReader sqlDataReader = null;
        protected SqlDataAdapter sqlDataAdapter = null;
        protected SqlParameter sqlParameter = null;

        public BaseController(IConfiguration configuration_)
        {
            currentConection = configuration_.GetSection("AppSettings:CURRENT_PROVIDER").Value;
            stringConnection = configuration_.GetSection(string.Concat("ConectionStrings:", currentConection, "_DATABASE")).Value;

            this.sqlConnection = new SqlConnection(this.stringConnection);
            this.sqlConnection.Open();
        }

        protected async Task<IActionResult> ExecuteDataReader()
        {
            try
            {
                this.sqlDataReader = await this.sqlCommand.ExecuteReaderAsync();

                if (this.sqlDataReader.HasRows)
                {
                    this.stringBuilder = new StringBuilder();

                    while (this.sqlDataReader.Read())
                    {
                        this.stringBuilder.Append(Convert.ToString(this.sqlDataReader.GetValue(0)));
                    }

                    var _objObject = JsonConvert.DeserializeObject<dynamic>(this.stringBuilder.ToString());

                    return new ObjectResult(_objObject);
                }
                else
                {
                    return new ObjectResult(null);
                }
            }
            catch (Exception _objException)
            {
                return new BadRequestObjectResult(_objException.Message);
            }
        }

        public virtual void Dispose()
        {
            if (this.sqlConnection.State == System.Data.ConnectionState.Open)
                this.sqlConnection.Close();

            this.currentConection = null;
            this.stringConnection = null;

            this.stringBuilder = null;

            this.sqlConnection = null;
            this.sqlCommand = null;
            this.sqlDataReader = null;
            this.sqlDataAdapter = null;
            this.sqlParameter = null;
        }
    }
}
