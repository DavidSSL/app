using System;

namespace app.container
{
  public interface IFetchDependencies
  {
    Dependency an<Dependency>();
    object an(Type type);
  }
}