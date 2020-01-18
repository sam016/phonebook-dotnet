using System.Collections.Generic;

namespace Sam016.Phonebook.Domain.Models
{
    public class User : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        // public string Role { get; set; }

        public virtual ICollection<Phonebook> Phonebook { get; set; }

        public User()
        {
            Phonebook = new HashSet<Phonebook>();
        }
    }
}
