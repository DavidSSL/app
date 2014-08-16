using app.core;

namespace app.startup
{
  public class StartupItems
  {
    public class core
    {
      public static ICombineActions combine_actions = (first, second) => new CombinedAction(first, second);
    }
  }
}