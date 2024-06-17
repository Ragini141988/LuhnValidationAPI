using CreditCardValidatorAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;

[TestClass]
public class CreditCardControllerTests
{
    private CreditCardController _controller;

    [TestInitialize]
    public void Setup()
    {
        _controller = new CreditCardController();
    }

    [TestMethod]
    public void ValidateCreditCard_ValidVisa_ReturnsTrue()
    {
        // Arrange
        string validVisaCardNumber = "4532015112830366";

        // Act
        var result = _controller.ValidateCreditCard(validVisaCardNumber) as OkNegotiatedContentResult<bool>;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Content);
    }

    [TestMethod]
    public void ValidateCreditCard_ValidDiscover_ReturnsTrue()
    {
        // Arrange
        string validDiscoverCardNumber = "6011514433546201";

        // Act
        var result = _controller.ValidateCreditCard(validDiscoverCardNumber) as OkNegotiatedContentResult<bool>;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Content);
    }

    [TestMethod]
    public void ValidateCreditCard_ValidVisa_Another_ReturnsTrue()
    {
        // Arrange
        string anotherValidVisaCardNumber = "4111111111111111";

        // Act
        var result = _controller.ValidateCreditCard(anotherValidVisaCardNumber) as OkNegotiatedContentResult<bool>;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Content);
    }

    [TestMethod]
    public void ValidateCreditCard_InvalidLuhnNumber_ReturnsFalse()
    {
        // Arrange
        string invalidLuhnCardNumber = "1234567812345678";

        // Act
        var result = _controller.ValidateCreditCard(invalidLuhnCardNumber) as OkNegotiatedContentResult<bool>;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Content);
    }

    [TestMethod]
    public void ValidateCreditCard_Empty_ReturnsBadRequest()
    {
        // Arrange
        string emptyCardNumber = "";

        // Act
        var result = _controller.ValidateCreditCard(emptyCardNumber);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
    }

    [TestMethod]
    public void ValidateCreditCard_NonDigitCharacters_ReturnsBadRequest()
    {
        // Arrange
        string nonDigitCardNumber = "abcd1234abcd5678";

        // Act
        var result = _controller.ValidateCreditCard(nonDigitCardNumber);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
    }
}
