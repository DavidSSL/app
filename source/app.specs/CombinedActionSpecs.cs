using app.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.specs
{
  [Subject(typeof(CombinedAction))]
  public class CombinedActionSpecs
  {
    public abstract class concern : Observes<IRun,
      CombinedAction>
    {
    }

    public class when_run : concern
    {
      Establish c = () =>
      {
        first = fake.an<IRun>();
        second = fake.an<IRun>();

        sut_factory.create_using(() => new CombinedAction(first, second));
      };

      Because b = () =>
        sut.run();

      It runs_both_of_its_steps = () =>
      {
        first.received(x => x.run());
        second.received(x => x.run());
      };

      static IRun first;
      static IRun second;
    }
  }
}