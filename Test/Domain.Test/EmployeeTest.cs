using AutoMapper;
using MasGlobalTest.Application;
using MasGlobalTest.Application.Employees;
using MasGlobalTest.Domain;
using MasGlobalTest.ExternalServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Test
{
    [TestClass]
    public class EmployeeTest
    {
        private EmployeeFactory employeeFactory;

        [TestInitialize]
        public void SetUp()
        {
            employeeFactory = new EmployeeFactory();
        }

        [TestMethod]
        public void MonthlyEmployeeTest()
        {
            Employee employee = employeeFactory.CreateContract("MonthlySalaryEmployee");
            employee.ContractTypeName = "MonthlySalaryEmployee";
            employee.HourlySalary = 100;
            employee.Id = 1;
            employee.MonthlySalary = 2000;
            employee.Role = new Role() { RoleId = 1, RoleName = "Administrator" };
            employee.CalculateSalary();

            Assert.AreEqual(employee.AnnualSalary, 24000);
        }

        [TestMethod]
        public void HourlyEmployeeTest()
        {
            Employee employee = employeeFactory.CreateContract("HourlySalaryEmployee");
            employee.ContractTypeName = "HourlySalaryEmployee";
            employee.HourlySalary = 100;
            employee.Id = 2;
            employee.MonthlySalary = 2000;
            employee.Role = new Role() { RoleId = 2, RoleName = "Contractor" };
            employee.CalculateSalary();

            Assert.AreEqual(employee.AnnualSalary, 144000);
        }

        [TestMethod]
        public void GetAllQueryHandlerTest()
        {
            List<EmployeeDto> employeeList = GetEmployeesDto();
            Mock<GetEmployeeFilter> filter = new Mock<GetEmployeeFilter>();
            Mock<IQueryHandler<GetEmployeeFilter, IEnumerable<Employee>>> queryHadler = new Mock<IQueryHandler<GetEmployeeFilter, IEnumerable<Employee>>>();
            queryHadler.Setup(i => i.HandleAsync(filter.Object)).ReturnsAsync(GetEmployees());

            var result = queryHadler.Object.HandleAsync(filter.Object).Result;

            Assert.AreEqual(result.Count(), employeeList.Count);
        }

        [TestMethod]
        public void GetByIdQueryHandler()
        {
            Employee employee = GetEmployeesById(1);
            Mock<GetEmployeeFilter> filter = new Mock<GetEmployeeFilter>();
            filter.Object.EmployeeId = 1;
            Mock<IQueryHandler<GetEmployeeFilter, Employee>> queryHadler = new Mock<IQueryHandler<GetEmployeeFilter, Employee>>();
            queryHadler.Setup(i => i.HandleAsync(filter.Object)).ReturnsAsync(GetEmployeesById(1));

            var result = queryHadler.Object.HandleAsync(filter.Object).Result;

            Assert.AreEqual(result.AnnualSalary, employee.AnnualSalary);
        }

        private static List<Employee> GetEmployees()
        {
            var employeesDto = GetEmployeesDto();
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

        private static Employee GetEmployeesById(int id)
        {
            var employeeDto = GetEmployeesDto().FirstOrDefault(i => i.Id == id);
            var employeeFactory = new EmployeeFactory();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EmployeeDto, Employee>(); });
            IMapper iMapper = config.CreateMapper();

            Employee employee = employeeFactory.CreateContract(employeeDto.ContractTypeName);
            iMapper.Map(employeeDto, employee);
            employee.Role = new Role(employeeDto);
            employee.CalculateSalary();

            return employee;
        }

        private static List<EmployeeDto> GetEmployeesDto()
        {
            return new List<EmployeeDto> {
                 new EmployeeDto
                 {
                    ContractTypeName = "HourlySalaryEmployee",
                    HourlySalary = 100,
                    Id = 1,
                    MonthlySalary = 2000,
                    RoleId = 2,
                    RoleName = "Administrator"
                 },
                  new EmployeeDto
                 {
                    ContractTypeName = "HourlySalaryEmployee",
                    HourlySalary = 100,
                    Id = 2,
                    MonthlySalary = 2000,
                    RoleId = 2,
                    RoleName = "Contractor"
                 },
            };
        }
    }
}
