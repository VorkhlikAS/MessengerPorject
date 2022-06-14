using System.Collections.Generic;

namespace MessengerNetSix.Models.Interfaces
{
    public interface IContacts
    {
        IEnumerable<Contact> GetUserContacts();
        IEnumerable<Contact> GetNonConfirmedContacts();
    }
}
