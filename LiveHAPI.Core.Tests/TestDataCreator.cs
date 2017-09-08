using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Infrastructure;
using LiveHAPI.Shared.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.Core.Tests
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


            Clear(context);
            Create(context, counties, subCounties, practiceTypes, practices, practiceActivations);
        }

        public static void Create(LiveHAPIContext context, params IEnumerable<object>[] entities)
        {
            foreach (IEnumerable<object> t in entities)
            {
                context.AddRange(t);
            }
            context.SaveChanges();
        }
        public static void Clear(LiveHAPIContext context, params DbSet<object>[] entities)
        {
            context.RemoveRange(context.PracticeActivations);
            context.RemoveRange(context.Practices);
            context.RemoveRange(context.PracticeTypes);
            context.RemoveRange(context.SubCounties);
            context.RemoveRange(context.Counties);

            context.SaveChanges();
        }
    }
}
