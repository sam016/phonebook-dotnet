namespace Sam016.Phonebook.API.Models.Requests.PhoneEntry
{
    public class UpdatePhoneEntryRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrganizationName { get; set; }
        public string Address { get; set; }
        public System.DateTime Birthday { get; set; }
    }
}
