using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TweeterBook.HealthChecks
{
    /// <summary>
    /// The Idea: Make a call to redis to get a key that does not exist
    /// If there is an exeception there something is wrong but if no exception them all is healthy
    /// </summary>
    public class RedisHealthCheck : IHealthCheck
    {
        private readonly IConnectionMultiplexer _connectionMultiplerxer;

        public RedisHealthCheck(IConnectionMultiplexer connectionMultiplerxer)
        {
            _connectionMultiplerxer = connectionMultiplerxer;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var database = _connectionMultiplerxer.GetDatabase();
                database.StringGet("Healthy");
                return Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
               return Task.FromResult(HealthCheckResult.Unhealthy(ex.Message)); //ex.Message is not recommended as passing the exception as it can expose sensitive info
            }
        }
    }
}
