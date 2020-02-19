using AybCommerce.Domain.Enumerations;

namespace AybCommerce.UI.ViewModels.User
{
    public class UpdateUserStatusViewModel
    {
        public string UserId { get; set; }

        public EntityStatus StatusId { get; set; }
    }
}
