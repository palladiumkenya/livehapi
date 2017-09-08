using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Network;

namespace LiveHAPI.Infrastructure.Repository
{
    public class PracticeActivationRepository : BaseRepository<PracticeActivation, Guid>, IPracticeActivationRepository
    {
        public PracticeActivationRepository(LiveHAPIContext context) : base(context)
        {
        }

      
    }
}