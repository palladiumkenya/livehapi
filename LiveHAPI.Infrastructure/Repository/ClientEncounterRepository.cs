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
    public class ClientEncounterRepository : BaseRepository<Encounter, Guid>, IClientEncounterRepository
    {
        private readonly List<EncounterType> _encounterTypes;

        public ClientEncounterRepository(LiveHAPIContext context) : base(context)
        {
            _encounterTypes = context.EncounterTypes.AsNoTracking().ToList();
        }

        public IEnumerable<Encounter> GetReferralLinkage(Guid clientId)
        {
            var encounters = Context.Encounters
                .Where(x => x.ClientId == clientId &&
                            x.EncounterTypeId == GetEncounterTypeId("HTS Linkage"))
                .Include(x => x.ObsLinkages)
                .OrderByDescending(x=>x.EncounterDate)
                .AsNoTracking();

            return encounters;
        }

        public IEnumerable<Encounter> GetTracing(Guid clientId)
        {
            var encounters = Context.Encounters
                .Where(x => x.ClientId == clientId &&
                            (x.EncounterTypeId == GetEncounterTypeId("HTS Trace")|| x.EncounterTypeId == GetEncounterTypeId("HTS Linkage"))
                            )
                .Include(x => x.ObsTraceResults)
                .AsNoTracking();

            return encounters;
        }

        public IEnumerable<Encounter> GetTesting(Guid clientId)
        {
            var encounters = Context.Encounters
                .Where(x => x.ClientId == clientId &&
                            x.EncounterTypeId == GetEncounterTypeId("HTS Initial Test"))
                .Include(x => x.ObsTestResults)
                .AsNoTracking();

            return encounters;
        }

        public IEnumerable<Encounter> GetFinalTesting(Guid clientId)
        {
            var encounters = Context.Encounters
                .Where(x => x.ClientId == clientId &&
                            x.EncounterTypeId == GetEncounterTypeId("HTS Initial Test"))
                .Include(x => x.ObsFinalTestResults)
                .AsNoTracking();

            return encounters;
        }

        public IEnumerable<Encounter> GetPretest(Guid clientId)
        {
            var encounters = Context.Encounters
                .Where(x => x.ClientId == clientId &&
                            x.EncounterTypeId == GetEncounterTypeId("HTS Initial"))
                .Include(x => x.Obses)
                .AsNoTracking();

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
            
        262FDA4-877F-11E7-BB31-BE2E44B66B34	Family Screening
            B262FDA4-877F-11E7-BB31-BE2E44B67B34	Family Trace
            B262FDA4-877F-11E7-BB31-BE2E44B68B34	Partner Screening
            B262FDA4-877F-11E7-BB31-BE2E44B69B34	Partner Trace
        */
    }
}