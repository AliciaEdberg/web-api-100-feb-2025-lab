using Riok.Mapperly.Abstractions;

namespace SoftwareCatalog.Api.Vendors.V2
{
    [Mapper]
    public static partial class VendorMappersV2
    {
        public static IQueryable<VendorDetailsResponseModel> ProjectToModel(this IQueryable<Vendor2Entity> entities)
        {
            return entities.Select(entity => new VendorDetailsResponseModel
            {
                Id = entity.Slug,
                Name = entity.Name,
                Link = entity.Link,
                CreatedOn = entity.CreatedOn
            });
        }

        [MapProperty(nameof(Vendor2Entity.Slug), nameof(VendorDetailsResponseModel.Id))]
        [MapperIgnoreSource(nameof(Vendor2Entity.Id))]
        public static partial VendorDetailsResponseModel MapToModel(this Vendor2Entity entity);
    }
}