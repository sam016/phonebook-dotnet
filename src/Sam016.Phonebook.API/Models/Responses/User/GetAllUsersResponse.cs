using System.Collections.Generic;

namespace Sam016.Phonebook.API.Models.Responses.User
{
    /// <summary>
    /// List of all the Users
    /// </summary>
    public class GetAllUsersResponse : BaseResponseData<IEnumerable<Sam016.Phonebook.Domain.Dtos.UserDto>>
    {
    }
}
