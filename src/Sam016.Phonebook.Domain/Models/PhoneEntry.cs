
namespace Sam016.Phonebook.Domain.Models
{
    public class PhoneEntry : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrganizationName { get; set; }
        public string Address { get; set; }
        public int PhonebookId { get; set; }
    }
}
