using AutoMapper;
using LiveHAPI.Sync.Core.Profiles;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests
{
    [SetUpFixture]
    public class Config
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            try
            {
                Mapper.Initialize(cfg => { cfg.AddProfile<ClientProfile>(); });
            }
            catch
            {
            }
        }
    }
}