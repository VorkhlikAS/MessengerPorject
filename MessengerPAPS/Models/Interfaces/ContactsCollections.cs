using System.Collections.Generic;
using System.Linq;

namespace MessengerPAPS.Models.Interfaces
{
    public class ContactsCollections : IContacts
    {
        private readonly ApplicationContext _context;
        public ContactsCollections(ApplicationContext context)
        {
            _context = context;
         
        }

        public IEnumerable<Contact> GetNonConfirmedContacts() => _context.Contacts.Where(c => !c.IsConfirmed && !c.Forbiden).ToList();

        public IEnumerable<Contact> GetUserContacts() => _context.Contacts.Where(p => p.Forbiden == false).ToList();

    }
}
