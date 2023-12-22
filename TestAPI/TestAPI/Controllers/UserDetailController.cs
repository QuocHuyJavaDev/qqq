using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteAPI.Model;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace NoteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        OracleConnection conn = new OracleConnection("Data Source=(LQHDB =(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = DESKTOP-RPJP4E8)" +
            "(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = lqhdb))));user id=NOTEDB;password=NOTEDB");
        int result = -1;

        [HttpGet("getall")]
        public IActionResult GetAllInfor()
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<UserDetail> userDetails = new List<UserDetail>();
            OracleCommand command = new OracleCommand("SELECT * FROM USERDETAIL", conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                userDetails.Add(new UserDetail
                {
                    UsDetailId = oracleDataReader.GetInt32(0),
                    UsDetailName = oracleDataReader.GetString(1),
                    UsDetailMail = oracleDataReader.GetString(2),
                    UsDetailDOB = oracleDataReader.GetString(3),
                    UsDetailSex = oracleDataReader.GetInt32(4),
                    UDByUser = oracleDataReader.GetInt32(5)
                });
            }
            if (userDetails.Count > 0)
            {
                return Ok(userDetails);
            }
            else
            {
                return NotFound();
            }
        }
        //get by user
        [HttpGet("getbyUser/{userid}")]
        public IActionResult GetByUsId(int userid)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<UserDetail> ntb = new List<UserDetail>();
            OracleCommand command = new OracleCommand($"SELECT * FROM USERDETAIL WHERE UDByUser={userid}", conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                ntb.Add(new UserDetail
                {
                    UsDetailId = oracleDataReader.GetInt32(0),
                    UsDetailName = oracleDataReader.GetString(1),
                    UsDetailMail = oracleDataReader.GetString(2),
                    UsDetailDOB = oracleDataReader.GetString(3),
                    UsDetailSex = oracleDataReader.GetInt32(4),
                    UDByUser = oracleDataReader.GetInt32(5)

                });
            }
            if (ntb.Count > 0)
            {
                return Ok(ntb);
            }
            else
            {
                return NotFound();
            }
        }
        //Insert

        [HttpPost("addDetail")]
        public IActionResult AddNtb(UserDetail ud)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"INSERT INTO USERDETAIL " +
                    $"(UsDetailName, UsDetailMail, UsDetailDOB, UsDetailSex, UDByUser) VALUES " +
                    $"('{ud.UsDetailName}', '{ud.UsDetailMail}', '{ud.UsDetailDOB}', {ud.UsDetailSex}, {ud.UDByUser})", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllInfor();
            }
            else
            {
                return NotFound();
            }
        }
        //edit
        [HttpPut("editInfor/{userid}")]
        public IActionResult UpdateInfor(int userid, UserDetail ud)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"UPDATE USERDETAIL SET UsDetailName= '{ud.UsDetailName}'," +
                $" UsDetailMail= '{ud.UsDetailMail}', UsDetailDOB= '{ud.UsDetailDOB}', UsDetailSex={ud.UsDetailSex}" +
                $" WHERE UDByUser='{userid}'", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllInfor();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
