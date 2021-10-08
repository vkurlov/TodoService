using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Todo.Common.Exceptions;

namespace Todo.Managers
{
    public abstract class BaseManager
    {
        protected ILogger Logger { get; }

        protected BaseManager(ILogger logger)
        {
            Logger = logger;
        }

        protected async Task<T> TryGetFromDb<T>(Func<Task<T>> func, string logErrorMessage)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, logErrorMessage);
                throw new DbContextAccessException();
            }
        }


        protected async Task<T> TryGetFromDbAsSingle<T>(Func<Task<T>> func, string logErrorMessage, string objectNotFoundExceptionMessage = null)
        {
            var item = await TryGetFromDb(func, logErrorMessage);
            if (item == null)
            {
                throw new ObjectNotFoundException(objectNotFoundExceptionMessage ?? "Object not found.");
            }

            return item;
        }

        protected async Task ExecuteInDb(Func<Task> func, string logErrorMessage, Func<Exception, Exception> getExceptionToThrow = null)
        {
            try
            {
                await func();
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, logErrorMessage);
                throw getExceptionToThrow == null
                    ? new DbContextAccessException()
                    : ex;
            }
        }

    }
}