
namespace Sam016.Phonebook.Domain.Models
{
    public class PhoneEntry : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryCode { get; set; }
        public string Phone { get; set; }
        public string OrganizationName { get; set; }
        public string Address { get; set; }
        public uint PhonebookId { get; set; }

        public virtual Phonebook Phonebook { get; set; }
    }
}
