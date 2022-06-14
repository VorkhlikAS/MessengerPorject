using System.Collections.Generic;

namespace MessengerNetSix.Models.Interfaces
{
    public interface IContacts
    {
        IEnumerable<Contact> GetUserContacts(string id);
        IEnumerable<Contact> GetNonConfirmedContacts(string id);
    }
}
