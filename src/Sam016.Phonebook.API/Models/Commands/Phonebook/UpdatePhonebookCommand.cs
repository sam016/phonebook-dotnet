namespace Sam016.Phonebook.API.Models.Commands.Phonebook
{
    public class UpdatePhonebookCommand : MediatR.IRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
