using Orleans.TestingHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GrainTests
{
    public class ClusterFixture : IDisposable
    {
        public TestCluster Cluster { get; private set; }
        public ClusterFixture()
        {
            var builder = new TestClusterBuilder();
            builder.Options.ServiceId = Guid.NewGuid().ToString();
            Cluster = builder.Build();
            Cluster.Deploy();
        }

        public void Dispose()
        {
            Cluster.StopAllSilos();
        }
    }

    [CollectionDefinition(ClusterCollection.Name)]
    public class ClusterCollection : ICollectionFixture<ClusterFixture>
    {
        public const string Name = "ClusterCollection";
    }
}
