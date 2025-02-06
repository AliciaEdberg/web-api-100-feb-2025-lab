

using NSubstitute;
using SoftwareCatalog.Api.Vendors;

namespace SoftwareCatalog.Tests.Vendors.V2;

[Trait("Category", "Unit")]
[Trait("Feature", "Vendors")]
[Trait("Bug", "938938")]
public class GeneratingSlugs
{
    [Theory]
    [InlineData("Microsoft", "microsoft")]
    [InlineData("IBM Corporation, Inc", "ibm-corporation-inc")]
    [InlineData("  tacos    are    good", "tacos-are-good")]
    public async Task CanGenerateSlugs(string vendorName, string slug)
    {
        var fakeSlugChecker = Substitute.For<ICheckForUniqueVendorSlugs>();
        fakeSlugChecker.CheckUniqueSlug(Arg.Any<string>()).Returns(true);
        var slugGenerator = new VendorSlugGenerator(fakeSlugChecker);

        string result = await slugGenerator.GenerateSlugFor(vendorName);

        Assert.Equal(slug, result);
    }


    [Theory]
    [InlineData("Microsoft", "microsoft-4")] // if we already have microsoft, microsoft-1, microsoft-2, microsoft-3
    [InlineData("IBM Corporation, Inc", "ibm-corporation-inc-1")]
    public async Task CanGenerateUniqueSlugs(string vendorName, string slug)
    {
        var fakeSlugChecker = Substitute.For<ICheckForUniqueVendorSlugs>();
        fakeSlugChecker.CheckUniqueSlug(slug).Returns(true);
        var slugGenerator = new VendorSlugGenerator(fakeSlugChecker);

        string result = await slugGenerator.GenerateSlugFor(vendorName);

        Assert.Equal(slug, result);
    }

    [Fact]
    public async Task UsesAGuidForTooManyTries()
    {
        var fakeSlugChecker = Substitute.For<ICheckForUniqueVendorSlugs>();
        var slugGenerator = new VendorSlugGenerator(fakeSlugChecker);

        var result = await slugGenerator.GenerateSlugFor("beer");

        var expectedResult = $"beer-{Guid.Empty}";

        Assert.Equal(expectedResult.Length, result.Length);
    }

}

//public class FakeSlugCheckerThingy : ICheckForUniqueVendorSlugs
//{
//    public Task<bool> CheckUniqueSlug(string slug)
//    {
//        return Task.FromResult(true);
//    }
//}
