using AybCommerce.Domain.Entities;
using AybCommerce.UI.ViewModels.Util;

namespace AybCommerce.UI.ViewModels.User
{
    public class ProfileViewModel
    {
        public UserInfoViewModel UserInfo { get; set; } = new UserInfoViewModel();

        public AddressInfoViewModel AddresInfo { get; set; } = new AddressInfoViewModel();
    }

    public class UserInfoViewModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string IdentityNumber { get; set; }
    }

    public class AddressInfoViewModel
    {
        public int? AddressId { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

    }

    public class ProfileViewModelBuilder : GenericViewModelBuilder<ProfileViewModel>
    {
        Domain.Entities.User _user;

        public ProfileViewModelBuilder(Domain.Entities.User user)
        {
            _user = user;
        }

        public override ProfileViewModel Build()
        {
            var model = new ProfileViewModel
            {
                UserInfo =
                {
                    Name = _user.Name,
                    Surname = _user.Surname,
                    Email = _user.Email,
                    IdentityNumber = _user.IdentityNumber
                },
                AddresInfo =
                {
                    AddressId = _user.Address?.Id,
                    AddressLine1 = _user.Address?.AddressLine1,
                    AddressLine2 = _user.Address?.AddressLine2,
                    ZipCode = _user.Address?.ZipCode,
                    City = _user.Address?.City,
                    State = _user.Address?.State
                }
            };

            return model;
        }
    }
}
