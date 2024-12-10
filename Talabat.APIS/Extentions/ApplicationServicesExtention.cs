using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Helpers;
using Talabat.Repository.Data;
using Talabat.Repository;
using Talabate.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Talabat.APIS.Errors;

namespace Talabat.APIS.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            #region Configer Services & Services to the Container 

            //builder.Services.AddScoped<IGenaricRepository<Product>,GenericRepository<Product>>();
            Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenericRepository<>));

            // builder.Services.AddAutoMapper(M=>M.AddProfile(new MappingProfile()));
            Services.AddAutoMapper(typeof(MappingProfile));
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory =(actioncontext) =>
                {
                    var errors = actioncontext.ModelState.Where(p => p.Value.Errors.Count()>0)
                    .SelectMany(p => p.Value.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray();
                    var validationErrorResponse = new ApiValidtionErorrResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });

            #endregion

            return Services;

        }
    }
}
