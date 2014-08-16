 using System.Net;
 using System.Web;
 using app.specs.utility;
 using app.startup;
 using app.web.aspnet;
 using app.web.core;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace app.specs
{  
  [Subject(typeof(AspnetRequestHandler))]  
  public class AspnetRequestHandlerSpecs
  {
    public abstract class concern : Observes<IHttpHandler,
      AspnetRequestHandler>
    {
        
    }

   
    public class when_processing_an_http_context   : concern
    {
      Establish c = () =>
      {
        a_new_request = fake.an<IProvideRequestDetails>();
        front_controller = depends.on<IProcessWebRequests>();
        context = ObjectFactory.web.create_http_context();
        depends.on<ICreateAControllerRequest>(x =>
        {
          x.ShouldEqual(context);
          return a_new_request;
        });
        
      };

      Because b = () =>
        sut.ProcessRequest(context);

      It delegates_the_processing_of_a_new_controller_request_to_our_front_controller = () =>
        front_controller.received(x => x.process(a_new_request));

      static IProcessWebRequests front_controller;
      static HttpContext context;
      static IProvideRequestDetails a_new_request;
    }

    public class when_run : concern
    {
      Establish c = () =>
      {
        Startup.run();
        sut_factory.create_using(() => new AspnetRequestHandler());
      };

      It should_not_fail = () =>
      {
      };
         
    }

  }
}
