using System.Threading.Tasks;
using Todo.Common.Exceptions;
using Todo.Dto;

namespace Todo.Managers.Todo
{
    /// <summary>
    /// Represents _todo manager
    /// </summary>
    public interface ITodoManager
    {
        /// <summary>
        /// Retrieves all todos
        /// </summary>
        /// <returns>Array of todos</returns>
        /// <exception cref="DbContextAccessException">Database is not available, can not retrieve todos.</exception>
        Task<TodoDto[]> GetAllTodosAsync();

        /// <summary>
        /// Gets _todo by id
        /// </summary>
        /// <param name="id">_todo id to search</param>
        /// <returns>Found _todo</returns>
        /// <exception cref="DbContextAccessException">Database is not available, can not retrieve _todo.</exception>
        /// <exception cref="ObjectNotFoundException">_todo is not found.</exception>
        Task<TodoDto> GetTodoByIdAsync(long id);

        /// <summary>
        /// Updates _todo  
        /// </summary>
        /// <param name="todoItem">Dto with new data</param>
        /// <returns><c>true</c> when updated successfully, otherwise <c>false</c></returns>
        /// <exception cref="DbContextAccessException">Database is not available, can not update _todo.</exception>
        /// <exception cref="ObjectNotFoundException">_todo is not found.</exception>
        Task<bool> UpdateTodoAsync(TodoDto todoItem);

        /// <summary>
        /// Creates new _todo
        /// </summary>
        /// <param name="todoItem">Dto with data to add</param>
        /// <returns>New created _todo</returns>
        /// <exception cref="DbContextAccessException">Database is not available, can not create _todo.</exception>
        Task<TodoDto> CreateTodoAsync(TodoDto todoItem);

        /// <summary>
        /// Deletes _todo  
        /// </summary>
        /// <param name="id">Id of _todo to delete</param>
        /// <returns><c>true</c> when deleted successfully, otherwise <c>false</c></returns>
        /// <exception cref="DbContextAccessException">Database is not available, can not delete _todo.</exception>
        /// <exception cref="ObjectNotFoundException">_todo is not found.</exception>
        Task<bool> DeleteTodoAsync(long id);
    }
}