using Microsoft.AspNetCore.Hosting;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Gateway
{
    internal sealed class WebHostingService<TStartup> : StatelessService, ICommunicationListener where TStartup : class
    {
        private readonly string _endpointName;

        private IWebHost _webHost;

        public WebHostingService(StatelessServiceContext serviceContext, string endpointName)
            : base(serviceContext)
        {
            _endpointName = endpointName;
        }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new[] { new ServiceInstanceListener(_ => this) };
        }

        void ICommunicationListener.Abort()
        {
            _webHost?.Dispose();
        }

        Task ICommunicationListener.CloseAsync(CancellationToken cancellationToken)
        {
            _webHost?.Dispose();

            return Task.FromResult(true);
        }

        Task<string> ICommunicationListener.OpenAsync(CancellationToken cancellationToken)
        {
            var endpoint = FabricRuntime.GetActivationContext().GetEndpoint(_endpointName);

            string serverUrl = $"{endpoint.Protocol}://{FabricRuntime.GetNodeContext().IPAddressOrFQDN}:{endpoint.Port}";

            _webHost = new WebHostBuilder().UseKestrel()
                                           .UseContentRoot(Directory.GetCurrentDirectory())
                                           .UseStartup<TStartup>()
                                           .UseUrls(serverUrl)
                                           .Build();

            _webHost.Start();

            return Task.FromResult(serverUrl);
        }
    }
}