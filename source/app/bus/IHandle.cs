namespace app.bus
{
  public interface IHandle<in Message> 
  {
    void handle(Message message);
  }

}