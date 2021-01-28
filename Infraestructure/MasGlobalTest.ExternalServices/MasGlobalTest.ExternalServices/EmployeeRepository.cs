using MasGlobalTest.Common.Exceptions;
using MasGlobalTest.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasGlobalTest.ExternalServices
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IApiBroker _apiBroker;
        public EmployeeRepository(IApiBroker apiBroker) => (_apiBroker) = (apiBroker);

        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            var parameters = new RequestParameter(System.Net.Http.HttpMethod.Get, "");
            var employees = await _apiBroker.GetServiceObjectResponseAsync<IEnumerable<EmployeeDto>>(parameters).ConfigureAwait(false);

            return employees;
        }

        public async Task<EmployeeDto> GetById(int id)
        {
            var employees = await GetAll().ConfigureAwait(false);

            var employee = employees.FirstOrDefault(i => i.Id == id);
            if (employee == null)
            {
                throw new EntityDoesNotExistException($"Employee with ID {id} was not found");
            }

            return employee;
        }
    }
}
