namespace app.startup
{
  public interface IBuildAStartupChain
  {
    IBuildAStartupChain followed_by<Step>() where Step : IRunAStartupStep;
    void finish_by<Step>() where Step : IRunAStartupStep;
  }
}