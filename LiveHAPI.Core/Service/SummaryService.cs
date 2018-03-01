using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Service
{
    public class SummaryService:ISummaryService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IClientSummaryRepository _clientSummaryRepository;
        private readonly IUserSummaryRepository _userSummaryRepository;
        private readonly IEncounterRepository _encounterRepository;

        public SummaryService(IItemRepository itemRepository, IClientSummaryRepository clientSummaryRepository, IUserSummaryRepository userSummaryRepository, IEncounterRepository encounterRepository)
        {
            _itemRepository = itemRepository;
            _clientSummaryRepository = clientSummaryRepository;
            _userSummaryRepository = userSummaryRepository;
            _encounterRepository = encounterRepository;
        }

        public IEnumerable<ClientSummary> Generate(Client client)
        {
            _clientSummaryRepository.DeleteByClient(client.Id);

            var clientSummaries = new List<ClientSummary>();
            int rank = 1;
            
            //HTS Enrollment

            if (client.Identifiers.Any())
            {
                var id = client.Identifiers.OrderByDescending(x => x.RegistrationDate).FirstOrDefault();
                if (null != id)
                    clientSummaries.Add(new ClientSummary("HTS Enrollment",$"{id.IdentifierTypeId}:{id.Identifier}", id.RegistrationDate, rank, client.Id)); rank++;
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
                        clientSummaries.Add(new ClientSummary("Testing", GetLookupName(id.FinalResult),testingEncounter.EncounterDate, rank, client.Id)); rank++;
                        clientSummaries.Add(new ClientSummary("PNS Accepted", GetLookupName(id.SelfTestOption), testingEncounter.EncounterDate, rank, client.Id)); rank++;
                    }
                }


                var linkageEncounter = encounters.Where(x => x.EncounterTypeId == new Guid("b262fc32-852f-11e7-bb31-be2e44b06b34"))
                    .OrderByDescending(x => x.EncounterDate).FirstOrDefault();

                if (null != linkageEncounter && linkageEncounter.ObsFinalTestResults.Any())
                {
                    var id = linkageEncounter.ObsLinkages.FirstOrDefault();
                    if (null != id)
                    {
                        clientSummaries.Add(new ClientSummary("Referred To",id.ReferredTo, linkageEncounter.EncounterDate, rank, client.Id)); rank++;
                        clientSummaries.Add(new ClientSummary("Linked To", id.FacilityHandedTo, linkageEncounter.EncounterDate, rank, client.Id)); rank++;
                        clientSummaries.Add(new ClientSummary("CCC Enrollment", id.EnrollmentId, id.DateEnrolled, rank, client.Id));
                    }
                }

                //Linkage
            }

            if (clientSummaries.Count > 0)
            {
                _clientSummaryRepository.Insert(clientSummaries);
                _clientSummaryRepository.Save();
            }
            return clientSummaries;
        }

        public IEnumerable<UserSummary> Generate(Guid userId)
        {
            _userSummaryRepository.DeleteByProvider(userId);
            var userSummaries = new List<UserSummary>();
            int rank = 1;
            var encounters = _encounterRepository.LoadByUser(userId).ToList();
            var testingEncounter = encounters.SelectMany(x => x.ObsPartnerScreenings).ToList();

            /*            
                Partners identified
                                
                Partners tested
                Partners who tested positive
                Partners linked to care 
             */

            //  Index clients screened
            var indexClientsScreened = testingEncounter.Select(x => x.IndexClientId).Distinct().Count();
            userSummaries.Add(new UserSummary("Index clients screened", indexClientsScreened, rank, userId));
            rank++;

            //  Partners known positive
            var partnersKnownPositive = testingEncounter.Count(x => x.HivStatus==new Guid("B25EFD8A-852F-11E7-BB31-BE2E44B06B34"));
            userSummaries.Add(new UserSummary("Partners known positive", partnersKnownPositive, rank, userId));
            rank++;

            //  Partners Eligible for testing
            var partnersEligibleForTesting = testingEncounter.Count(x => x.Eligibility == new Guid("b25eccd4-852f-11e7-bb31-be2e44b06b34"));
            userSummaries.Add(new UserSummary("Partners Eligible for testing", partnersEligibleForTesting, rank, userId));
            rank++;


            if (userSummaries.Count > 0)
            {
                _userSummaryRepository.Insert(userSummaries);
                _userSummaryRepository.Save();
            }

            return userSummaries;
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