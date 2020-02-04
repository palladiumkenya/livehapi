using System;
using System.Linq;
using Humanizer;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Sync.Core.Extractor;
using LiveHAPI.Sync.Core.Interface.Extractors;
using LiveHAPI.Sync.Core.Interface.Loaders;
using LiveHAPI.Sync.Core.Interface.Writers;
using LiveHAPI.Sync.Core.Loader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Loader
{
    [TestFixture]
    public class IndexClientMessageLoaderTests
    {
        private IClientStageExtractor _clientStageExtractor;
        private IClientPretestStageExtractor _clientPretestStageExtractor;
        private IIndexClientMessageLoader _clientMessageLoader;
        private LiveHAPIContext _context;

        [SetUp]
        public void SetUp()
        {
            _context= TestInitializer.ServiceProvider.GetService<LiveHAPIContext>();
            _clientMessageLoader = TestInitializer.ServiceProvider.GetService<IIndexClientMessageLoader>();
            _clientStageExtractor=TestInitializer.ServiceProvider.GetService<IClientStageExtractor>();
            _clientPretestStageExtractor=TestInitializer.ServiceProvider.GetService<IClientPretestStageExtractor>();

        }

        [Test]
        public void should_Load_By_Client()
        {
            var startTime = DateTime.Now;
            Guid clientid = _context.ClientStages.First().ClientId;
            var indexClientMessages = _clientMessageLoader.Load(clientid).Result.ToList();
            Assert.True(indexClientMessages.Any());
            Assert.False(indexClientMessages.Any(x => x.ClientId.IsNullOrEmpty()));
            foreach (var m in indexClientMessages)
            {
                Console.WriteLine(JsonConvert.SerializeObject(m,Formatting.Indented));
                Console.WriteLine(new string('=', 50));
            }

            Console.WriteLine($"Took {startTime.Humanize(false)}");
        }

        [Test]
        public void should_Load_All()
        {
            var startTime = DateTime.Now;
            var indexClientMessages = _clientMessageLoader.Load(null).Result.ToList();
            Assert.True(indexClientMessages.Any());
       //     Assert.False(indexClientMessages.Any(x => x.ClientId.IsNullOrEmpty()));
            foreach (var m in indexClientMessages)
            {
                Console.WriteLine(JsonConvert.SerializeObject(m,Formatting.Indented));
                Console.WriteLine(new string('=', 50));
            }
            Console.WriteLine($"Took {startTime.Humanize(false)}");
        }


        [Test]
        public void should_Extract_Load_By_Client()
        {
            var clients = _clientStageExtractor.ExtractAndStage().Result;
            var pretests = _clientPretestStageExtractor.ExtractAndStage().Result;
            Assert.True(clients == 1);
            Assert.True(pretests == 1);

            var indexClientMessages = _clientMessageLoader.Load(null).Result.ToList();
            Assert.True(indexClientMessages.Any());
            Assert.False(indexClientMessages.Any(x => x.ClientId.IsNullOrEmpty()));
            foreach (var m in indexClientMessages)
            {
                Console.WriteLine(JsonConvert.SerializeObject(m));
                Console.WriteLine(new string('=', 50));
            }
            var r = indexClientMessages.First();
            Console.WriteLine(JsonConvert.SerializeObject(r));
        }

        [TestCase(LoadAction.RegistrationOnly)]
        [TestCase(LoadAction.Pretest)]
        [TestCase(LoadAction.Pretest,LoadAction.Testing)]
        [TestCase(LoadAction.Pretest,LoadAction.Testing,LoadAction.Referral)]
        [TestCase(LoadAction.Pretest,LoadAction.Testing,LoadAction.Referral,LoadAction.Linkage)]
        [TestCase(LoadAction.Linkage)]
        [TestCase(LoadAction.Tracing)]
        public void should_Load_With_Actions_By_Client(params LoadAction[] actions)
        {
            var clients = _clientStageExtractor.ExtractAndStage().Result;
            var pretests = _clientPretestStageExtractor.ExtractAndStage().Result;
            Assert.True(clients==1);
            Assert.True(pretests == 1);

            var indexClientMessages = _clientMessageLoader.Load(null, actions).Result.ToList();
            Assert.True(indexClientMessages.Any());
            var r = indexClientMessages.First();
            //Assert.IsNull(r.CLIENTS.First().ENCOUNTER);
            Console.WriteLine(JsonConvert.SerializeObject(r));
        }
    }
}
