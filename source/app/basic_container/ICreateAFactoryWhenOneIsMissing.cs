using System;

namespace app.basic_container
{
  public delegate ICreateOneDependency ICreateAFactoryWhenOneIsMissing(Type type_that_has_no_factory);
}