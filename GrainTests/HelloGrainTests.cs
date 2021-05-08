using GrainInterfaces;
using Orleans.TestingHost;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GrainTests
{
    public class HelloGrainTests
    {
        private readonly TestCluster _cluster;
        public HelloGrainTests(ClusterFixture fixture)
        {
            _cluster = fixture.Cluster;
        }

        [Fact]
        public async Task SaysHelloCorrectly()
        {
            var hello = _cluster.GrainFactory.GetGrain<IHello>(123);
            var greeting = await hello.SayHello("hi");

            Assert.Equal("\n Client said 'hi', so HelloGrain says: Hello!", greeting);
        }
    }
}
