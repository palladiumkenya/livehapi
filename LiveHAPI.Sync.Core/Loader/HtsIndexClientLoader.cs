using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Sync.Core.Exchange;
using LiveHAPI.Sync.Core.Interface.Loaders;

namespace LiveHAPI.Sync.Core.Loader
{
    public class HtsIndexClientLoader:IHtsIndexClientLoader
    {
        private readonly IPracticeRepository _practiceRepository;
        private readonly IClientStageRepository _clientStageRepository;
        private readonly IHtsEncounterLoader _encounterLoader;
        public HtsIndexClientLoader(IPracticeRepository practiceRepository, IClientStageRepository clientStageRepository, IHtsEncounterLoader encounterLoader)
        {
            _practiceRepository = practiceRepository;
            _clientStageRepository = clientStageRepository;
            _encounterLoader = encounterLoader;
        }

        public HtsRegistry Load()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<HtsRegistry> LoadAll()
        {
            var list=new List<HtsRegistry>();
            var prac = _practiceRepository.GetDefault();
            var mfl = null != prac ? prac.Code : "00000";
            var clients = _clientStageRepository.GetAll().ToList();
            foreach (var clientStage in clients)
            {
                var encounters = _encounterLoader.Load(clientStage.ClientId);
                list.Add(HtsRegistry.Create(mfl,clientStage,encounters));
            }

            return list;
        }

     
    }
}