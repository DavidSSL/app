using app.bus;
using app.web.application.catalog_browsing;
using app.web.application.orders;
using app.web.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.specs
{
  [Subject(typeof(SubmitOrder))]
  public class SubmitOrderSpecs
  {
    public abstract class concern : Observes<ISupportAUserStory,
      SubmitOrder>
    {
    }

    public class when_run : concern
    {
      Establish c = () =>
      {
        event_bus = depends.on<IPublishEvents>();
        request = fake.an<IProvideRequestDetails>();
        submit_order_input = new SubmitOrderInput();

        request.setup(x => x.map<SubmitOrderInput>()).Return(submit_order_input);
      };

      Because b = () =>
        sut.process(request);

      It publishes_an_order_submitted_event_to_the_event_bus = () =>
        event_bus.received(x => x.publish(submit_order_input));

      static IPublishEvents event_bus;
      static SubmitOrderInput submit_order_input;
      static IProvideRequestDetails request;
    }
  }

}