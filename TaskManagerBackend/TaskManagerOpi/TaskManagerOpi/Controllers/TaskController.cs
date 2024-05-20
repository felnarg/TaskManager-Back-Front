using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using static Application.Commands.ChangeStatusTaskCommand;

namespace TaskManagerOpi.Controllers
{
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskCreate? _taskCreate;
        private readonly ITaskDelete? _taskDelete;
        private readonly ITaskGetAll? _taskGetAll;
        private readonly ITaskGetById? _taskGetById;
        private readonly ITaskUpdate? _taskUpdate;
        private readonly IOrderTasks? _orderTasks;
        private readonly IChangeStatusTask? _changeStatusTask;
        private readonly IImportanceFilter? _importanceFilter;
        public TaskController(ITaskCreate taskCreate, ITaskDelete? taskDelete, ITaskGetAll? taskGetAll, ITaskGetById? taskGetById,
            ITaskUpdate? taskUpdate, IOrderTasks? orderTasks, IChangeStatusTask? changeStatusTask, IImportanceFilter? importanceFilter)
        {
            _taskCreate = taskCreate;
            _taskDelete = taskDelete;
            _taskGetAll = taskGetAll;
            _taskGetById = taskGetById;
            _taskUpdate = taskUpdate;
            _orderTasks = orderTasks;
            _changeStatusTask = changeStatusTask;
            _importanceFilter = importanceFilter;
        }

        [HttpPost]
        [Route("createtask")]
        public async Task<IActionResult> CreateTask([FromBody] TaskEntity entity)
        {            
            await _taskCreate!.TaskCreate(entity);
            return Ok();
        }

        [HttpPost]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
        {
            await _taskDelete!.TaskDelete(id);
            return Ok();
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateTask([FromBody] TaskEntity entity)
        {
            await _taskUpdate!.TaskUpdate(entity);
            return Ok();
        }

        [HttpGet]
        [Route("gettask/{id}")]
        public async Task<IActionResult> GetTaskById([FromRoute] Guid id)
        {
            return Ok(await _taskGetById!.TaskGetById(id));
        }

        [HttpGet]
        [Route("gettasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(await _taskGetAll!.TaskGetAll());
        }

        [HttpPut]
        [Route("changestatus/{id}")]
        public async Task<IActionResult> ChangeStatusTask([FromRoute] Guid id)
        {
            await _changeStatusTask!.ChangeStatusTask(id);
            return Ok();
        }

        [HttpGet]
        [Route("ordertasks")]
        public async Task<IActionResult> GetActiveTasks()
        {            
            return Ok(await _orderTasks!.OrderTasksByImportance());
        }

        [HttpGet]
        [Route("filterbyimportance/{importance}")]
        public async Task<IActionResult> GetFilterTaskByImportance([FromRoute] Importance importance)
        {
            return Ok(await _importanceFilter!.FilterByImportanceLevel(importance));
        }
    }
}
