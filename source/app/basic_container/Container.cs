using System;
using app.container;

namespace app.basic_container
{
  public class Container : IFetchDependencies
  {
    IGetTheFactoryForADependency factories;
    ICreateAnErrorWhenCreationFails error_builder;

    public Container(IGetTheFactoryForADependency factories, ICreateAnErrorWhenCreationFails error_builder)
    {
      this.factories = factories;
      this.error_builder = error_builder;
    }

    public Dependency an<Dependency>()
    {
      return (Dependency) an(typeof(Dependency));
    }

    public object an(Type type)
    {
      var factory = factories.get_the_factory_that_can_create(type);
      try
      {
        var instance = factory.create();
        return instance;
      }
      catch (Exception e)
      {
        throw error_builder(e, type);
      }
    }
  }
}