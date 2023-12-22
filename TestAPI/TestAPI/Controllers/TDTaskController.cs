using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteAPI.Model;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.DirectoryServices.ActiveDirectory;

namespace NoteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TDTaskController : ControllerBase
    {
        OracleConnection conn = new OracleConnection("Data Source=(LQHDB =(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = DESKTOP-RPJP4E8)" +
           "(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = lqhdb))));user id=NOTEDB;password=NOTEDB");
        int result = -1;
        //Get All by Main
        [HttpGet("gettask")]
        public IActionResult GetAllTask()
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<TodoTask> tdTask = new List<TodoTask>();
            OracleCommand command = new OracleCommand("SELECT * FROM TODOTASKS", conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                tdTask.Add(new TodoTask
                {
                    TaskId = oracleDataReader.GetInt32(0),
                    TaskName = oracleDataReader.GetString(1),
                    IsDone = oracleDataReader.GetInt32(2),
                    TByMain = oracleDataReader.GetInt32(3)
                });
            }
            if (tdTask.Count > 0)
            {
                return Ok(tdTask);
            }
            else
            {
                return NotFound();
            }
        }
        //Get task by TDMain
        [HttpGet("{mainid}")]
        public IActionResult GetTaskByMain(int mainid)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<TodoTask> tdTask = new List<TodoTask>();
            OracleCommand command = new OracleCommand($"SELECT * FROM TODOTASKS WHERE TByMain={mainid}", conn);
            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read() && oracleDataReader != null)
            {
                tdTask.Add(new TodoTask
                {
                    TaskId = oracleDataReader.GetInt32(0),
                    TaskName = oracleDataReader.GetString(1),
                    IsDone = oracleDataReader.GetInt32(2),
                    TByMain = oracleDataReader.GetInt32(3)

                });
            }
            if (tdTask.Count > 0)
            {
                return Ok(tdTask);
            }
            else
            {
                return NotFound();
            }
        }
        //Add Task
        [HttpPost("addtask")]
        public IActionResult AddTask(TodoTask task)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"INSERT INTO TODOTASKS " +
            $"(TaskName, IsDone, TByMain) VALUES " +
                    $"('{task.TaskName}', {task.IsDone}, {task.TByMain})", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllTask();
            }
            else
            {
                return NotFound();
            }
        }
        //edit task
        [HttpPut("edittask/{taskid}")]
        public IActionResult EditTask(int taskid, TodoTask tdTask)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"UPDATE TODOTASKS SET TaskName= '{tdTask.TaskName}'," +
                $" IsDone= {tdTask.IsDone}, TByMain={tdTask.TByMain} WHERE TaskId= {taskid}", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllTask();
            }
            else
            {
                return NotFound();
            }
        }
        //delete task
        [HttpDelete("delete/{taskid}")]
        public IActionResult DeleteTask(int taskid)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"DELETE FROM TODOTASKS WHERE TaskId='{taskid}'", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllTask();
            }
            else
            {
                return NotFound();
            }
        }
        //Is done
        [HttpPut("done/{taskid}")]
        public IActionResult TaskDone(int taskid, TodoTask tdTask)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"UPDATE TODOTASKS SET IsDone={tdTask.IsDone}" +
                $" WHERE TaskId='{taskid}'", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllTask();
            }
            else
            {
                return NotFound();
            }
        }
        //edit name
        [HttpPut("editname/{taskid}")]
        public IActionResult NameTask(int taskid, TodoTask tdTask)
        {
            if (conn == null || conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OracleCommand oc = new OracleCommand($"UPDATE TODOTASKS SET TaskName= '{tdTask.TaskName}'" +
                $" WHERE TaskId= {taskid}", conn);
            try
            {
                result = oc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return GetAllTask();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
