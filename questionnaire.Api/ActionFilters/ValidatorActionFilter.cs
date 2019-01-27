using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace questionnaire.Api.ActionFilters {
    public class ValidatorActionFilter : IActionFilter {
        public void OnActionExecuting (ActionExecutingContext filterContext) {
            if (!filterContext.ModelState.IsValid) {
                filterContext.Result = new BadRequestObjectResult (filterContext.ModelState);
            }
        }

        public void OnActionExecuted (ActionExecutedContext context) { }
    }
}