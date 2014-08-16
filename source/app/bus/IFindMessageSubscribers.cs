using System.Collections.Generic;

namespace app.bus
{
  public interface IFindMessageSubscribers
  {
    IEnumerable<IHandle<Message>> get_all_handlers_for<Message>();
  }
}