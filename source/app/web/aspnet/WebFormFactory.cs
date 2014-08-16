using System.Web;
using app.web.aspnet.stubs;

namespace app.web.aspnet
{
  public class WebFormFactory : ICreateViewsThatCanDisplayAReport
  {
    IFindPathsToViews path_registry;
    ICreatePageInstances page_factory;

    public WebFormFactory():this(new StubPathRegistry(), WebStubs.create_page)
    {
    }

    public WebFormFactory(IFindPathsToViews path_registry, ICreatePageInstances page_factory)
    {
      this.path_registry = path_registry;
      this.page_factory = page_factory;
    }

    public IHttpHandler create_view_to_display<Report>(Report report)
    {
      var path = path_registry.get_path_to_view_that_can_display<Report>();
      var handler = (IDisplayA<Report>)page_factory(path, typeof(IDisplayA<Report>));
      handler.report = report;
      return handler;
    }
  }
}