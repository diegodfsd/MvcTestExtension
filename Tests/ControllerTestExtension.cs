using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    public static class ControllerTestExtension
    {
        public static void ActionHasAttribute<TController>(this ControllerBase controller, Expression<Action<TController>> @action, Type filterType)
            where TController : ControllerBase
        {
            var method = ((MethodCallExpression)@action.Body).Method.Name;
            ActionIsAnnotatedWithAttribute(controller, method, filterType);
        }

        private static void ActionIsAnnotatedWithAttribute(ControllerBase controller, string methodName, Type filterType)
        {
            var actions = controller
                .GetType()
                .GetMethods()
                .Where(act => act.Name.Equals(methodName, StringComparison.InvariantCultureIgnoreCase));

            foreach (var action in actions)
            {
                Assert.IsNotNull(methodName);
                Assert.IsTrue(action.IsPublic, string.Format("The action {0} should be public", action.Name));
            }

            var result = actions
                .Any(act => act.GetCustomAttributes(filterType, false).Length > 0);

            Assert.IsTrue(result);
        }
    }
}
