using Microsoft.ServiceFabric.Services.Runtime;
using System.Threading;

namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceRuntime.RegisterServiceAsync(
                    "GatewayType", 
                    context => new WebHostingService<Startup>(context, "ServiceEndpoint")
                ).GetAwaiter().GetResult();

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
