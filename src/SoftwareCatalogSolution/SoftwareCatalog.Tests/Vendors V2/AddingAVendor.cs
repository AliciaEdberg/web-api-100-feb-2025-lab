

using Alba;
using Alba.Security;
using SoftwareCatalog.Api.Vendors;
using System.Security.Claims;

namespace SoftwareCatalog.Tests.Vendors.V2;

[Trait("Category", "System")]

public class AddingAVendorV2
{
    [Fact]
    public async Task CanAddAVendor()
    {
        var fakeIdentity = new AuthenticationStub().WithName("babs")
            .With(new Claim(ClaimTypes.Role, "manager"))
            .With(new Claim(ClaimTypes.Role, "software-center"));

        var host = await AlbaHost.For<Program>(fakeIdentity);

        var requestModel = new VendorCreateModel
        {
            Name = "Jetbrains",
            Link = "https://jetbrains.com"
        };

        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(requestModel).ToUrl("/v2/vendors");
            api.StatusCodeShouldBe(201);
        });

        var location = postResponse.Context.Response.Headers.Location.ToString();

        var postBody = postResponse.ReadAsJson<VendorDetailsResponseModel>();

        Assert.NotNull(postBody);

        var getResponse = await host.Scenario(api =>
        {
            api.Get.Url(location);
        });

        var getBody = getResponse.ReadAsJson<VendorDetailsResponseModel>();
        Assert.NotNull(getBody);

        var ts = postBody.CreatedOn - getBody.CreatedOn;

        Assert.Equal(postBody, getBody);

    }

    [Fact]
    public async Task InputsAreValidated()
    {
        var fakeIdentity = new AuthenticationStub().WithName("babs")
            .With(new Claim(ClaimTypes.Role, "manager"))
            .With(new Claim(ClaimTypes.Role, "software-center"));
        var host = await AlbaHost.For<Program>(fakeIdentity);




        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(new { }).ToUrl("/v2/vendors");
            api.StatusCodeShouldBe(400);
        });
    }
}
