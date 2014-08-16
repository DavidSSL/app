using System;
using app.basic_container;
using app.container;
using app.core;
using app.web.aspnet.stubs;

namespace app.startup
{
  public class StartTheApp
  {
    class LazyContainer : IFetchDependencies
    {
      public Dependency an<Dependency>()
      {
        return Dependencies.fetch.an<Dependency>();
      }

      public object an(Type type)
      {
        return Dependencies.fetch.an(type);
      }
    }

    public static ICreateAStartupChain chain_builder = type =>
    {
      var factory_builder = new BuildDependencyFactories(new LazyContainer());
      var startup_services = new StartupServices(factory_builder);
      var factory = new StepFactory(() => startup_services);
      IRun step = factory.create(type);

      return new StartupPipelineBuilder(step, factory, StartupItems.core.combine_actions);
    };

    public static IBuildAStartupChain by<StartupStep>() where StartupStep : IRunAStartupStep
    {
      return chain_builder(typeof(StartupStep));
    }
  }
}