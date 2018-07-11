using System;
using NUnit.Framework;
using Serilog;
using Z.Dapper.Plus;

namespace LiveHAPI.Sync.Core.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
       
        [OneTimeSetUp]
        public void Setup()
        {
            try
            {
                string bulkConfigName = @"1755;701-ThePalladiumGroup";
                string bulkConfigCode = @"9005d618-3965-8877-97a5-7a27cbb21c8f";

                DapperPlusManager.AddLicense(bulkConfigName, bulkConfigCode);
                if (!Z.Dapper.Plus.DapperPlusManager.ValidateLicense(out var licenseErrorMessage))
                {
                    throw new Exception(licenseErrorMessage);
                }
            }
            catch (Exception e)
            {
                Log.Fatal($"{e}");
                throw;
            }
        }
    }
}