using Todo.Common.Mappers.Items;
using Todo.Dto;
using TodoItem = Todo.DomainContext.Models.Todo;
namespace Todo.Common.Mappers
{
    public static class Mapper
    {
        public static TodoDto MapToTodoDto(TodoItem from) 
            => new MapToTodoDto().Map(from);
    }
}