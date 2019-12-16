using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using TOP.API.Controllers;
using TOP.API.Data.Helpers;
using TOP.API.Service;
using TOP.Library.Data.models;
using Xunit;

namespace TOP_API_Tests
{
    public class AuthControllerTest
    {
        AuthController _authController;
        IAuthService _authService;
        public AuthControllerTest()
        {
            _authService = new AuthServiceFake();
            _authController = new AuthController(_authService);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _authController.GetAccounts();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _authController.GetAccount(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testGuid = new Guid("C77FBF8E-5B63-4FF0-E045-08D77985E5CB");

            // Act
            var okResult = _authController.GetAccount(testGuid);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testGuid = new Guid("C77FBF8E-5B63-4FF0-E045-08D77985E5CB");

            // Act
            var okResult = _authController.GetAccount(testGuid) as OkObjectResult;

            // Assert
            Assert.IsType<Account>(okResult.Value);
            Assert.Equal(testGuid, (okResult.Value as Account).Id);
        }

        //[Fact]
        //public void Add_InvalidObjectPassed_ReturnsBadRequest()
        //{
        //    // Arrange
        //    var nameMissingItem = new ShoppingItem()
        //    {
        //        Manufacturer = "Guinness",
        //        Price = 12.00M
        //    };
        //    _controller.ModelState.AddModelError("Name", "Required");

        //    // Act
        //    var badResponse = _controller.Post(nameMissingItem);

        //    // Assert
        //    Assert.IsType<BadRequestObjectResult>(badResponse);
        //}


        //[Fact]
        //public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        //{
        //    // Arrange
        //    ShoppingItem testItem = new ShoppingItem()
        //    {
        //        Name = "Guinness Original 6 Pack",
        //        Manufacturer = "Guinness",
        //        Price = 12.00M
        //    };

        //    // Act
        //    var createdResponse = _controller.Post(testItem);

        //    // Assert
        //    Assert.IsType<CreatedAtActionResult>(createdResponse);
        //}


        //[Fact]
        //public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        //{
        //    // Arrange
        //    var testItem = new ShoppingItem()
        //    {
        //        Name = "Guinness Original 6 Pack",
        //        Manufacturer = "Guinness",
        //        Price = 12.00M
        //    };

        //    // Act
        //    var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
        //    var item = createdResponse.Value as ShoppingItem;

        //    // Assert
        //    Assert.IsType<ShoppingItem>(item);
        //    Assert.Equal("Guinness Original 6 Pack", item.Name);
        //}

        //[Fact]
        //public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        //{
        //    // Arrange
        //    var notExistingGuid = Guid.NewGuid();

        //    // Act
        //    var badResponse = _controller.Remove(notExistingGuid);

        //    // Assert
        //    Assert.IsType<NotFoundResult>(badResponse);
        //}

        //[Fact]
        //public void Remove_ExistingGuidPassed_ReturnsOkResult()
        //{
        //    // Arrange
        //    var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

        //    // Act
        //    var okResponse = _controller.Remove(existingGuid);

        //    // Assert
        //    Assert.IsType<OkResult>(okResponse);
        //}
        //[Fact]
        //public void Remove_ExistingGuidPassed_RemovesOneItem()
        //{
        //    // Arrange
        //    var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

        //    // Act
        //    var okResponse = _controller.Remove(existingGuid);

        //    // Assert
        //    Assert.Equal(2, _service.GetAllItems().Count());
        //}
    }
}
