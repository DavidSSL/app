using System.Collections.Generic;
using app.bus;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.specs
{
  [Subject(typeof(EventBus))]
  public class EventBusSpecs
  {
    public abstract class concern : Observes<IPublishEvents, EventBus>
    {
    }

    public class when_an_event_is_published : concern
    {
      Establish context = () =>
      {
        the_message = new SomeMessage();
        handler = fake.an<IHandle<SomeMessage>>();

        subscriber_registry = depends.on<IFindMessageSubscribers>();
        IList<IHandle<SomeMessage> > handlers = new List<IHandle<SomeMessage>>{handler};

        subscriber_registry.setup(x => x.get_all_handlers_for<SomeMessage>())
          .Return(handlers);
          
      };

      Because of = () => sut.publish(the_message);

      It should_notify_its_subscribers = () =>
        handler.received(x => x.handle(the_message));

      static SomeMessage the_message;
      static IFindMessageSubscribers subscriber_registry;
      static IHandle<SomeMessage> handler;
    }

    public class SomeMessage
    {
    }

    public class Other
    {
    }
  }
}