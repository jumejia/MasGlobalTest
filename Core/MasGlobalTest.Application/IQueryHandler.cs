using System.Threading.Tasks;

namespace MasGlobalTest.Application
{
    /// <summary>
    /// Handles the query
    /// </summary>
    public interface IQueryHandler<in TQuery, TReturn> where TQuery : IQuery
    {
        /// <summary>
        /// Performs the query action
        /// </summary>
        /// <param name="query">The query to execute</param>
        /// <returns>Retrieves the query results</returns>
        Task<TReturn> Handle(TQuery query);
    }
}
