using app.web;
using app.web.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.specs
{
  [Subject(typeof(FrontController))]
  public class FrontControllerSpecs
  {
    public abstract class concern : Observes<IProcessWebRequests,
      FrontController>
    {
    }

    public class when_processing_a_request   : concern
    {
      Establish c = () =>
      {
        request = fake.an<IProvideRequestDetails>();

        command_that_can_process_request = fake.an<IProcessOneRequest>();

        command_registry = depends.on<IFindCommandsThatCanProcessRequests>();

        command_registry.setup(x => x.get_the_command_that_can_process(request))
          .Return(command_that_can_process_request);
      };

      Because b = () =>
        sut.process(request);

      It delegates_the_processing_to_the_command_that_can_process_the_request = () =>
        command_that_can_process_request.received(x => x.process(request));

      static IProcessOneRequest command_that_can_process_request;
      static IProvideRequestDetails request;
      static IFindCommandsThatCanProcessRequests command_registry;
    }
  }
}