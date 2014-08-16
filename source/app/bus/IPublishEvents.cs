namespace app.bus
{
  public interface IPublishEvents
  {
    void publish<Message>(Message message);
  }
}