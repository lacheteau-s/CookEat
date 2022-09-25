using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace CookEat.Api.Configuration
{
    public class RouteConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _routePrefix;

        public RouteConvention(IRouteTemplateProvider routeTemplateProvider)
        {
            if (routeTemplateProvider == null)
                throw new ArgumentNullException(nameof(routeTemplateProvider));

            _routePrefix = new AttributeRouteModel(routeTemplateProvider);
        }

        public void Apply(ApplicationModel application)
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            foreach (var controller in application.Controllers)
            {
                var matchedSelectors = controller.Selectors.Where(s => s.AttributeRouteModel != null).ToList();

                if (matchedSelectors.Any())
                    foreach (var selector in matchedSelectors)
                        selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel);

                var unmatchedSelectors = controller.Selectors.Where(s => s.AttributeRouteModel == null).ToList();

                if (unmatchedSelectors.Any())
                    foreach (var selector in unmatchedSelectors)
                        selector.AttributeRouteModel = _routePrefix;
            }
        }
    }
}
