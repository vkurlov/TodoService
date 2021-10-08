using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.DomainContext.Contexts;
using TodoItem = Todo.DomainContext.Models.Todo;

namespace Todo.DataProviders.Todo
{
    /// <summary>
    /// Represents _Todo Data Provider
    /// </summary>
    public interface ITodoDataProvider
    {
        /// <summary>
        /// Gets DB Context
        /// </summary>
        TodoContext DbContext { get; }

        /// <summary>
        /// Checks existing of _todo by id
        /// </summary>
        /// <param name="id">Id to search _todo</param>
        /// <returns><c>true</c> when found, otherwise <c>false</c></returns>
        bool IsExist(long id);

        /// <summary>
        /// Gets all todos with no tracking
        /// </summary>
        /// <returns>IEnumerable of todos</returns>
        Task<IEnumerable<TodoItem>> GetAllTodosAsNoTrackingAsync();

        /// <summary>
        /// Gets _todo by id with tracking
        /// </summary>
        /// <param name="id">Id to search _todo</param>
        /// <returns>found _todo</returns>
        Task<TodoItem> GetTodoByIdAsync(long id);

        /// <summary>
        /// Gets _todo by id with no tracking
        /// </summary>
        /// <param name="id">Id to search _todo</param>
        /// <returns>found _todo</returns>
        Task<TodoItem> GetTodoByIdAsNoTrackingAsync(long id);
    }
}