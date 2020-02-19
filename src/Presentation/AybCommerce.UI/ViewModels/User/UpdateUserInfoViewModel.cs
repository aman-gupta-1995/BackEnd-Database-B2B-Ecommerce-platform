namespace AybCommerce.UI.ViewModels.User
{
    public class UpdateUserInfoViewModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string IdentityNumber { get; set; }

        public Domain.Entities.User BuildUser(string userId)
        {
            return new Domain.Entities.User
            {
                Id = userId,
                Name = Name,
                Surname = Surname,
                IdentityNumber = IdentityNumber
            };
        }
    }
}
