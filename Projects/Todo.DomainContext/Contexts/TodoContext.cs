using Microsoft.EntityFrameworkCore;
using TodoItem = Todo.DomainContext.Models.Todo;
namespace Todo.DomainContext.Contexts
{
    /// <summary>
    /// Represents _todo DB context
    /// </summary>
    public class TodoContext : DbContext
    {
        /// <summary>
        /// Initializes new instance of class <see cref="TodoContext"/> 
        /// </summary>
        /// <param name="options">DB context options</param>
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets DbSet for <see cref="Todo.DomainContext.Models.Todo"/> entity
        /// </summary>
        public DbSet<TodoItem> TodoItems { get; set; }


        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().ToTable("Todo");
        }
    }
}