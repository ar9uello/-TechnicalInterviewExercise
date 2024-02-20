using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController(ITaskService taskService) : ControllerBase
    {
        private readonly ITaskService _taskService = taskService;

        /// <summary>
        /// Get all tasks
        /// </summary>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<GetTaskVm>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return Ok(_taskService.GetAll());
        }

        /// <summary>
        /// Get a task by its id
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GetTaskVm), StatusCodes.Status200OK)]
        public IActionResult GetById([FromRoute] int id)
        {
            return Ok(_taskService.GetById(id));
        }

        /// <summary>
        /// Create a new task
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateTaskVm vm)
        {
            var newId = _taskService.Create(vm);
            return CreatedAtAction(nameof(GetById), new { id = newId }, newId);
        }

        /// <summary>
        /// Update task
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Update([FromBody] UpdateTaskVm vm)
        {
            _taskService.Update(vm);
            return Ok();
        }

        /// <summary>
        /// Delete task
        /// </summary>
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete([FromRoute] int id)
        {
            _taskService.Delete(id);
            return Ok();
        }
    }
}
