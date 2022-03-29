using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.AdapterInbound.Rest.Filter
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .SelectMany(v => v.Value.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

                context.Result = new BadRequestObjectResult(errors);
                return;
            }

            await next();
        }
    }
}
