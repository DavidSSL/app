using System;

namespace app.container
{
  public class Dependencies
  {
    public static IProvideAccessToTheContainerCreatedAtStartup provide_access_to_the_container = delegate
    {
      throw new NotImplementedException("This needs to be configured by a startup process");
    };

    public static IFetchDependencies fetch
    {
        get { return provide_access_to_the_container(); }
    }
  }
}