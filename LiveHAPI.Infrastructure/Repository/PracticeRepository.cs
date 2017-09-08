using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Network;

namespace LiveHAPI.Infrastructure.Repository
{
    public class PracticeRepository : BaseRepository<Practice, Guid>, IPracticeRepository
    {
        public PracticeRepository(LiveHAPIContext context) : base(context)
        {
        }

        public Practice GetByCode(string code)
        {
            return GetAll().FirstOrDefault(x => x.Code.ToLower() == code.ToLower());
        }
    }
}