using app.container;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.specs
{
  [Subject(typeof(Dependencies))]
  public class DependenciesSpecs
  {
    public abstract class concern : Observes
    {
    }

    public class when_providing_access_to_the_container_facade : concern
    {
      Establish c = () =>
      {
        the_container_facade = fake.an<IFetchDependencies>();
        IProvideAccessToTheContainerCreatedAtStartup startup = () => the_container_facade;

        spec.change(() => Dependencies.provide_access_to_the_container).to(startup);
      };

      Because b = () =>
        result = Dependencies.fetch;

      It returns_the_container_facade_that_was_configured_during_startup = () =>
        result.ShouldEqual(the_container_facade);

      static IFetchDependencies result;
      static IFetchDependencies the_container_facade;
    }
  }
}