using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Controllers;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View() => Assert.IsType<ViewResult>(new HomeController().Index());

        [TestMethod]
        public void SomeAction_Returns_View() => Assert.IsType<ViewResult>(new HomeController().SomeAction());

        [TestMethod]
        public void Blog_Returns_View() => Assert.IsType<ViewResult>(new HomeController().Blog());

        [TestMethod]
        public void BlogSingle_Returns_View() => Assert.IsType<ViewResult>(new HomeController().BlogSingle());

        [TestMethod]
        public void ContactUs_Returns_View() => Assert.IsType<ViewResult>(new HomeController().ContactUs());

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Throw_Thrown_ApplicationException() => Assert.IsType<ViewResult>(new HomeController().Throw("123"));

        [TestMethod]
        public void Throw_Thrown_ApplicationException2()
        {
            var controller = new HomeController();

            const string expected_exception_text = "123";

            var exception = Assert.Throws<ApplicationException>(() => controller.Throw(expected_exception_text));

            Assert.Equal(expected_exception_text, exception.Message);
        }

        [TestMethod]
        public void Error404_Returns_View() => Assert.IsType<ViewResult>(new HomeController().Error404());

        [TestMethod]
        public void ErrorStatus_404_RedirectTo_Error404()
        {
            var controller = new HomeController();

            const string status_code = "404";

            var result = controller.ErrorStatus(status_code);

            var redirect_to_action = Assert.IsType<RedirectToActionResult>(result); //(RedirectToActionResult) result;
            Assert.Null(redirect_to_action.ControllerName);
            Assert.Equal(nameof(HomeController.Error404), redirect_to_action.ActionName);
        }
    }
}
