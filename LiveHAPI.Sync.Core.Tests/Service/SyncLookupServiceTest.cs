using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Tests.TestHelpers;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Services;
using LiveHAPI.Sync.Core.Reader;
using LiveHAPI.Sync.Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Service
{
    [TestFixture]
    public class SyncLookupServiceTest
    {
        private readonly string _baseUrl = "http://localhost:3333";
        private LiveHAPIContext _context;
        private ISubscriberTranslationRepository _repository;
        private ISyncLookupService _service;
        private IClientLookupReader _reader;
        private ISubscriberSystemRepository _subscriberSystemRepository;
        private SubscriberSystem _emr;

        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            //var connectionString = config["connectionStrings:hAPIConnection"].Replace("#dir#", TestContext.CurrentContext.TestDirectory.HasToEndWith(@"\"));
            var connectionString = config["connectionStrings:livehAPIConnection"];

            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString)
                .Options;
            _reader = new ClientLookupReader(new RestClient(_baseUrl));
            _context = new LiveHAPIContext(options);
          
            _repository = new SubscriberTranslationRepository(_context);
            _subscriberSystemRepository=new SubscriberSystemRepository(_context);
            _service=new SyncLookupService(_reader,_repository);
            _emr = _subscriberSystemRepository.GetDefault();
        }

        [Test]
        public void should_Sync_Lookups()
        {
            var count=_service.Sync().Result;
            Assert.True(count>0);
            Console.WriteLine($"synced {count}");
        }

        [Test]
        public void should_Have_Mapped_Pretest_Lookups()
        {
            var exluded = new List<Guid>
            {
                new Guid("B260665C-852F-11E7-BB31-BE2E44B06B34"),
                new Guid("B26039A2-852F-11E7-BB31-BE2E44B06B34")
            };
            
            var lookups = _reader.Read().Result.ToList();
            Assert.True(lookups.Any());
            var questionIds = _context.Questions.AsNoTracking().Where(x=>!exluded.Contains(x.Id)).Select(x => x.Id.ToString().ToLower()).ToList();

            var count = _service.Sync().Result;
            Assert.True(count > 0);
            
                
              var translations = _repository.GetAll(
                x => x.SubscriberSystemId == _emr.Id &&
                     questionIds.Contains(x.Ref.ToLower())
            ).ToList();

            Assert.True(translations.Any());

            foreach (var translation in translations)
            {
                var exisits = lookups.Any(x => x.ItemId ==Convert.ToInt32(translation.SubCode)&&
                                               x.MasterName.IsSameAs(translation.SubRef) &&
                                               x.ItemName.IsSameAs(translation.SubDisplay)
                                               );
                Assert.True(exisits);
            }
            Console.WriteLine("im done");
        }

        [Test]
        public void should_Have_Mapped_Lookups()
        {
            var mismatches=new List<SubscriberTranslation>();
            
            var lookups = _reader.Read().Result.ToList();
            var count = _service.Sync().Result;
            Assert.True(count > 0);


            var translations = _repository.GetAll(x => x.SubscriberSystemId == _emr.Id).ToList();

            Assert.True(translations.Any());

            foreach (var translation in translations)
            {
                var exisits = lookups.Any(x => x.ItemId == Convert.ToInt32(translation.SubCode) &&
                                               x.MasterName.IsSameAs(translation.SubRef) &&
                                               x.ItemName.IsSameAs(translation.SubDisplay)
                );
                if(!exisits)
                    mismatches.Add(translation);

            }

            foreach (var subscriberTranslation in mismatches)
            {
                Console.WriteLine(subscriberTranslation);
            }
            Assert.False(mismatches.Any());
            
            Console.WriteLine("im done");

        }
    }
}