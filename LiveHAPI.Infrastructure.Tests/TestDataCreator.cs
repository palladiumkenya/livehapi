using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Shared.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.Infrastructure.Tests
{
    public class TestDataCreator
    {
        public static void Init(LiveHAPIContext context)
        {
            var counties = TestData.TestCounties();
            var subCounties = TestData.TestSubCounties();
            var practiceTypes = TestData.TestPracticeTypes();
            var practices = TestData.TestPracticeWithActivation();
            var practiceActivations = practices.SelectMany(x => x.Activations).ToList();

            Create<County>(context, counties);
            Create<SubCounty>(context, subCounties);
            Create<PracticeType>(context, practiceTypes);
            Create<Practice>(context, practices);
            Create<PracticeActivation>(context, practiceActivations);
        }

        public static void Create<T>(LiveHAPIContext context, IEnumerable<object> entities) where T:class 
        {
            context.RemoveRange(context.Set<T>());
            context.SaveChanges();
            context.AddRange(entities);
            context.SaveChanges();
        }
    }
}
