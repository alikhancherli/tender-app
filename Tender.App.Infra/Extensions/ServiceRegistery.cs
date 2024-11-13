using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Tender.App.Application.Commands;
using Tender.App.Application.Queries;
using Tender.App.Domain.Repositories;
using Tender.App.Infra.Implementations;

namespace Tender.App.Infra.Extensions;

public static class ServiceRegistery
{
    public static IServiceCollection RegisterAllServices(this IServiceCollection services)
    {
        services.AddMediatR(opt => opt.RegisterServicesFromAssembly(typeof(AddBidCommand).Assembly));
        services.AddValidatorsFromAssemblyContaining<GetBidByIdQuery>();
        services.AddScoped<IBidRepository, BidRepository>();

        return services;
    }

}
