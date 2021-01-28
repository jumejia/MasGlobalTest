using MasGlobalTest.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasGlobalTest.ExternalServices
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDto>> GetAll();
        Task<EmployeeDto> GetById(int id);
    }
}
