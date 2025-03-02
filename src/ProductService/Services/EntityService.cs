using ProductService.Entities;

namespace ProductService.Services
{
    public class EntityService
    {
        public void SetCreatedProperties(BaseEntity entity, string createdBy)
        {
            var user = string.IsNullOrEmpty(createdBy) ? "Admin User" : createdBy;
            var now = DateTime.UtcNow;

            entity.CreatedBy = user;
            entity.CreatedOn = now;
            entity.UpdatedBy = user;
            entity.UpdatedOn = now;
        }

        public void SetUpdatedProperties(BaseEntity entity, string updatedBy)
        {
            entity.UpdatedBy = string.IsNullOrEmpty(updatedBy) ? "Admin User" : updatedBy;
            entity.UpdatedOn = DateTime.UtcNow;
        }
    }
}