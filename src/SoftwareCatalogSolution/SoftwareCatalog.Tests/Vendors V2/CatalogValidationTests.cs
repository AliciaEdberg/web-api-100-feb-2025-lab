using FluentValidation.TestHelper;
using SoftwareCatalog.Api.Vendors;
namespace SoftwareCatalog.Tests.Vendors;

[Trait("Category", "Unit")]
public class VendorValidationTestsV2
{
    [Theory]
    [InlineData("XXX")]
    [InlineData("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")]
    public void NameMustBeThreeTo100Letters(string name)
    {
        var validator = new VendorCreateModelValidator();
        var model = new VendorCreateModel() { Name = name };
        var validations = validator.TestValidate(model);

        Assert.True(validations.IsValid);
    }

    [Theory]
#pragma warning disable xUnit1012 // Null should only be used for nullable parameters
    [InlineData(null)]
#pragma warning restore xUnit1012 // Null should only be used for nullable parameters
    [InlineData("")]
    [InlineData("X")]
    [InlineData("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")]
    public void BadNames(string name)
    {
        var validator = new VendorCreateModelValidator();
        var model = new VendorCreateModel() { Name = name };
        var validations = validator.TestValidate(model);

        Assert.False(validations.IsValid);
    }

    [Fact]
    public void LinksAreOptional()
    {
        var model = new VendorCreateModel() { Name = "bob" };
        var validator = new VendorCreateModelValidator();

        var validations = validator.TestValidate(model);

        Assert.True(validations.IsValid);
    }
    [Theory]
    [InlineData("https://house.com")]
    [InlineData("https://dog.com")]
    public void LinksMustBeHttpsIfTheyExist(string link)
    {

        var model = new VendorCreateModel() { Name = "bob", Link = link };
        var validator = new VendorCreateModelValidator();

        var validations = validator.TestValidate(model);

        Assert.True(validations.IsValid);
    }

    [Theory]
    [InlineData("http://house.com")]
    [InlineData("dog.com")]
    public void LinksMustBeHttps(string link)
    {

        var model = new VendorCreateModel() { Name = "bob", Link = link };
        var validator = new VendorCreateModelValidator();

        var validations = validator.TestValidate(model);

        Assert.False(validations.IsValid);
    }




}
