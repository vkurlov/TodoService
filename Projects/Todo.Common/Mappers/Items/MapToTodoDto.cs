using Todo.Dto;
using TodoItem = Todo.DomainContext.Models.Todo;

namespace Todo.Common.Mappers.Items
{
    /// <summary>
    /// Represents mapper from <see cref="Todo.DomainContext.Models.Todo"/> to <see cref="TodoDto"/>
    /// </summary>
    internal sealed class MapToTodoDto : IMapper<TodoItem, TodoDto>
    {
        /// <summary>
        /// Does mapping
        /// </summary>
        /// <param name="from">From object to make mapping</param>
        /// <returns> <see cref="TodoDto"/></returns>
        public TodoDto Map(TodoItem from)
        {
            return new TodoDto
            {
                Id = from.Id,
                Name = from.Name,
                IsComplete = from.IsComplete
            };
        }
    }
}