﻿using MasGlobalTest.Application;
using MasGlobalTest.Application.Employees;
using MasGlobalTest.Domain;
using MasGlobalTest.ExternalServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Web
{
    public static class StartupServicesConfiguration
    {
        public static void ConfigureQueryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IApiBroker, ApiBroker>();

            services.AddScoped<IQueryHandler<GetEmployeeFilter, IEnumerable<Employee>>, GetAllQueryHandler>();
            services.AddScoped<IQueryHandler<GetEmployeeFilter, Employee>, GetByIdQueryHandler>();

            services.Configure<ExternalServiceConfigurationSettings>(configuration.GetSection("ExternalServiceConfiguration"));
        }
    }
}
