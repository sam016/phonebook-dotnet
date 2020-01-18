using System.Collections.Generic;

namespace Sam016.Phonebook.Domain.Models
{
    public class Phonebook : BaseModel
    {
        public string Name { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<PhoneEntry> PhoneEntry { get; set; }

        public Phonebook()
        {
            PhoneEntry = new HashSet<PhoneEntry>();
        }
    }
}
