using MasGlobalTest.Application;
using MasGlobalTest.Application.Employees;
using MasGlobalTest.Common.Exceptions;
using MasGlobalTest.General;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasGlobalTest.Employees
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IQueryHandler<GetEmployeeFilter, IEnumerable<Employee>> _getAllQueryHandler;
        private readonly IQueryHandler<GetEmployeeFilter, Employee> _getByIdQueryHandler;

        public EmployeeController(
            IQueryHandler<GetEmployeeFilter, IEnumerable<Employee>> getAllQueryHandler,
            IQueryHandler<GetEmployeeFilter, Employee> getByIdQueryHandler)
        {
            _getAllQueryHandler = getAllQueryHandler;
            _getByIdQueryHandler = getByIdQueryHandler;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> GetEmployeeAll()
        {
            try
            {
                var employees = await _getAllQueryHandler.HandleAsync(new GetEmployeeFilter());
                return Ok(employees);
            }
            catch (UnavailableExternalServiceException ex)
            {
                return NotFound(ex);
            }
            catch (EntityDoesNotExistException ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetEmployeByIdl([FromRoute] int id)
        {
            try
            {
                var employees = await _getByIdQueryHandler.HandleAsync(new GetEmployeeFilter() { EmployeeId = id });
                return Ok(employees);
            }
            catch (UnavailableExternalServiceException ex)
            {
                return NotFound(ex);
            }
            catch (EntityDoesNotExistException ex)
            {
                return NotFound(ex);
            }
        }
    }
}
