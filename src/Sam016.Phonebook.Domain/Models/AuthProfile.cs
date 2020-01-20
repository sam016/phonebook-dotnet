using System.Collections.Generic;

namespace Sam016.Phonebook.Domain.Models
{
    public class AuthProfile : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public uint UserId { get; set; }
    }
}
