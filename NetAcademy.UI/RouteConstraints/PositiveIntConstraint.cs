namespace NetAcademy.UI.RouteConstraints;

public class PositiveIntConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        return values[routeKey] != null 
               && values[routeKey] is int 
                && (int)values[routeKey] >= 0;
    }
}