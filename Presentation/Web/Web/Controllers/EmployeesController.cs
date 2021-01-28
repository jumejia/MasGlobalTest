using MasGlobalTest.Application;
using MasGlobalTest.Application.Employees;
using MasGlobalTest.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Web.General;

namespace Web.Employees
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IQueryHandler<GetEmployeeFilter, IEnumerable<Employee>> _getAllQueryHandler;
        private readonly IQueryHandler<GetEmployeeFilter, Employee> _getByIdQueryHandler;

        public EmployeesController(
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
                return StatusCode((int)HttpStatusCode.ServiceUnavailable, new ErrorModel("ServiceUnavailable", ex.Message));
            }
            catch (EntityDoesNotExistException ex)
            {
                return NotFound(new ErrorModel("NotFound", ex.Message));
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
                return StatusCode((int)HttpStatusCode.ServiceUnavailable, new ErrorModel("ServiceUnavailable", ex.Message));
            }
            catch (EntityDoesNotExistException ex)
            {
                return NotFound(new ErrorModel("NotFound", ex.Message));
            }
        }
    }
}
