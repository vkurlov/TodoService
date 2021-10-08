using System;

namespace Todo.Common.Exceptions
{
    /// <summary>
    /// Represents object not found exception
    /// </summary>
    [Serializable]
    public class ObjectNotFoundException:Exception
    {
        /// <summary>
        /// Initializes new instance of class <see cref="ObjectNotFoundException"/>
        /// </summary>
        /// <param name="message">Exception message</param>
        public ObjectNotFoundException(string message)
            : base(message)
        {
        }
    }
}
