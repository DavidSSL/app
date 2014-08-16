 using app.container;
 using app.startup;
 using app.web.aspnet;
 using app.web.core;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace app.specs
{  
  [Subject(typeof(Startup))]  
  public class StartupSpecs
  {
    public abstract class concern : Observes
    {
        
    }

   
    public class when_the_application_has_finished_its_startup_process   : concern
    {
      Because b = () =>
        Startup.run();

      It key_services_should_be_accessible_from_the_container = () =>
      {
        Dependencies.fetch.an<IProcessWebRequests>().ShouldBeAn<FrontController>();
        Dependencies.fetch.an<IGetTheCurrentHttpContext>().ShouldNotBeNull();
      };
    }
  }
}
