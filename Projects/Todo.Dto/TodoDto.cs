namespace Todo.Dto
{
    public class TodoDto
    {
        /// <summary>
        /// Gets or sets task ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets task name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets <c>true</c> when the task is completed, otherwise <c>false</c>
        /// </summary>
        public bool IsComplete { get; set; }
    }
}
