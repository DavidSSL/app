using app.basic_container;
using app.container;
using app.web.aspnet.stubs;

namespace app.startup
{
  public class InitializingTheContainer : IRunAStartupStep
  {
    IProvideStartupServices startup_services;

    public InitializingTheContainer(IProvideStartupServices startup_services)
    {
      this.startup_services = startup_services;
    }

    public void run()
    {
      var dependency_factories = new DependencyFactories(startup_services, WebStubs.missing_dependency_factory);
      var container = new Container(dependency_factories, WebStubs.dependency_creation_error);
      Dependencies.provide_access_to_the_container = () => container;
    }
  }
}