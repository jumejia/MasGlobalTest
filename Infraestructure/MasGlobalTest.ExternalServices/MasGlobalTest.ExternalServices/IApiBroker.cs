using System.Threading.Tasks;

namespace MasGlobalTest.ExternalServices
{
    public interface IApiBroker
    {
        /// <summary>
        /// Retrieves any Object passed as T class.
        /// </summary>
        /// <typeparam name="T">Object T</typeparam>
        /// <param name="requestParameter">RequestParameter requestParameter.</param>
        /// <returns>Object T</returns>
        Task<T> GetServiceObjectResponseAsync<T>(RequestParameter requestParameter) where T : class;
    }
}
