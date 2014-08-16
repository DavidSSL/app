namespace app.bus
{
  public class EventBus : IPublishEvents
  {
    IFindMessageSubscribers subscriber_registry;

    public EventBus(IFindMessageSubscribers subscriber_registry)
    {
      this.subscriber_registry = subscriber_registry;
    }

    public void publish<Message>(Message message)
    {
      foreach (var handler in subscriber_registry.get_all_handlers_for<Message>())
        handler.handle(message);
    }
  }
}