using app.core;

namespace app.startup
{
  public class StartupPipelineBuilder : IBuildAStartupChain
  {
    public IRun step;
    ICreateAStartupStep step_factory;
    ICombineActions combine_actions;

    public StartupPipelineBuilder(IRun step, ICreateAStartupStep step_factory, ICombineActions combine_actions)
    {
      this.step = step;
      this.step_factory = step_factory;
      this.combine_actions = combine_actions;
    }

    IRun combine_with<Step>() where Step : IRunAStartupStep
    {
      return combine_actions(step, step_factory.create(typeof(Step)));
    }

    public IBuildAStartupChain followed_by<Step>() where Step : IRunAStartupStep
    {
      return new StartupPipelineBuilder(combine_with<Step>(), step_factory, combine_actions);
    }

    public void finish_by<Step>() where Step : IRunAStartupStep
    {
      combine_with<Step>().run();
    }
  }
}