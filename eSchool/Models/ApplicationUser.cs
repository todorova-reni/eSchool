using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace eSchool.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        [Required]
        [MinLength(10)]
        [Phone]
        public string MobilePhone { get; set; }
        
        public string Address { get; set; }

        [Required]
        public string Role { get; set; }

        public int Active { get; set; }

        [Required]
        public DateTime Created_At { get; set; }

        public DateTime Updated_At { get; set; }

        public string Updated_By { get; set; }
    }
}
