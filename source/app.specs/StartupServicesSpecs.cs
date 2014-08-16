using System;
using app.basic_container;
using app.startup;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.specs
{
  [Subject(typeof(StartupServices))]
  public class StartupServicesSpecs
  {
    public abstract class concern : Observes<IProvideStartupServices,
      StartupServices>
    {
    }

    public class when_registering : concern
    {
      public class a_contract_and_its_implementation_type
      {
        Establish c = () =>
        {
          factory_builder = depends.on<IBuildDependencyFactories>();
          the_factory = fake.an<ICreateOneDependency>();

          factory_builder.setup(x => x.create_factory_for_implementation(typeof(IDoSomething), typeof(DoSomething))).Return(the_factory);
        };

        Because b = () =>
          sut.register<IDoSomething, DoSomething>();

        It registers_the_factory_that_can_create_the_dependency = () =>
          concrete_sut.ShouldContain(the_factory);

        static ICreateOneDependency the_factory;
        static IBuildDependencyFactories factory_builder;
      }

      public class a_contract_and_its_instance
      {
        Establish c = () =>
        {
          instance = new DoSomething();
          factory_builder = depends.on<IBuildDependencyFactories>();
          the_factory = fake.an<ICreateOneDependency>();

          factory_builder.setup(x => x.create_factory_for_implementation<IDoSomething>(instance)).Return(the_factory);
        };

        Because b = () =>
          sut.register<IDoSomething>(instance);

        It registers_the_factory_that_can_create_the_dependency = () =>
          concrete_sut.ShouldContain(the_factory);

        static ICreateOneDependency the_factory;
        static IBuildDependencyFactories factory_builder;
        static DoSomething instance;
      }
    }
  }

  public interface IDoSomething
  {
  }

  public class DoSomething :IDoSomething
  {
  }
}