using Microsoft.EntityFrameworkCore;
using Posterr.InfraData.Context;
using System;

namespace Posterr.Test.Builders
{
    public class DataContextBuilder
    {
        public static DataContext GetDataContext()
        {

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging();

            var options = optionsBuilder.Options;

            return new DataContext(options, null);
        }
    }
}
