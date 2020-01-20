
using System;

namespace Sam016.Phonebook.Domain.Dtos
{
    public class PhoneEntryDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryCode { get; set; }
        public string Phone { get; set; }
        public string OrganizationName { get; set; }
        public string Address { get; set; }
        // public DateTime Birthday { get; set; }
        public uint PhonebookId { get; set; }
    }
}
