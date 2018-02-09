using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.IQCare.Core.Interfaces.Repository;
using LiveHAPI.IQCare.Core.Model;
using LiveHAPI.IQCare.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.IQCare.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class ConfigRepositoryTests
    {
        private EMRContext  _context;
        private IConfigRepository _configRepository;
        private User _testUser;
        private Group _testGroup;
        private Group _testGroupNew;
        private string _testGroupName;
        private List<int> _featureIds;
        private List<int> _userIds;
        private DbContextOptions<EMRContext> _options;

        [OneTimeSetUp]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["connectionStrings:EMRConnection"];

            _options = new DbContextOptionsBuilder<EMRContext>()
                .UseSqlServer(connectionString)
                .Options;

           var context = new EMRContext(_options);
            context.ApplyMigrations();
            context.UpdateTranslations();

        }

        [SetUp]
        public void SetUp()
        {
            
            _context = new EMRContext(_options);
            _configRepository=new ConfigRepository(_context);

            _testUser = new User("MauntestMaun");
            _testGroup = new Group("MaungroupMaun");
            _testGroupNew = new Group("MaungroupMaunNEW");
            _featureIds = _context.Features.Where(x => x.DeleteFlag == 0).Take(2).Select(x => x.FeatureID).ToList();
            _userIds = _context.Users.Where(x => x.DeleteFlag == 0).Select(x => x.UserId).ToList();
            _context.Add(_testUser);
            _context.Add(_testGroup);
            _context.SaveChanges();
        }


        [Test]
        public void should_Get_Users()
        {
            var users = _configRepository.GetUsers().ToList();
            Assert.IsTrue(users.Count > 0);
            foreach (var user in users)
            {
                Console.WriteLine($"{user}");
            }
        }

        [Test]
        public void should_Get_Groups()
        {
            var groups = _configRepository.GetGroups().ToList();
            Assert.IsTrue(groups.Count > 0);
            foreach (var g in groups)
            {
                Console.WriteLine($"{g}");
            }
        }

        [Test]
        public void should_Get_User_Groups()
        {
            var userGroups = _configRepository.GetUserGroups().ToList();
            Assert.IsTrue(userGroups.Count > 0);
            foreach (var userGroup in userGroups)
            {
                Console.WriteLine($"{userGroup}");
            }
        }
        [Test]
        public void should_Get_Group_Features()
        {
            var groupFeatures = _configRepository.GetGroupFeatures().ToList();
            Assert.IsTrue(groupFeatures.Count > 0);
            foreach (var groupFeature in groupFeatures)
            {
                Console.WriteLine($"{groupFeature}");
            }
        }


        [Test]
        public void should_Get_Facilities()
        {
            var locations = _configRepository.GetLocations().ToList();
            Assert.IsTrue(locations.Count > 0);
            foreach (var location in locations)
            {
                Console.WriteLine($">.{location}");
            }
        }
        [Test]
        public void should_Get_Modules()
        {
            var modules = _configRepository.GetModules().ToList();
            Assert.IsTrue(modules.Count > 0);
            foreach (var module in modules)
            {
                Console.WriteLine($">.{module}");
            }
        }
        [Test]
        public void should_Get_Features()
        {
            var features = _configRepository.GetFeatures().ToList();
            Assert.IsTrue(features.Count > 0);
            foreach (var feature in features)
            {
                Console.WriteLine($">.{feature}");
            }
        }

        [Test]
        public void should_Get_VisitTypes()
        {
            var visitTypes = _configRepository.GetVisitTypes().ToList();
            Assert.IsTrue(visitTypes.Count > 0);
            foreach (var visitType in visitTypes)
            {
                Console.WriteLine($">.{visitType}");
            }
        }


        [Test]
        public void should_Create_Or_Update_Group_New()
        {
            _configRepository.CreateOrUpdateGroup(_testGroupNew);
            var newGroup = _configRepository.GetGroup(_testGroupNew.GroupName);
            Assert.NotNull(newGroup);
            Console.WriteLine(newGroup);
        }
        [Test]
        public void should_Create_Or_Update_Group_Update()
        {
            _testGroupName = "XXX Group";
            _testGroup.GroupName = _testGroupName;
            _configRepository.CreateOrUpdateGroup(_testGroup);
            var updateGroup = _configRepository.GetGroup(_testGroupName);
            Assert.NotNull(updateGroup);
            Assert.AreEqual("XXX Group", updateGroup.GroupName);
            Console.WriteLine(updateGroup);
        }

        [Test]
        public void should_Create_Or_Update_Group_Features_New()
        {
            _configRepository.CreateGroupFeature(_testGroup.GroupName, _featureIds);
            var groupFeatures = _configRepository.GetGroupFeatures().Where(x => x.GroupID == _testGroup.GroupID)
                .ToList();
            Assert.True(groupFeatures.Count == 10);
        }

        [Test]
        public void should_Assign_Users_To_Group()
        {
            _configRepository.AssignUsersToGroup(_userIds,_testGroup.GroupName);
            var groupFeatures = _configRepository.GetUserGroups().Where(x => _userIds.Contains(x.UserID)).ToList();
            Assert.True(groupFeatures.Count >0);
        }

        [TearDown]
        public void TearDown()
        {

            var connection = _context.Database.GetDbConnection() as SqlConnection;
            connection.Execute($@"
            delete from  [lnk_usergroup] where GroupID in (select GroupID from  [mst_groups] where GroupName in ('{_testGroup.GroupName}','{_testGroupNew.GroupName}','{_testGroupName}'));	
            delete from  [lnk_GroupFeatures] where GroupID in (select GroupID from  [mst_groups] where GroupName in ('{_testGroup.GroupName}','{_testGroupNew.GroupName}','{_testGroupName}'));
            delete from  [mst_groups] where GroupName in ('{_testGroup.GroupName}','{_testGroupNew.GroupName}','{_testGroupName}');            
            delete from  [mst_user] where UserName in ('{_testUser.UserName}');
            ");
            
        }
    }
}