using app.web.core;

namespace app.web.application.catalog_browsing
{
  public class ViewA<Report> : ISupportAUserStory
  {
    IDisplayInformation display_engine;
    IFetchData<Report> query;

    public ViewA(IDisplayInformation display_engine, IFetchData<Report> query)
    {
      this.display_engine = display_engine;
      this.query = query;
    }

    public void process(IProvideRequestDetails request)
    {
      display_engine.display(query(request));
    }
  }
}