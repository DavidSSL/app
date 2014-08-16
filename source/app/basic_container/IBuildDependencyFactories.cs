using System;
using app.container;
using app.web.aspnet.stubs;

namespace app.basic_container
{
  public interface IBuildDependencyFactories
  {
    ICreateOneDependency create_factory_for_implementation(Type contract, Type implementation);
    ICreateOneDependency create_factory_for_implementation<Contract>(Contract implementation);
  }

  public class BuildDependencyFactories : IBuildDependencyFactories
  {
    IFetchDependencies container;

    public BuildDependencyFactories(IFetchDependencies container)
    {
      this.container = container;
    }

    public ICreateOneDependency create_factory_for_implementation(Type contract, Type implementation)
    {
      return create_factory(contract, new AutomaticallyWiringFactory(
        WebStubs.greediest_picker,
        implementation,
        container
        ));
    }

    public ICreateOneDependency create_factory_for_implementation<Contract>(Contract implementation)
    {
      return create_factory(typeof(Contract), new SimpleFactory(() => implementation));
    }

    public ICreateOneDependency create_factory(Type type, ICreateADependency real_factory)
    {
      return new CreateOneDependency(real_factory,WebStubs.is_a(type));
    }
  }
}