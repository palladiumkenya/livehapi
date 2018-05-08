using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Sync.Core.Exchange;
using LiveHAPI.Sync.Core.Interface.Loaders;

namespace LiveHAPI.Sync.Core.Loader
{
    public class HtsRegistryLoader: IHtsRegistryLoader
    {
        private readonly IPracticeRepository _practiceRepository;
        private readonly IClientStageRepository _clientStageRepository;

        public HtsRegistryLoader(IClientStageRepository clientStageRepository, IPracticeRepository practiceRepository)
        {
            _clientStageRepository = clientStageRepository;
            _practiceRepository = practiceRepository;
        }

        public HtsRegistry Load()
        {
            var prac = _practiceRepository.GetDefault();
            var mfl = null != prac ? prac.Code : "00000";
            var clients = _clientStageRepository.GetAll().ToList();
            return HtsRegistry.Create(mfl, clients);
        }
    }
}