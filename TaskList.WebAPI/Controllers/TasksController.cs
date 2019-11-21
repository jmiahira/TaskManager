using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskList.Domain;
using TaskList.Repository;
using TaskList.WebAPI.DTO;

namespace TaskList.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        public readonly ITaskListRepository _repository;

        private readonly IMapper _mapper;

        public TasksController(ITaskListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tasks = await _repository.GetAllTasksAsync(1, StatusTaskType.Opened, false);
                var results = _mapper.Map<TasksDTO[]>(tasks);
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
                var task = await _repository.GetTaskAsyncById(taskId, true);
                var results = _mapper.Map<TasksDTO>(task);
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
                var tasks = await _repository.GetAllTasksAsyncByTitle(1, 0, title, false);
                var results = _mapper.Map<TasksDTO[]>(tasks);
                return (Ok(results));
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(TasksDTO model)
        {
            try
            {
                var task = _mapper.Map<Tasks>(model);
                _repository.Add(task);
                if (await(_repository.SaveChangesAsync())){
                    return Created($"/api/tasks/{model.Id}", _mapper.Map<TasksDTO>(task));
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");                
            }
            return BadRequest();
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> Put(int taskId, TasksDTO model)
        {
            try
            {
                var task = await _repository.GetTaskAsyncById(taskId, false);
                if (task == null) return NotFound();

                _mapper.Map(model, task);

                _repository.Update(task);
                if (await(_repository.SaveChangesAsync())){
                    return Created($"/api/tasks/{model.Id}", _mapper.Map<TasksDTO>(task));
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