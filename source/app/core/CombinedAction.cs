namespace app.core
{
  public class CombinedAction : IRun
  {
    public IRun first;
    public IRun second;

    public CombinedAction(IRun first, IRun second)
    {
      this.first = first;
      this.second = second;
    }

    public void run()
    {
      first.run();
      second.run();
    }
  }
}