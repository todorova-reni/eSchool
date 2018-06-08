using System;

namespace eSchool.Models.ProfileViewModels
{
    public class DisplayProfileViewModel
    {
        public string Email { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string MobilePhone { get; set; }
        
        public string Address { get; set; }
        
        public string Role { get; set; }

        public int Active { get; set; }
        

    public DateTime Created_At { get; set; }

        public DateTime Updated_At { get; set; }

        public string Updated_By { get; set; }
    }
}
