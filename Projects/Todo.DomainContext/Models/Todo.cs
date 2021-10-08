using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.DomainContext.Models
{
    /// <summary>
    /// _Todo table
    /// </summary>
    public class Todo
    {
        /// <summary>
        /// Gets or sets _todo id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets _todo name
        /// </summary>
        
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets <c>true</c> when the _todo is completed, otherwise <c>false</c>
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Gets or sets the secret value for this _todo
        /// </summary>
        [MaxLength(255)]
        public string Secret { get; set; }
    }
}