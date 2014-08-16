using System.Web;

namespace app.web.core
{
  public delegate IProvideRequestDetails ICreateAControllerRequest(HttpContext context);
}