using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Studio;
using LiveHAPI.Shared.Custom;
using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ContactsEncounterRepository : BaseRepository<Encounter, Guid>, IContactsEncounterRepository
    {
        private readonly List<EncounterType> _encounterTypes;

        public ContactsEncounterRepository(LiveHAPIContext context) : base(context)
        {
            _encounterTypes = context.EncounterTypes.AsNoTracking().ToList();
        }

        public IEnumerable<Encounter> GetFamilyScreening(Guid? clientId = null)
        {
            var encounters = Context.Encounters
                .Where(x => x.EncounterTypeId == GetEncounterTypeId("Family Screening"))
                .Include(x => x.ObsMemberScreenings)
                .AsNoTracking();

            if (!clientId.IsNullOrEmpty())
                encounters = encounters.Where(x => x.ClientId == clientId).AsNoTracking();

            return encounters;
        }

        public IEnumerable<Encounter> GetFamilyTracing(Guid? clientId = null)
        {
            var encounters = Context.Encounters
                .Where(x => x.EncounterTypeId == GetEncounterTypeId("Family Trace"))
                .Include(x => x.ObsFamilyTraceResults)
                .AsNoTracking();

            if (!clientId.IsNullOrEmpty())
                encounters = encounters.Where(x => x.ClientId == clientId).AsNoTracking();

            return encounters;
        }

        public IEnumerable<Encounter> GetPartnerScreening(Guid? clientId = null)
        {
            var encounters = Context.Encounters
                .Where(x => x.EncounterTypeId == GetEncounterTypeId("Partner Screening"))
                .Include(x => x.ObsPartnerScreenings)
                .AsNoTracking();

            if (!clientId.IsNullOrEmpty())
                encounters = encounters.Where(x => x.ClientId == clientId).AsNoTracking();

            return encounters;
        }

        public IEnumerable<Encounter> GetPartnerTracing(Guid? clientId = null)
        {
            var encounters = Context.Encounters
                .Where(x => x.EncounterTypeId == GetEncounterTypeId("Partner Trace"))
                .Include(x => x.ObsPartnerTraceResults)
                .AsNoTracking();

            if (!clientId.IsNullOrEmpty())
                encounters = encounters.Where(x => x.ClientId == clientId).AsNoTracking();

            return encounters;
        }

        private Guid GetEncounterTypeId(string name)
        {

            if (null == _encounterTypes||!_encounterTypes.Any())
                throw new ArgumentException($"Encounter Types could not be loaded");


            var encounterType = _encounterTypes.FirstOrDefault(x => x.Name.IsSameAs(name));
            if (null == encounterType)
                throw new ArgumentException($"Enocunter type {name} not found");
            return encounterType.Id;
        }

        /*
          B262FAAC-852F-11E7-BB31-BE2E44B06B34	HTS Confirmatory Test
            
        262FDA4-877F-11E7-BB31-BE2E44B66B34	
            B262FDA4-877F-11E7-BB31-BE2E44B67B34	
            B262FDA4-877F-11E7-BB31-BE2E44B68B34	
            B262FDA4-877F-11E7-BB31-BE2E44B69B34	
        */
       
    }
}