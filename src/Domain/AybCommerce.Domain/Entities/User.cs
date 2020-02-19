using AybCommerce.Domain.Enumerations;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AybCommerce.Domain.Entities
{
    public class User : IdentityUser
    {
        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        //{
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

        //    return userIdentity;
        //}


        public override string Id { get => base.Id; set => base.Id = value; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Surname { get; set; }

        [StringLength(11)]
        [Column(TypeName = "char(11)")]
        public string IdentityNumber { get; set; }

        public EntityStatus Status { get; set; }

        [Obsolete("I think I should delete it")]
        public int? AddressId { get; set; }

        #region [ Navigation Properties ]

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        #endregion
    }
}
