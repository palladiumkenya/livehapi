using System;
using System.Collections.Generic;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Infrastructure.Repository
{
  public  class EncounterRepository : BaseRepository<Encounter, Guid> ,IEncounterRepository
    {
        public EncounterRepository(LiveHAPIContext context) : base(context)
        {
        }
    }
}
