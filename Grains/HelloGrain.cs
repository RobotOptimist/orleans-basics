using GrainInterfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class HelloGrain : Orleans.Grain, IHello
    {
        private readonly ILogger _logger;
        public HelloGrain(ILogger<HelloGrain> logger)
        {
            _logger = logger;
        }
        Task<string> IHello.SayHello(string greeting)
        {
            _logger.LogInformation($"\n SayHello message received: greeting = '{greeting}'");
            return Task.FromResult($"\n Client said '{greeting}', so HelloGrain says: Hello!");
        }
    }
}
