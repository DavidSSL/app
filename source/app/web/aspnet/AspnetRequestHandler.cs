using System.Web;
using app.container;
using app.web.core;

namespace app.web.aspnet
{
  public class AspnetRequestHandler : IHttpHandler
  {
    ICreateAControllerRequest request_factory;
    IProcessWebRequests front_controller;

    public AspnetRequestHandler() : this(
      Dependencies.fetch.an<IProcessWebRequests>(),
      Dependencies.fetch.an<ICreateAControllerRequest>())
    {
    }

    public AspnetRequestHandler(IProcessWebRequests front_controller, ICreateAControllerRequest request_factory)
    {
      this.front_controller = front_controller;
      this.request_factory = request_factory;
    }

    public void ProcessRequest(HttpContext context)
    {
      var request = request_factory(context);
      front_controller.process(request);
    }

    public bool IsReusable
    {
      get { return true; }
    }
  }
}