
namespace Sam016.Phonebook.Domain.Dtos
{
    public class UserDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // **DO NOT** show the password to the user
        // public string Password { get; set; }
    }
}
