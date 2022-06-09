using System.Collections.Generic;

namespace MessengerPAPS.Models.Interfaces
{
    public interface IContacts
    {
        IEnumerable<Contact> GetUserContacts();
        IEnumerable<Contact> GetNonConfirmedContacts();
    }
}
