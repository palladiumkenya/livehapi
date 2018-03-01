using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Service
{
    public class ClientSummaryService:IClientSummaryService
    {
        private readonly IItemRepository _itemRepository;

        public ClientSummaryService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public IEnumerable<ClientSummary> Generate(Client client)
        {
            var list = new List<ClientSummary>();
            int rank = 1;
            
            //HTS Enrollment

            if (client.Identifiers.Any())
            {
                var id = client.Identifiers.OrderByDescending(x => x.RegistrationDate).FirstOrDefault();
                if (null != id)
                    list.Add(new ClientSummary("HTS Enrollment",$"{id.IdentifierTypeId}:{id.Identifier}", id.RegistrationDate, rank)); rank++;
            }
            var encounters = client.Encounters.ToList();
            if (encounters.Any())
            {
                //Testing

                var testingEncounter = encounters.Where(x => x.EncounterTypeId == new Guid("b262f4ee-852f-11e7-bb31-be2e44b06b34"))
                    .OrderByDescending(x => x.EncounterDate).FirstOrDefault();

                if (null != testingEncounter && testingEncounter.ObsFinalTestResults.Any())
                {
                    var id = testingEncounter.ObsFinalTestResults.FirstOrDefault();
                    if (null != id)
                    {
                        list.Add(new ClientSummary("Testing", GetLookupName(id.FinalResult),testingEncounter.EncounterDate, rank)); rank++;
                        list.Add(new ClientSummary("PNS Accepted", GetLookupName(id.SelfTestOption), testingEncounter.EncounterDate, rank)); rank++;
                    }
                }


                var linkageEncounter = encounters.Where(x => x.EncounterTypeId == new Guid("b262fc32-852f-11e7-bb31-be2e44b06b34"))
                    .OrderByDescending(x => x.EncounterDate).FirstOrDefault();

                if (null != linkageEncounter && linkageEncounter.ObsFinalTestResults.Any())
                {
                    var id = linkageEncounter.ObsLinkages.FirstOrDefault();
                    if (null != id)
                    {
                        list.Add(new ClientSummary("Referred To",id.ReferredTo, linkageEncounter.EncounterDate, rank)); rank++;
                        list.Add(new ClientSummary("Linked To", id.FacilityHandedTo, linkageEncounter.EncounterDate, rank)); rank++;
                        list.Add(new ClientSummary("CCC Enrollment", id.EnrollmentId, id.DateEnrolled, rank));
                    }
                }

                //Linkage
            }

            return list;
        }

        private string GetLookupName(Guid? itemId)
        {
            if (null == itemId)
                return string.Empty;

            var item = _itemRepository.Get(itemId.Value);
            return null != item ? item.Display : string.Empty;
        }
    }
}