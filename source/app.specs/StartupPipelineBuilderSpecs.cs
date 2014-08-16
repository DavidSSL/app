using app.core;
using app.startup;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.specs
{
  [Subject(typeof(StartupPipelineBuilder))]
  public class StartupPipelineBuilderSpecs
  {
    public abstract class concern : Observes<IBuildAStartupChain,
      StartupPipelineBuilder>
    {
    }

    public class when_specifying_a_follow_up_step : concern
    {
      Establish c = () =>
      {
        first = depends.on<IRun>();   
        second = fake.an<IRunAStartupStep>();
        combined = fake.an<IRun>();

        step_factory = depends.on<ICreateAStartupStep>();

        depends.on<ICombineActions>((x, y) =>
        {
          x.ShouldEqual(first);
          y.ShouldEqual(second);

          return combined;
        });

        step_factory.setup(x => x.create(typeof(SecondStep))).Return(second);
      };

      Because b = () =>
        result = sut.followed_by<SecondStep>();

      It returns_a_new_builder_instance_with_the_combined_step = () =>
      {
        var builder = result.ShouldBeAn<StartupPipelineBuilder>();
        builder.step.ShouldEqual(combined);
        builder.ShouldNotEqual(sut);
      };

      static IBuildAStartupChain result;
      static IRun first;
      static IRunAStartupStep second;
      static IRun combined;
      static ICreateAStartupStep step_factory;
    }

    public class when_the_finish_step_is_specified : concern
    {
      Establish c = () =>
      {
        first = depends.on<IRun>();   
        second = fake.an<IRunAStartupStep>();
        combined = fake.an<IRun>();

        step_factory = depends.on<ICreateAStartupStep>();

        depends.on<ICombineActions>((x, y) =>
        {
          x.ShouldEqual(first);
          y.ShouldEqual(second);

          return combined;
        });

        step_factory.setup(x => x.create(typeof(SecondStep))).Return(second);
      };

      Because b = () =>
        sut.finish_by<SecondStep>();

      It runs_all_of_the_steps_in_the_chain = () =>
        combined.received(x => x.run());

      static IRun first;
      static IRunAStartupStep second;
      static IRun combined;
      static ICreateAStartupStep step_factory;
    }

    public class SecondStep : IRunAStartupStep
    {
      public void run()
      {
      }
    }
  }
}