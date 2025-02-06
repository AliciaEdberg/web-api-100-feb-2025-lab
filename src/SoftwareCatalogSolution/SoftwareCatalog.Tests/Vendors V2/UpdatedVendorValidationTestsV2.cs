using FluentValidation.TestHelper;
using SoftwareCatalog.Api.Vendors;
namespace SoftwareCatalog.Tests.Vendors;
public class UpdatedVendorValidationTestsV2
{
    [Theory]
    [InlineData("XXX")]
    [InlineData("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")]
    public void NameMustBeThreeTo100Letters(string name)
    {
        var validator = new UpdatedVendorCreateModelValidator();
        var model = new VendorCreateModel() { Name = name };
        var validations = validator.TestValidate(model);

        Assert.True(validations.IsValid);
    }

    //[Theory]
    //[InlineData(null)]
    //[InlineData("")]
    //[InlineData("X")]
    //[InlineData("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")]
    //public void BadNames(string name)
    //{
    //    var validator = new VendorCreateModelValidator();
    //    var model = new VendorCreateModel() { Name = name };
    //    var validations = validator.TestValidate(model);

    //    Assert.False(validations.IsValid);
    //}

    //[Fact]
    //public void LinksAreOptional()
    //{
    //    var model = new VendorCreateModel() { Name = "bob" };
    //    var validator = new VendorCreateModelValidator();

    //    var validations = validator.TestValidate(model);

    //    Assert.True(validations.IsValid);
    //}
    //[Theory]
    //[InlineData("https://house.com")]
    //[InlineData("https://dog.com")]
    //public void LinksMustBeHttpsIfTheyExist(string link)
    //{

    //    var model = new VendorCreateModel() { Name = "bob", Link = link };
    //    var validator = new VendorCreateModelValidator();

    //    var validations = validator.TestValidate(model);

    //    Assert.True(validations.IsValid);
    //}

    //[Theory]
    //[InlineData("http://house.com")]
    //[InlineData("dog.com")]
    //public void LinksMustBeHttps(string link)
    //{

    //    var model = new VendorCreateModel() { Name = "bob", Link = link };
    //    var validator = new VendorCreateModelValidator();

    //    var validations = validator.TestValidate(model);

    //    Assert.False(validations.IsValid);
    //}




}
