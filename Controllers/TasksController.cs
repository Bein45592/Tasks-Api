using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Task = TasksApi.Model.Task;
using TaskService = TasksApi.Services.TaskService;

namespace TasksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/tasks
        [HttpGet]
        public ActionResult<IEnumerable<Task>> GetTasks()
        {
            try
            {
                var tasks = _taskService.GetAllTasks();
                return Ok(tasks);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/tasks/5
        [HttpGet("{id}")]
        public ActionResult<Task> GetTask(int id)
        {
            try
            {
                var task = _taskService.GetTaskById(id);
                if (task == null)
                {
                    return NotFound();
                }
                return Ok(task);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/tasks
        [HttpPost]
        public ActionResult<Task> CreateTask(Task newTask)
        {
            try
            {
                _taskService.AddTask(newTask);
                return Ok(newTask);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public ActionResult UpdateTask(int id, Task updatedTask)
        {
            try
            {
                _taskService.UpdateTask(id, updatedTask);
                return Ok(updatedTask);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            try
            {
                _taskService.DeleteTask(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}