using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Dto;
using Todo.Managers.Todo;

namespace Todo.Api.Controllers
{
    /// <summary>
    /// _Todo Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "1.0")]
    public class TodoItemsController : BaseController
    {
        /// <summary>
        /// _Todo manager
        /// </summary>
        private readonly ITodoManager _todoManager;

        /// <summary>
        /// Initializes new instance of the class <see cref="TodoItemsController"/>
        /// </summary>
        /// <param name="todoManager">_Todo Manager</param>
        public TodoItemsController(ITodoManager todoManager)
        {
            _todoManager = todoManager;
        }

        /// <summary>
        /// Gets all tasks
        /// </summary>
        /// <returns>IEnumerable of tasks</returns>
        /// <response code="200">Tasks are retrieved</response>
        /// <response code="500">Internal server error, can not retrieve tasks</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoDto[]))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodoItems()
        {
            var result = await _todoManager.GetAllTodosAsync();
            return result;

        }

        /// <summary>
        /// Gets task by id
        /// </summary>
        /// <param name="id">Task id to search</param>
        /// <returns>Found task</returns>
        /// <response code="200">Task is retrieved</response>
        /// <response code="400">Request model is wrong.</response>
        /// <response code="404">Task not found</response>
        /// <response code="500">Internal server error, can not retrieve task</response>
        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<ActionResult<TodoDto>> GetTodoItem(long id)
        {
            var result = await _todoManager.GetTodoByIdAsync(id);
            return result;
        }

        /// <summary>
        /// Updates task
        /// </summary>
        /// <param name="id">Task id to search</param>
        /// <param name="item">Data to update the found task by <paramref name="id"/></param>
        /// <returns><see cref="HttpStatusCode.NoContent"/></returns>
        /// <response code="204">Task is updated</response>
        /// <response code="400">Request model is wrong.</response>
        /// <response code="404">Task not found, can not be updated.</response>
        /// <response code="500">Internal server error, can not update task</response>
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoDto item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            await _todoManager.UpdateTodoAsync(item);
            return NoContent();
        }

        /// <summary>
        /// Creates new task
        /// </summary>
        /// <param name="item">Data to create a new task</param>
        /// <returns>The newly created task</returns>
        /// <response code="201">_Todo is created</response>
        /// <response code="400">Request model is wrong.</response>
        /// <response code="500">Internal server error, can not create _todo</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TodoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<ActionResult<TodoDto>> CreateTodoItem(TodoDto item)
        {
            var result = await _todoManager.CreateTodoAsync(item);
            return CreatedAtAction(nameof(GetTodoItem), new {id = result.Id}, result);
        }

        /// <summary>
        /// Deletes _todo
        /// </summary>
        /// <param name="id">Id of _todo to delete</param>
        /// <returns><see cref="HttpStatusCode.NoContent"/></returns>
        /// <response code="204">Task is deleted</response>
        /// <response code="400">Request model is wrong.</response>
        /// <response code="404">Task not found, can not be deleted.</response>
        /// <response code="500">Internal server error, can not delete task</response>
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            await _todoManager.DeleteTodoAsync(id);
            return NoContent();
        }
    }
}
