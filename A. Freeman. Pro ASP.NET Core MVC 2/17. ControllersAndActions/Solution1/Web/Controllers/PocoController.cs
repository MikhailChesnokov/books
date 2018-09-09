namespace Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    // POCO - plain old CLR object
    // Any public class whose name ends with Controller
    public class PocoController
    {
        [ControllerContext]
        public ControllerContext ControllerContext { get; set; }

        
        
        // Any public method
        public string Index() => "This is a POCO controller";
        
        public ViewResult Index2() => new ViewResult
        {
            ViewName = "Result",
            ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = "This is a POCO controller"
            }
        };

        public ViewResult Index3()
        {
            var httpContext = ControllerContext.HttpContext;
            var modelState = ControllerContext.ModelState;
            // ...

            return null;
        }
    }
}