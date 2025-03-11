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
    public class AcerteUsuarioController : BaseController, IDisposable
    {
        public AcerteUsuarioController(IConfiguration configuration_) : base(configuration_)
        {
        }

        [HttpGet("{usuuni}")]
        public async Task<IActionResult> Get(string usuuni)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_USUARIO_S");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@USUUNI", usuuni);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            return await base.ExecuteDataReader();
        }

        [HttpPost()]
        public async Task<IActionResult> Post(AcerteUsuarioFilter objAcerteUsuarioFilter_)
        {
            Guid _usuUni;

            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_USUARIO_I");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@USUUNI", objAcerteUsuarioFilter_.USUUNI);
            base.sqlParameter.Direction = ParameterDirection.Output;
            base.sqlParameter.Size = int.MaxValue;
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USUNOM", objAcerteUsuarioFilter_.USUNOM);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USUMAI", objAcerteUsuarioFilter_.USUMAI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USUNIC", objAcerteUsuarioFilter_.USUNIC);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USURNK", objAcerteUsuarioFilter_.USURNK);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USURECNOT", objAcerteUsuarioFilter_.USURECNOT);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USURECMAI", objAcerteUsuarioFilter_.USURECMAI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            try
            {
                await base.sqlCommand.ExecuteNonQueryAsync();

                if (Guid.TryParse(Convert.ToString(base.sqlCommand.Parameters["@USUUNI"].Value), out _usuUni))
                {
                    objAcerteUsuarioFilter_.USUUNI = _usuUni;

                    return new ObjectResult(objAcerteUsuarioFilter_);
                }
                else
                {
                    return new BadRequestObjectResult("Não foi possível criar/obter o usuário.");
                }
            }
            catch (Exception _objException)
            {
                return new BadRequestObjectResult(_objException.Message);
            }
        }

        [HttpPut()]
        public async Task<IActionResult> Put(AcerteUsuarioFilter objAcerteUsuarioFilter_)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_USUARIO_U");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@USUUNI", objAcerteUsuarioFilter_.USUUNI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USUNIC", objAcerteUsuarioFilter_.USUNIC);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USURNK", objAcerteUsuarioFilter_.USURNK);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USURECNOT", objAcerteUsuarioFilter_.USURECNOT);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USURECMAI", objAcerteUsuarioFilter_.USURECMAI);
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

        [HttpPatch]
        public async Task<IActionResult> Patch(AcerteUsuarioPatch objAcerteUsuarioPatch_)
        {
            if (objAcerteUsuarioPatch_.USUMAN == 0)
            {
                return await ManutApelido(objAcerteUsuarioPatch_);
            }
            else if (objAcerteUsuarioPatch_.USUMAN == 1)
            {
                return await ManutNotificacao(objAcerteUsuarioPatch_);
            }
            else if (objAcerteUsuarioPatch_.USUMAN == 2)
            {
                return await ManutEMail(objAcerteUsuarioPatch_);
            }
            else
            {
                return await ManutGrupoAcerteOGrid(objAcerteUsuarioPatch_);
            }
        }

        [HttpDelete("{usumai}")]
        public async Task<IActionResult> Delete(string usumai)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_USUARIO_D");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


            base.sqlParameter = new SqlParameter("@USUMAI", usumai);
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

        private async Task<IActionResult> ManutApelido(AcerteUsuarioPatch objAcerteUsuarioPatch_)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_USUARIO_USUNIC_U");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@USUUNI", objAcerteUsuarioPatch_.USUUNI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USUNIC", objAcerteUsuarioPatch_.USUNIC);
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

        private async Task<IActionResult> ManutNotificacao(AcerteUsuarioPatch objAcerteUsuarioPatch_)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_USUARIO_USURECNOT_U");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@USUUNI", objAcerteUsuarioPatch_.USUUNI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USURECNOT", objAcerteUsuarioPatch_.USURECNOT);
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

        private async Task<IActionResult> ManutEMail(AcerteUsuarioPatch objAcerteUsuarioPatch_)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_USUARIO_USURECMAI_U");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@USUUNI", objAcerteUsuarioPatch_.USUUNI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USURECMAI", objAcerteUsuarioPatch_.USURECMAI);
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

        private async Task<IActionResult> ManutGrupoAcerteOGrid(AcerteUsuarioPatch objAcerteUsuarioPatch_)
        {
            base.sqlCommand = new SqlCommand("STP_AOG_API_ACERTE_USUARIO_USURNK_U");

            base.sqlCommand.Connection = base.sqlConnection;
            base.sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            base.sqlParameter = new SqlParameter("@USUUNI", objAcerteUsuarioPatch_.USUUNI);
            base.sqlCommand.Parameters.Add(base.sqlParameter);

            base.sqlParameter = new SqlParameter("@USURNK", objAcerteUsuarioPatch_.USURNK);
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

    public class AcerteUsuarioFilter
    {
        public Guid? USUUNI { get; set; }
        public string USUNOM { get; set; }
        public string USUMAI { get; set; }
        public string USUNIC { get; set; }
        public bool USURNK { get; set; }
        public bool USURECNOT { get; set; }
        public bool USURECMAI { get; set; }
    }

    public class AcerteUsuarioPatch
    {
        public int USUMAN { get; set; }
        public Guid USUUNI { get; set; }
        public string USUNIC { get; set; }
        public bool USURNK { get; set; }
        public bool USURECNOT { get; set; }
        public bool USURECMAI { get; set; }
    }
}