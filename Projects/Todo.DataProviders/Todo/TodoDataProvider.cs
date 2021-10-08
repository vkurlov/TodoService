using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.DomainContext.Contexts;
using TodoItem = Todo.DomainContext.Models.Todo;

namespace Todo.DataProviders.Todo
{
    /// <summary>
    /// Represents _Todo Data Provider
    /// </summary>
    public class TodoDataProvider:ITodoDataProvider
    {
        /// <summary>
        /// Gets DB context
        /// </summary>
        public TodoContext DbContext { get; }

        /// <summary>
        /// Initializes new instance of the class <see cref="TodoDataProvider"/>
        /// </summary>
        /// <param name="dbContext">DB context</param>
        public TodoDataProvider(TodoContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <inheritdoc />
        public bool IsExist(long id)
            => DbContext.TodoItems.Any(todo => todo.Id == id);

        /// <inheritdoc />
        public async Task<IEnumerable<TodoItem>> GetAllTodosAsNoTrackingAsync() 
            => await DbContext.TodoItems.AsNoTracking().ToArrayAsync();

        /// <inheritdoc />
        public async Task<TodoItem> GetTodoByIdAsync(long id) 
            => await DbContext.TodoItems.FindAsync(id);

        /// <inheritdoc />
        public async Task<TodoItem> GetTodoByIdAsNoTrackingAsync(long id)
        {
            var todoItem = await GetTodoByIdAsync(id);
            if(todoItem != null)
                DbContext.Entry(todoItem).State = EntityState.Detached;
    
            return todoItem;
        }
    }
}
