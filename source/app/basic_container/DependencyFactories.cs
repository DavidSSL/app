using System;
using System.Collections.Generic;
using System.Linq;

namespace app.basic_container
{
  public class DependencyFactories : IGetTheFactoryForADependency
  {
    IEnumerable<ICreateOneDependency> factories;
    ICreateAFactoryWhenOneIsMissing missing_factory_builder;

    public DependencyFactories(IEnumerable<ICreateOneDependency> factories, ICreateAFactoryWhenOneIsMissing missing_factory_builder)
    {
      this.factories = factories;
      this.missing_factory_builder = missing_factory_builder;
    }

    public ICreateOneDependency get_the_factory_that_can_create(Type type)
    {
      var result = factories.FirstOrDefault(x => x.can_create(type));
      return result ?? missing_factory_builder(type);
    }
  }
}