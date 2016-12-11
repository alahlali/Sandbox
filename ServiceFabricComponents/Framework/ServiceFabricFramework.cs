using System;
using System.Collections.Generic;
using System.Fabric;
using Autofac;
using Microsoft.ServiceFabric.Actors.Runtime;

namespace Framework
{
    public static class ServiceFabricFramework
    {
        public static ActorContainerBuilder<TActor> CreateActorContainerBuilder<TActor>() where TActor : ActorBase
        {
            return new ActorContainerBuilder<TActor>();
        }
    }
}