using MasGlobalTest.Domain;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasGlobalTest.ExternalServices
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IApiBroker _apiBroker;
        private readonly string employeeServiceUrl;

        public EmployeeRepository(IApiBroker apiBroker, IOptions<ExternalServiceConfigurationSettings> options) 
            => (_apiBroker, employeeServiceUrl) = (apiBroker, options.Value.ApiEmployeeUrl);

        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            var parameters = new RequestParameter(System.Net.Http.HttpMethod.Get, employeeServiceUrl);
            var employees = await _apiBroker.GetServiceObjectResponseAsync<IEnumerable<EmployeeDto>>(parameters).ConfigureAwait(false);

            return employees;
        }

        public async Task<EmployeeDto> GetById(int id)
        {
            var employees = await GetAll().ConfigureAwait(false);
            var employee = employees.FirstOrDefault(i => i.Id == id);
           
            return employee;
        }
    }
}