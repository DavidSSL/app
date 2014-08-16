 using app.core;
 using app.startup;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace app.specs
{   

  [Subject(typeof(StartupItems))]
  public class StartupItemsSpecs
  {
    public abstract class concern : Observes
    {
        
    }

    public class when_combining_two_actions : concern
    {
      Because b = () =>
        result = StartupItems.core.combine_actions(the_first, the_second);

      It returns_a_combined_action_with_the_steps_configured_correctly = () =>
      {
        var combined = result.ShouldBeAn<CombinedAction>();
        combined.first.ShouldEqual(the_first);
        combined.second.ShouldEqual(the_second);
      };

      static IRun result;
      static IRun the_first;
      static IRun the_second;
    }
  }
}
