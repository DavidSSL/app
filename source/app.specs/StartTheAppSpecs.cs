using System;
using app.startup;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.specs
{
  [Subject(typeof(StartTheApp))]
  public class StartTheAppSpecs
  {
    public abstract class concern : Observes
    {
    }

    public class when_starting_the_creation_of_a_startup_chain : concern
    {
      Establish c = () =>
      {
        the_startup_chain = fake.an<IBuildAStartupChain>();
        ICreateAStartupChain create_chain = x =>
        {
          x.ShouldEqual(typeof(SomeStep));
          return the_startup_chain;
        };

        spec.change(() => StartTheApp.chain_builder).to(create_chain);
      };

      Because b = () =>
        result = StartTheApp.by<SomeStep>();

      It returns_the_startup_pipeline_builder_that_can_carry_on_specifying_the_remaining_steps = () =>
        result.ShouldEqual(the_startup_chain);

      static IBuildAStartupChain result;
      static IBuildAStartupChain the_startup_chain;
    }
  }

  public class SomeStep : IRunAStartupStep
  {
    public void run()
    {
      throw new NotImplementedException();
    }
  }
}