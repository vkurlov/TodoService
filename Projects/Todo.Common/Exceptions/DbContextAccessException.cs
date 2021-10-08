using System;

namespace Todo.Common.Exceptions
{
    /// <summary>
    /// Represents db context access failed exception
    /// </summary>
    [Serializable]
    public class DbContextAccessException:Exception
    {
        /// <summary>
        /// Initializes new instance of class <see cref="DbContextAccessException"/>
        /// </summary>
        public DbContextAccessException()
            : base("Data provider service temporarily unavailable.")
        {
        }
    }
}
