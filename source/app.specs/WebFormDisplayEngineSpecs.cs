using System.Web;
using System.Web.UI.WebControls;
using app.specs.utility;
using app.web.aspnet;
using app.web.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.specs
{
  [Subject(typeof(IDisplayInformation))]
  public class WebFormDisplayEngineSpecs
  {
    public abstract class concern : Observes<IDisplayInformation,
      WebFormDisplayEngine>
    {
    }

    public class when_displaying_a_report_model : concern
    {
      Establish c = () =>
      {
        some_report = new SomeReport();
        view = fake.an<IHttpHandler>();
        current_context = ObjectFactory.web.create_http_context();
        view_factory = depends.on<ICreateViewsThatCanDisplayAReport>();
        depends.on<IGetTheCurrentHttpContext>(() => current_context);

        view_factory.setup(x => x.create_view_to_display(some_report)).Return(view);
      };

      Because b = () =>
        sut.display(some_report);

      It tell_the_view_to_render_in_the_current_context = () =>
        view.received(x => x.ProcessRequest(current_context));


      static ICreateViewsThatCanDisplayAReport view_factory;
      static SomeReport some_report;
      static IHttpHandler view;
      static HttpContext current_context;
    }
  }

  public class SomeReport
  {
  }
}