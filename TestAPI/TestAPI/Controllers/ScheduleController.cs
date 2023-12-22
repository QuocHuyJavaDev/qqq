using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteAPI.Model;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace NoteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        OracleConnection conn = new OracleConnection("Data Source=(LQHDB =(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = DESKTOP-RPJP4E8)" +
           "(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = lqhdb))));user id=NOTEDB;password=NOTEDB");
        int result = -1;
        [HttpGet("allsche")]
        public IActionResult GetAllEvent() 
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<Schedule> even = new List<Schedule>();
            OracleCommand command = new OracleCommand("SELECT * FROM SCHEDULE", conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                even.Add(new Schedule
                {
                    EventId = oracleDataReader.GetInt32(0),
                    EventStart = oracleDataReader.GetString(1),
                    EventEnd = oracleDataReader.GetString(2),
                    EventName = oracleDataReader.GetString(3),
                    EByUser = oracleDataReader.GetInt32(4)

                });
            }
            if (even.Count > 0)
            {
                return Ok(even);
            }
            else
            {
                return NotFound();
            }
        }
        //get by user
        //GetById
        [HttpGet("sche/{userid}")]
        public IActionResult GetEvenByUser(int userid)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<Schedule> schedules = new List<Schedule>();
            OracleCommand command = new OracleCommand($"SELECT * FROM SCHEDULE WHERE EByUser={userid}", conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                schedules.Add(new Schedule
                {
                    EventId = oracleDataReader.GetInt32(0),
                    EventStart = oracleDataReader.GetString(1),
                    EventEnd = oracleDataReader.GetString(2),
                    EventName = oracleDataReader.GetString(3),
                    EByUser = oracleDataReader.GetInt32(4)

                });
            }
            if (schedules.Count > 0)
            {
                return Ok(schedules);
            }
            else
            {
                return NotFound();
            }
        }
        //Insert

        [HttpPost("addsche")]
        public IActionResult AddSche(Schedule sche)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"INSERT INTO SCHEDULE " +
                    $"(EventStart, EventEnd, EventName, EByUser) VALUES " +
                    $"('{sche.EventStart}', '{sche.EventEnd}', '{sche.EventName}', " +
                    $"{sche.EByUser})", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllEvent();
            }
            else
            {
                return NotFound();
            }
        }
        //edit
        [HttpPut("editsche/{scheid}")]
        public IActionResult UpdateNtb(int scheid, Schedule sche)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"UPDATE SCHEDULE SET EventStart= '{sche.EventStart}'," +
                $" EventEnd= '{sche.EventEnd}', EventName='{sche.EventName}', EByUser={sche.EByUser}" +
                $" WHERE EventId='{scheid}'", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllEvent();
            }
            else
            {
                return NotFound();
            }
        }
        //Del
        [HttpDelete("delete/{scheid}")]
        public IActionResult DelNtb(int scheid)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"DELETE FROM SCHEDULE WHERE EventId='{scheid}'", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllEvent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
