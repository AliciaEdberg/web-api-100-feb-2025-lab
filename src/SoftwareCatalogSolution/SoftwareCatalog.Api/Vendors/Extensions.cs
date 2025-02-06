using FluentValidation;
using SoftwareCatalog.Api.Shared.Catalog;
using SoftwareCatalog.Api.Vendors.Endpoints; // Assuming V1 endpoints are here
using SoftwareCatalog.Api.Vendors.V2.Endpoints; // Assuming V2 endpoints are here

namespace SoftwareCatalog.Api.Vendors
{
    public static class Extensions
    {
        public static IServiceCollection AddVendors(this IServiceCollection services)
        {
            services.AddScoped<VendorSlugGenerator>();
            services.AddScoped<ICheckForUniqueVendorSlugs, VendorDataService>();

            services.AddScoped<IValidator<VendorCreateModel>, UpdatedVendorCreateModelValidator>();
            services.AddScoped<ICheckForVendorExistenceForCatalog, VendorDataService>();

            services.AddAuthorizationBuilder()
                .AddPolicy("canAddVendors", p =>
                {
                    p.RequireRole("manager");
                    p.RequireRole("software-center");
                });

            return services;
        }

        // Mapping for V1 endpoints
        public static IEndpointRouteBuilder MapVendors(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("vendors").WithTags("Approved Vendors").WithDescription("The Approved Vendors for the Company");

            group.MapPost("/", AddingAVendor.CanAddVendorAsync).RequireAuthorization("canAddVendors");
            group.MapGet("/{id}", GettingAVendor.GetVendorAsync).WithTags("Approved Vendors", "Catalog");
            group.MapGet("/", GettingAVendor.GetVendorsAsync);
            return group;
        }

        // Mapping for V2 endpoints
        public static IEndpointRouteBuilder MapVendorsV2(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("v2/vendors").WithTags("Approved Vendors V2").WithDescription("The Approved Vendors for the Company V2");

            group.MapPost("/", AddingAVendorV2.CanAddVendorAsync).RequireAuthorization("canAddVendors");
            group.MapGet("/{id}", GettingAVendorV2.GetVendorAsync).WithTags("Approved Vendors", "Catalog");
            group.MapGet("/", GettingAVendorV2.GetVendorsAsync);
            return group;
        }
    }
}
