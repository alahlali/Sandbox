using System;
using System.Collections.Generic;
using System.Fabric;
using Autofac;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;

namespace Framework
{
    public class ActorContainerBuilder<TActor> where TActor : ActorBase
    {
        private IContainer _container;
        private readonly IList<Action<ContainerBuilder>> _containerActions = new List<Action<ContainerBuilder>>();

        public delegate void Bootstrap(ContainerBuilder containerBuilder, StatefulServiceContext context);

        private readonly IList<Bootstrap> _bootstrappers = new List<Bootstrap>();

        public ActorContainerBuilder<TActor> AddAction(Action<ContainerBuilder> item)
        {
            _containerActions.Add(item);
            return this;
        }

        public ActorContainerBuilder<TActor> AddBootstrapper(Bootstrap bootstrap)
        {
            _bootstrappers.Add(bootstrap);
            return this;
        }

        public void Register()
        {
            ActorRuntime.RegisterActorAsync<TActor>(ActorServiceFactory)
                .GetAwaiter()
                .GetResult();
        }

        private ActorService ActorServiceFactory(StatefulServiceContext context, ActorTypeInformation actorType)
        {
            BuildContainer(context);

            return new ActorService(context, actorType, ActorFactory);
        }

        private ActorBase ActorFactory(ActorService actorService, ActorId actorId)
        {
            using (var scope = _container.BeginLifetimeScope(
                    actorscope =>
                    {
                        actorscope.RegisterInstance(actorService);
                        actorscope.RegisterInstance(actorId);
                    }))
            {
                return scope.Resolve<TActor>();
            }
        }

        private void BuildContainer(StatefulServiceContext context)
        {
            var containerBuilder = new ContainerBuilder();
            EnrollBootstrappers(containerBuilder, context);
            BuildActorContainer(containerBuilder);
        }

        private void EnrollBootstrappers(ContainerBuilder containerBuilder, StatefulServiceContext context)
        {
            foreach (var bootstrap in _bootstrappers)
            {
                bootstrap(containerBuilder, context);
            }
        }

        private void BuildActorContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<TActor>();

            foreach (var builder in _containerActions)
            {
                builder(containerBuilder);
            }

            _container = containerBuilder.Build();
        }
    }
}