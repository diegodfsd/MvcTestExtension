using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    public static class ActionTestExtension
    {
        public static ViewResult ReturnsViewResult(this ActionResult actionResult)
        {
            return (ViewResult)actionResult;
        }

        public static RedirectToRouteResult ReturnsRedirectToRouteResult(this ActionResult actionResult)
        {
            return (RedirectToRouteResult)actionResult;
        }

        public static T ViewData<T>(this ActionResult actionResult, string chave)
        {
            return (T)actionResult.ReturnsViewResult().ViewData[chave];
        }

        public static ViewResult WithName(this ViewResult viewResult, string viewName)
        {
            if (!viewResult.ViewName.Equals(viewName))
            {
                throw new Exception("ViewName is different.");
            }
            return viewResult;
        }

        public static TModel ShouldBeTrue<TModel>(this TModel @actual, Predicate<TModel> predicate)
        {
            Assert.IsTrue(predicate(@actual));
            return @actual;
        }

        public static void ShouldNotBeNull<TModel>(this TModel @object)
        {
            Assert.IsNotNull(@object);
        }

        public static TModel ShouldBeEqual<TModel, TProperty>(this TModel model, object expected, Func<TModel, TProperty> readProperty)
        {
            if(!expected.Equals(readProperty(model)))
            {
                throw new Exception("Not equal");
            }

            return model;
        }

        public static RedirectToRouteResult ToAction<TController>(this RedirectToRouteResult redirectToRouteResult, Expression<Action<TController>> @action)
        {
            var method = ((MethodCallExpression)@action.Body).Method.Name;

            return redirectToRouteResult
                .WithRouteValue("action", method);
        }

        public static RedirectToRouteResult ToController<TController>(this RedirectToRouteResult redirectToRouteResult)
        {
            var controllerName = typeof(TController).Name.Replace("Controller", "");
            return redirectToRouteResult
                .WithRouteValue("controller", controllerName);
        }

        public static TModel WithModel<TModel>(this ViewResult viewResult) 
            where TModel : class
        {
            TModel model = viewResult.ViewData.Model as TModel;

            if(model == null)
            {
                throw new NullReferenceException(string.Concat("The Model is not the type expected:", typeof(TModel).Name));
            }
            return model;
        }

        public static ViewResult WithModelStateValue(this ViewResult viewResult, string key, string message)
        {
            var modelStateDictionary = viewResult.ViewData.ModelState;
            if (modelStateDictionary[key] == null)
            {
                throw new ArgumentNullException(string.Format("ModelState key: {0} not found.", key));
            }

            if (!modelStateDictionary[key].Errors.Any(error => error.ErrorMessage.Equals(message)))
            {
                throw new Exception("Error message not found.");
            }

            return viewResult;
        }

        public static RedirectToRouteResult WithRouteValue(this RedirectToRouteResult redirectToRouteResult, string key, object value)
        {
            if (redirectToRouteResult.RouteValues[key] == null)
            {
                throw new ArgumentNullException(string.Format("Route value {0} cannot be null.", key));
            }

            if (!redirectToRouteResult.RouteValues[key].Equals(value))
            {
                throw new Exception("The route value is different of the expected.");
            }

            return redirectToRouteResult;
        }
    }
}
