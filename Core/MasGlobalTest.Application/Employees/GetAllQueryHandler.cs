using AutoMapper;
using MasGlobalTest.Domain;
using MasGlobalTest.ExternalServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasGlobalTest.Application.Employees
{
    public class GetAllQueryHandler : IQueryHandler<GetEmployeeFilter, IEnumerable<Employee>>
    {
        private readonly IEmployeeRepository _employeeService;
        public GetAllQueryHandler(IEmployeeRepository employeeService) =>
            (_employeeService) = (employeeService);

        public async Task<IEnumerable<Employee>> HandleAsync(GetEmployeeFilter query)
        {
            var employees = await _employeeService.GetAll().ConfigureAwait(false);
            return GetEmployees(employees);
        }

        private IEnumerable<Employee> GetEmployees(IEnumerable<EmployeeDto> employeesDto)
        {
            var employees = new List<Employee>();
            var employeeFactory = new EmployeeFactory();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EmployeeDto, Employee>(); });
            IMapper iMapper = config.CreateMapper();

            if (employeesDto != null)
            {
                foreach (var employeeDto in employeesDto)
                {
                    Employee employee = employeeFactory.CreateContract(employeeDto.ContractTypeName);
                    iMapper.Map(employeeDto, employee);
                    employee.Role = new Role(employeeDto);

                    employee.CalculateSalary();
                    employees.Add(employee);
                }
            }

            return employees;
        }

    }
}
