"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EmployeeService = void 0;
//@Injectable()
var EmployeeService = /** @class */ (function () {
    function EmployeeService(http) {
        this.http = http;
        this.baseUrl = 'http://localhost:57775';
    }
    EmployeeService.prototype.getEmployees = function () {
        return this.http.get(this.baseUrl + '/api/employees');
    };
    EmployeeService.prototype.getEmployee = function (id) {
        return this.http.get(this.baseUrl + '/api/employees/' + id);
    };
    return EmployeeService;
}());
exports.EmployeeService = EmployeeService;
//# sourceMappingURL=employee.service.js.map