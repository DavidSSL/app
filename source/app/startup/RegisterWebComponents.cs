using System.Collections.Generic;
using System.Web;
using app.web.aspnet;
using app.web.aspnet.stubs;
using app.web.core;

namespace app.startup
{
  public class RegisterWebComponents : IRunAStartupStep
  {
    IProvideStartupServices startup_service;

    public RegisterWebComponents(IProvideStartupServices startup_service)
    {
      this.startup_service = startup_service;
    }

    public void run()
    {
      startup_service.register<IProcessWebRequests, FrontController>();
      startup_service.register<IFindCommandsThatCanProcessRequests, CommandRegistry>();
      startup_service.register(WebStubs.missing_command_factory);
      startup_service.register<IDisplayInformation, WebFormDisplayEngine>();
      startup_service.register<IGetTheCurrentHttpContext>(() => HttpContext.Current);
      startup_service.register<ICreateViewsThatCanDisplayAReport, WebFormFactory>();
      startup_service.register(WebStubs.create_page);
      startup_service.register<IFindPathsToViews, StubPathRegistry>();
      startup_service.register<IEnumerable<IProcessOneRequest>>(new StubSetOfCommands());
      startup_service.register(WebStubs.request_factory);
    }
  }
}