
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WazeCreditGreen.Data.Repository;
using WazeCreditGreen.Data.Repository.IRepository;
using WazeCreditGreen.Models;
using WazeCreditGreen.Service;
using WazeCreditGreen.Service.LifeTimeExample;
using WazeCreditGreen.Utility.AppSettingClasses;

namespace WazeCreditGreen.Utility.DIConfig {
    public static class ConfigureDIServices {
        public static IServiceCollection AddAllServices(this IServiceCollection services) {

            services.AddTransient<IMarketForecaster, MarketForecaster>();
            services.TryAddEnumerable(new[]
            {
                ServiceDescriptor.Transient<IValidationChecker, CreditValidationChecker>(),
                ServiceDescriptor.Transient<IValidationChecker, AddressValidationChecker>()
            });


            services.AddScoped<ICreditValidator, CreditValidator>();

            services.AddTransient<TransientService>();
            services.AddTransient<SingletonService>();
            services.AddTransient<ScopedService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<CreditApprovedHigh>();
            services.AddScoped<CreditApprovedLow>();

            services.AddScoped<Func<CreditApprovedEnum, ICreditApproved>>(ServiceProvider => range => {
                switch (range) {
                    case CreditApprovedEnum.Low:
                        return ServiceProvider.GetService<CreditApprovedLow>();
                    case CreditApprovedEnum.High:
                        return ServiceProvider.GetService<CreditApprovedHigh>();
                    default: return ServiceProvider.GetService<CreditApprovedLow>();
                }
            });
            //builder.Services.AddScoped<IValidationChecker, CreditValidationChecker>();
            //builder.Services.AddScoped<IValidationChecker, AddressValidationChecker>();
            //decapreated - only for demo purposes
            // builder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<IValidationChecker, AddressValidationChecker>());
            // builder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<IValidationChecker, CreditValidationChecker>());

            return services;

        }
    }
}