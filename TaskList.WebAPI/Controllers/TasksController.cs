using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskList.Domain;
using TaskList.Repository;

namespace TaskList.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        public readonly ITaskListRepository _repository;

        public TasksController(ITaskListRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllTasksAsync(1, StatusTaskType.Opened, false);
                return (Ok(results));
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");                
            }
        }

        [HttpGet("task/{taskId}/title/{title}/statusType/{statusType}/userId/{userId}/includeRemarks/{includeRemarks}")]
        public async Task<IActionResult> Get(int taskId, string title, int statusType, int userId, bool includeRemarks)
        {
            try
            {
                FilterTaskDTQ filterTaskQuery = new FilterTaskDTQ();
                if (taskId > 0)
                    filterTaskQuery.TaskId = taskId;
                else
                {
                    filterTaskQuery.TitleDescription = title;
                    if (IsStatusTypeValid(statusType))
                        filterTaskQuery.status = (StatusTaskType)statusType;
                    filterTaskQuery.userId = userId;
                    filterTaskQuery.IncludeRemarks = includeRemarks;
                }
                var results = await _repository.Get(filterTaskQuery);
                return (Ok(results));
            } 
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");                
            }
        }

        private bool IsStatusTypeValid(int statusType)
        {
            return Array.IndexOf(Enum.GetValues(typeof (StatusTaskType)), (StatusTaskType)statusType) >= 0;
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> Get(int taskId)
        {
            try
            {
                var results = await _repository.GetTaskAsyncById(taskId, false);
                return (Ok(results));
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");                
            }
        }

        [HttpGet("getByTitle/{title}")]
        public async Task<IActionResult> Get(string title)
        {
            try
            {
                var results = await _repository.GetAllTasksAsyncByTitle(1, 0, title, false);
                return (Ok(results));
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Tasks model)
        {
            try
            {
                _repository.Add(model);
                if (await(_repository.SaveChangesAsync())){
                    return Created($"/api/tasks/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");                
            }
            return BadRequest();
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> Put(int taskId, Tasks model)
        {
            try
            {
                var task = await _repository.GetTaskAsyncById(taskId, false);
                if (task == null) return NotFound();

                _repository.Update(model);
                if (await(_repository.SaveChangesAsync())){
                    return Created($"/api/tasks/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");                
            }
            return BadRequest();
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> Delete(int taskId)
        {
            try
            {
                var task = await _repository.GetTaskAsyncById(taskId, false);
                if (task == null) return NotFound();

                _repository.Delete(task);
                if (await(_repository.SaveChangesAsync())){
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");                
            }
            return BadRequest();
        }
    }
}