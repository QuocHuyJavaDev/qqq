using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteAPI.Model;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace NoteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TDMainController : ControllerBase
    {
        OracleConnection conn = new OracleConnection("Data Source=(LQHDB =(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = DESKTOP-RPJP4E8)" +
           "(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = lqhdb))));user id=NOTEDB;password=NOTEDB");
        int result = -1;
        //Get all Todo
        [HttpGet("allTodo")]
        public IActionResult GetAllTodo()
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<TodoMain> tdMain = new List<TodoMain>();
            OracleCommand command = new OracleCommand("SELECT * FROM TODOMAIN", conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                tdMain.Add(new TodoMain
                {
                    MainId = oracleDataReader.GetInt32(0),
                    MainName = oracleDataReader.GetString(1),
                    DateMain = oracleDataReader.GetString(2),
                    MByUser = oracleDataReader.GetInt32(3)
                });
            }
            if (tdMain.Count > 0)
            {
                return Ok(tdMain);
            }
            else
            {
                return NotFound();
            }
        }
        //Get by Userid
        [HttpGet("{userid}")]
        public IActionResult GetTDByUser(int userid)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<TodoMain> tdMain = new List<TodoMain>();
            OracleCommand command = new OracleCommand($"SELECT * FROM TODOMAIN WHERE MByUser={userid}", conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                tdMain.Add(new TodoMain
                {
                    MainId = oracleDataReader.GetInt32(0),
                    MainName = oracleDataReader.GetString(1),
                    DateMain = oracleDataReader.GetString(2),
                    MByUser = oracleDataReader.GetInt32(3)

                });
            }
            if (tdMain.Count > 0)
            {
                return Ok(tdMain);
            }
            else
            {
                return NotFound();
            }
        }
        //Add todo
        [HttpPost("addtodo")]
        public IActionResult AddTodo(TodoMain tdMain)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"INSERT INTO TODOMAIN " +
                    $"(MainName, DateMain, MByUser) VALUES " +
                    $"('{tdMain.MainName}', '{tdMain.DateMain}', {tdMain.MByUser})", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllTodo();
            }
            else
            {
                return NotFound();
            }
        }
        //Edit todo Name
        [HttpPut("editTDName/{mainid}")]
        public IActionResult EditTDMain(int mainid, TodoMain tdMain)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"UPDATE TODOMAIN SET MainName= '{tdMain.MainName}'," +
                $" DateMain= '{tdMain.DateMain}', MByUser={tdMain.MByUser}" +
                $" WHERE MainId='{mainid}'", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllTodo();
            }
            else
            {
                return NotFound();
            }
        }
        //Delete Todo
        [HttpDelete("delete/{mainid}")]
        public IActionResult DeleteTodo(int mainid)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"DELETE FROM TODOMAIN WHERE MainId='{mainid}'", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllTodo();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
