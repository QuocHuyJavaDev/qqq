using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteAPI.Model;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace NoteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NtbController : ControllerBase
    {
        OracleConnection conn = new OracleConnection("Data Source=(LQHDB =(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = DESKTOP-RPJP4E8)" +
            "(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = lqhdb))));user id=NOTEDB;password=NOTEDB");
        int result = -1;

        [HttpGet]
        public IActionResult GetAllNtb()
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<Notebook> ntb = new List<Notebook>();
            OracleCommand command = new OracleCommand("SELECT * FROM NOTEBOOKS", conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                ntb.Add(new Notebook
                {
                    NotebookId = oracleDataReader.GetInt32(0),
                    NotebookName = oracleDataReader.GetString(1),
                    DateCreate = oracleDataReader.GetString(2),
                    ByUser = oracleDataReader.GetInt32(3)

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
        //GetById
        [HttpGet("{userid}")]
        public IActionResult GetByUsId(int userid) 
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<Notebook> ntb = new List<Notebook>();
            OracleCommand command = new OracleCommand($"SELECT * FROM NOTEBOOKS WHERE ByUser={userid}", conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                ntb.Add(new Notebook
                {
                    NotebookId = oracleDataReader.GetInt32(0),
                    NotebookName = oracleDataReader.GetString(1),
                    DateCreate = oracleDataReader.GetString(2),
                    ByUser = oracleDataReader.GetInt32(3)

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

        [HttpPost("addntb")]
        public IActionResult AddNtb(Notebook ntb)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"INSERT INTO NOTEBOOKS " +
                    $"(NotebookName, DateCreate, ByUser) VALUES " +
                    $"('{ntb.NotebookName}', '{ntb.DateCreate}', {ntb.ByUser})", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllNtb();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{ntbid}")]
        public IActionResult UpdateNtb(int ntbid, Notebook ntb)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"UPDATE NOTEBOOKS SET  NotebookName= '{ntb.NotebookName}'," +
                $" DateCreate= '{ntb.DateCreate}', ByUser={ntb.ByUser}" +
                $" WHERE NotebookId='{ntbid}'", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllNtb();
            }
            else
            {
                return NotFound();
            }
        }
        //Del
        [HttpDelete("delete/{ntbid}")]
        public IActionResult DelNtb(int ntbid)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"DELETE FROM NOTEBOOKS WHERE NotebookId='{ntbid}'", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllNtb();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
