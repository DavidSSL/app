using System.Collections.Generic;
using app.basic_container;

namespace app.startup
{
  public class StartupServices : List<ICreateOneDependency>, IProvideStartupServices
  {
    IBuildDependencyFactories factory_builder;

    public StartupServices(IBuildDependencyFactories factory_builder)
    {
      this.factory_builder = factory_builder;
    }

    public void register<Contract, Implementation>()
    {
      Add(factory_builder.create_factory_for_implementation(typeof(Contract), typeof(Implementation)));
    }

    public void register<Contract>(Contract instance)
    {
      Add(factory_builder.create_factory_for_implementation(instance));
    }
  }
}