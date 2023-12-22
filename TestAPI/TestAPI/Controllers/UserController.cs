using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteAPI.Model;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace NoteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        OracleConnection conn = new OracleConnection("Data Source=(LQHDB =(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = DESKTOP-RPJP4E8)" +
           "(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = lqhdb))));user id=NOTEDB;password=NOTEDB");
        int result = -1;
        [HttpGet]
        public IActionResult GetAllUser()
        {

            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<Users> user = new List<Users>();
            OracleCommand command = new OracleCommand("SELECT * FROM USERS", conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                user.Add(new Users
                {
                    UserId = oracleDataReader.GetInt32(0),
                    UserName = oracleDataReader.GetString(1),
                    UserPass = oracleDataReader.GetString(2)
                    
                });
            }
            if (user.Count > 0)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }
        //Login
        [HttpGet("{username}/{userpass}")]
        public IActionResult Login(string username, string userpass) 
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<Users> user = new List<Users>();
            OracleCommand command = new OracleCommand($"SELECT * FROM USERS WHERE Username='{username}'" +
                $" AND UserPass='{userpass}'", conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                user.Add(new Users
                {
                    UserId = oracleDataReader.GetInt32(0),
                    UserName = oracleDataReader.GetString(1),
                    UserPass = oracleDataReader.GetString(2)

                });
            }
            if (user.Count > 0)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }
        //Register
        [HttpPost("register")]
        public IActionResult Register(Users user)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"INSERT INTO USERS " +
                    $"(Username, UserPass) VALUES ('{user.UserName}', '{user.UserPass}')", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllUser();
            }
            else
            {
                return NotFound();
            }
        }
        //Del
        [HttpDelete("delete/{userid}")]
        public IActionResult DelNtb(int userid)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"DELETE FROM USERS WHERE UserId='{userid}'", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllUser();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
