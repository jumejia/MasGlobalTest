using AutoMapper;
using MasGlobalTest.Domain;
using MasGlobalTest.ExternalServices;
using System;
using System.Threading.Tasks;

namespace MasGlobalTest.Application.Employees
{
    public class GetByIdQueryHandler : IQueryHandler<GetEmployeeFilter, Employee>
    {
        private readonly IEmployeeRepository _employeeService;
        public GetByIdQueryHandler(IEmployeeRepository employeeService) =>
            (_employeeService) = (employeeService);

        public async Task<Employee> HandleAsync(GetEmployeeFilter query)
        {
            if (!query.EmployeeId.HasValue)
            {
                throw new Exception("Employee ID is required");
            }

            var employee = await _employeeService.GetById(query.EmployeeId.Value).ConfigureAwait(false);
            return GetEmployee(employee);
        }

        private Employee GetEmployee(EmployeeDto employeeDto)
        {
            var contractFactory = new EmployeeFactory();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EmployeeDto, Employee>(); });
            IMapper iMapper = config.CreateMapper();

            Employee employee = contractFactory.CreateContract(employeeDto.ContractTypeName);
            iMapper.Map(employeeDto, employee);
            employee.CalculateSalary();

            return employee;
        }

    }
}
