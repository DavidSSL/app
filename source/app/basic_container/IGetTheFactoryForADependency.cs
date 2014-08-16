using System;

namespace app.basic_container
{
  public interface IGetTheFactoryForADependency
  {
    ICreateOneDependency get_the_factory_that_can_create(Type type);
  }
}