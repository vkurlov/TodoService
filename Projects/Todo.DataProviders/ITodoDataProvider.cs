using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Todo.DomainContext.Contexts;
using TodoItem = Todo.DomainContext.Models.Todo;

namespace Todo.DataProviders
{
    public interface ITodoDataProvider
    {
        TodoContext DbContext { get; }

        bool IsExist(long id);

        Task<IEnumerable<TodoItem>> GetAllTodosAsNoTrackingAsync();
        Task<TodoItem> GetTodoByIdAsync(long id);
        Task<TodoItem> GetTodoByIdAsNoTrackingAsync(long id);
    }
}