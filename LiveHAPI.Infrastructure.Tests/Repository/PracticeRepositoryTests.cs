﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dapper.Contrib.Extensions;
using FizzWare.NBuilder;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class PracticeRepositoryTests
    {
        private LiveHAPIContext _context;
        private IPracticeRepository _practiceRepository;

        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["connectionStrings:hAPIConnection"];

            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new LiveHAPIContext(options);
            //TestData.Init();
            //TestDataCreator.Init(_context);

            _practiceRepository = new PracticeRepository(_context);
        }

        [Test]
        public void should_Get_By_Code()
        {
            var practice = _practiceRepository.GetByCode("14080");
            Assert.IsNotNull(practice);
            Console.WriteLine(practice);
        }

        [Test]
        public void should_Sync_New_Facility()
        {
            var practice = Builder<Practice>.CreateNew()
                .With(x => x.Code = DateTime.Now.Ticks.ToString())
                .With(x => x.CountyId = 47)
                .With(x=>x.PracticeTypeId=string.Empty)
                .Build();

            _practiceRepository.Sync(practice);
            _practiceRepository.Save();

            var facility = _practiceRepository.Get(practice.Id);
            Assert.IsNotNull(facility);
            Assert.AreEqual("Facility",facility.PracticeTypeId);
            Console.WriteLine(facility);
        }

        [Test]
        public void should_Sync_Multiple_New_Facility()
        {
            var practices = Builder<Practice>.CreateListOfSize(3).All()
                .With(x => x.Code = DateTime.Now.Ticks.ToString())
                .With(x => x.CountyId = 47)
                .With(x => x.PracticeTypeId = string.Empty)
                .With(x => x.IsDefault == false)
                .Build();
            practices[2].IsDefault = true;
            Console.WriteLine($"{practices[2]} <{practices[2].IsDefault}>" );
            _practiceRepository.Sync(practices);
         
            _practiceRepository=new PracticeRepository(_context);
            foreach (var practice in practices)
            {
                var facility = _practiceRepository.Get(practice.Id);
                Assert.IsNotNull(facility);
                Assert.AreEqual("Facility", facility.PracticeTypeId);
                Console.WriteLine($"{facility} [{facility.IsDefault}]");
            }
            var facilities =_practiceRepository.GetAll().ToList();
            Assert.True(facilities.Where(x=>x.IsDefault).ToList().Count==1);
        }
        [Test]
        public void should_Sync_Updated_Facility()
        {
            var practice = TestData.TestPractices().First();
            practice.Code = "14080";
            practice.Name = "Maun";


            _practiceRepository.Sync(practice);
            _practiceRepository.Save();

            var facility = _practiceRepository.GetByCode("14080");
            Assert.IsNotNull(facility);
            Assert.AreEqual("Maun", facility.Name);
            Console.WriteLine(facility);
        }
        [Test]
        public void should_Sync_Multiple_Updated_Facility()
        {
            var practice = TestData.TestPractices().First();
            practice.Code = "14080";
            practice.Name = "Maun";


            _practiceRepository.Sync(new List<Practice>{ practice });
          
            _practiceRepository=new PracticeRepository(_context);
            var facility = _practiceRepository.GetByCode("14080");
            Assert.IsNotNull(facility);
            Assert.AreEqual("Maun", facility.Name);
            Console.WriteLine(facility);
        }
        [Test]
        public void should_Sync_New_Default_Facility()
        {
            var practices = Builder<Practice>.CreateListOfSize(2).All()
                .With(x => x.Code = DateTime.Now.Ticks.ToString())
                .With(x => x.CountyId = 47)
                .With(x => x.PracticeTypeId = string.Empty)
                .With(x=>x.IsDefault=false)
                .Build().ToList();

            practices[1].IsDefault = true;

            foreach (var practice in practices)
            {
                _practiceRepository.Sync(practice);
                _practiceRepository.Save();
            }


            var facs = _practiceRepository.GetAll().ToList();
            Assert.That(facs.Where(x => x.IsDefault).ToList().Count==1);

            var facility = facs.FirstOrDefault(x => x.IsDefault);
            
            Assert.IsNotNull(facility);
            Assert.AreEqual("Facility", facility.PracticeTypeId);
            Assert.AreEqual(practices[1].Code, facility.Code);
            Console.WriteLine(facility);
        }

        [Test]
        public void should_Sync_Multiple_New_Default_Facility()
        {
            var practices = Builder<Practice>.CreateListOfSize(2).All()
                .With(x => x.Code = DateTime.Now.Ticks.ToString())
                .With(x => x.CountyId = 47)
                .With(x => x.PracticeTypeId = string.Empty)
                .With(x => x.IsDefault = false)
                .Build().ToList();

            practices[1].IsDefault = true;
            _practiceRepository.Sync(practices);

            _practiceRepository=new PracticeRepository(_context);
            var facs = _practiceRepository.GetAll().ToList();
            Assert.That(facs.Where(x => x.IsDefault).ToList().Count == 1);

            var facility = facs.FirstOrDefault(x => x.IsDefault);

            Assert.IsNotNull(facility);
            Assert.AreEqual("Facility", facility.PracticeTypeId);
            Assert.AreEqual(practices[1].Code, facility.Code);
            Console.WriteLine(facility);
        }

        [Test]
        public void should_Sync_Updated_Default_Facility()
        {
            var practice = TestData.TestPractices().First();
            practice.Code = "14080";
            practice.Name = "Maun";
            practice.IsDefault = true;

            _practiceRepository.Sync(practice);
            _practiceRepository.Save();

            var facs = _practiceRepository.GetAll().ToList();
            Assert.That(facs.Where(x => x.IsDefault).ToList().Count == 1);
            var facility = facs.FirstOrDefault(x => x.IsDefault);

            Assert.IsNotNull(facility);
            Assert.AreEqual("Maun", facility.Name);
            Console.WriteLine(facility);
        }

        [Test]
        public void should_Sync_Multiple_Updated_Default_Facility()
        {
            var practice = TestData.TestPractices().First();
            practice.Code = "14080";
            practice.Name = "Maun";
            practice.IsDefault = true;

            _practiceRepository.Sync(new List<Practice>{practice});
            
            _practiceRepository=new PracticeRepository(_context);
            var facs = _practiceRepository.GetAll().ToList();
            Assert.That(facs.Where(x => x.IsDefault).ToList().Count == 1);
            var facility = facs.FirstOrDefault(x => x.IsDefault);

            Assert.IsNotNull(facility);
            Assert.AreEqual("Maun", facility.Name);
            Console.WriteLine(facility);
        }

    }
}