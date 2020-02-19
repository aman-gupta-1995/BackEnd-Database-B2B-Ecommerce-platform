using System.Collections.Generic;
using System.Linq;
using AybCommerce.UI.ViewModels.Util;

namespace AybCommerce.UI.ViewModels.User
{
    public class UsersViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string CurrentAccount { get; set; }
    }

    public class UsersViewModelBuilder : GenericViewModelBuilder<List<UsersViewModel>>
    {
        List<Domain.Entities.User> _users;

        public UsersViewModelBuilder(List<Domain.Entities.User> users)
        {
            _users = users;
        }

        public override List<UsersViewModel> Build()
        {
            return _users.Select(user => new UsersViewModel
            {
                UserId = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Status = user.Status.ToString(),
            }).ToList();

            var model = new List<UsersViewModel>();

            foreach (var user in _users)
            {
                model.Add(new UsersViewModel
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Status = user.Status.ToString(),
                });
            }

            return model;
        }
    }
}
