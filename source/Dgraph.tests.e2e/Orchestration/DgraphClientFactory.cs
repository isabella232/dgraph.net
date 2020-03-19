using System.Threading.Tasks;
using Grpc.Core;
using Serilog;

namespace Dgraph.tests.e2e.Orchestration
{
    public class DgraphClientFactory {

        private bool printed;

        public async Task<IDgraphClient> GetDgraphClient() {
            
            var client = new DgraphClient(
                new Channel("127.0.0.1:9080", ChannelCredentials.Insecure));

            if(!printed) {
                var result = await client.CheckVersion();
                if (result.IsSuccess) {
                    Log.Information("Connected to Dgraph version {Version}", result.Value);
                } else {
                    Log.Information("Failed to get Dgraph version {Error}", result);
                }
                printed = true;
            }
            
            return client;
        }

    }
}