using System.Collections.Generic;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Interfaces.Builders
{
    public interface IClientContactNetworkBuilder
    {
        void CreatePrimary(Contact primaryContact);
        void AddSecondaryContact(Contact secondaryContact);
        void AddSecondaryContacts(IEnumerable<Contact> secondaryContacts);
        IEnumerable<ClientContactNetwork> Build();
    }
}