using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Todo.Common.Exceptions;
using Todo.Common.Mappers;
using Todo.DataProviders.Todo;
using Todo.Dto;
using TodoItem = Todo.DomainContext.Models.Todo;

namespace Todo.Managers.Todo
{
    /// <summary>
    /// Represents _todo manager
    /// </summary>
    public class TodoManager: BaseManager, ITodoManager
    {
        /// <summary>
        /// _Todo data provider
        /// </summary>
        private readonly ITodoDataProvider _todoDataProvider;

        /// <summary>
        /// Initializes new instance of class <see cref="TodoManager"/> 
        /// </summary>
        /// <param name="todoDataProvider">_Todo data provider</param>
        /// <param name="logger">Logger</param>
        public TodoManager(ITodoDataProvider todoDataProvider, ILogger<TodoManager> logger)
            : base(logger: logger)
        {
            _todoDataProvider = todoDataProvider;
        }

        /// <inheritdoc />
        public async Task<TodoDto[]> GetAllTodosAsync()
        {
            var todoRecords = await TryGetFromDb(
                func: () => _todoDataProvider.GetAllTodosAsNoTrackingAsync(),
                logErrorMessage: GetCanNotRetrieveTodoRecordsDbErrorMessage());

            return todoRecords.Select(selector: Mapper.MapToTodoDto).ToArray();
        }


        /// <inheritdoc />
        public async Task<TodoDto> GetTodoByIdAsync(long id)
        {
            var todo = await TryGetFromDbAsSingle(
                func: () => _todoDataProvider.GetTodoByIdAsNoTrackingAsync(id: id),
                logErrorMessage: GetCanNotRetrieveTodoRecordDbErrorMessage(id: id),
                objectNotFoundExceptionMessage: GetTodoRecordNotFoundDbErrorMessage(id: id));

            return Mapper.MapToTodoDto(@from: todo);
        }

        /// <inheritdoc />
        public async Task<bool> UpdateTodoAsync(TodoDto todo)
        {
            var todoItem = await TryGetFromDbAsSingle(
                func: () => _todoDataProvider.GetTodoByIdAsync(id: todo.Id),
                logErrorMessage: GetCanNotRetrieveTodoRecordDbErrorMessage(id: todo.Id),
                objectNotFoundExceptionMessage: GetTodoRecordNotFoundDbErrorMessage(id: todo.Id));

          
            todoItem.Name = todo.Name;
            todoItem.IsComplete = todo.IsComplete;

            await ExecuteInDb(func: () => _todoDataProvider.DbContext.SaveChangesAsync(),
                logErrorMessage: GetCanNotUpdateTodoRecordDbErrorMessage(id: todo.Id),
                getExceptionToThrow: exception => exception is DbUpdateConcurrencyException && !_todoDataProvider.IsExist(id: todo.Id)
                    ? (Exception) new ObjectNotFoundException(message: GetTodoRecordNotFoundDbErrorMessage(id: todo.Id)) 
                    : new DbContextAccessException());
            
            return true;
        }
        
        /// <inheritdoc />
        public async Task<TodoDto> CreateTodoAsync(TodoDto todoItem)
        {
            var todo = new TodoItem
            {
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };

            _todoDataProvider.DbContext.TodoItems.Add(entity: todo);

            await ExecuteInDb(
                func: () => _todoDataProvider.DbContext.SaveChangesAsync(),
                logErrorMessage: GetCanNotCreateTodoRecordDbErrorMessage());

            return Mapper.MapToTodoDto(@from: todo);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteTodoAsync(long id)
        {
            var todoItem = await TryGetFromDbAsSingle(
                func: () => _todoDataProvider.GetTodoByIdAsync(id: id),
                logErrorMessage: GetCanNotRetrieveTodoRecordDbErrorMessage(id: id),
                objectNotFoundExceptionMessage: GetTodoRecordNotFoundDbErrorMessage(id: id));

            _todoDataProvider.DbContext.TodoItems.Remove(entity: todoItem);


            await ExecuteInDb(
                func: () => _todoDataProvider.DbContext.SaveChangesAsync(),
                logErrorMessage: GetCanNotDeleteTodoRecordDbErrorMessage(id: id));

            return true;
        }

        #region Error messages

        /// <summary>
        /// Gets error message
        /// </summary>
        /// <param name="id">Record id</param>
        /// <returns>Can not retrieve todo record with id:{id} from database.</returns>
        private static string GetCanNotRetrieveTodoRecordDbErrorMessage(long id) 
            => $"Can not retrieve todo record with id:{{{id}}} from database.";

        /// <summary>
        /// Gets error message
        /// </summary>
        /// <param name="id">Record id</param>
        /// <returns>Todo with id={id} not found.</returns>
        private static string GetTodoRecordNotFoundDbErrorMessage(long id) 
            => $"Todo with id={{{id}}} not found.";

        /// <summary>
        /// Gets error message
        /// </summary>
        /// <param name="id">Record id</param>
        /// <returns>Can not delete todo record with id:{id}.</returns>
        private static string GetCanNotDeleteTodoRecordDbErrorMessage(long id)
            => $"Can not delete todo record with id:{{{id}}}.";

        /// <summary>
        /// Gets error message
        /// </summary>
        /// <param name="id">Record id</param>
        /// <returns>Can not update todo record with id:{id}.</returns>
        private static string GetCanNotUpdateTodoRecordDbErrorMessage(long id)
            => $"Can not update todo record with id:{{{id}}}.";

        /// <summary>
        /// Gets error message
        /// </summary>
        /// <returns>Can not create todo record.</returns>
        private static string GetCanNotCreateTodoRecordDbErrorMessage()
            => "Can not create todo record.";

        /// <summary>
        /// Gets error message
        /// </summary>
        /// <returns>Can not retrieve todo records from database.</returns>
        private static string GetCanNotRetrieveTodoRecordsDbErrorMessage()
            => "Can not retrieve todo records from database.";

        #endregion
    }
}
