using app.web.application.catalog_browsing;
using app.web.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.specs
{
  [Subject(typeof(ViewA<SomeReport>))]
  public class ViewSpecs
  {
    public abstract class concern : Observes<ISupportAUserStory,
      ViewA<SomeReport>>
    {
    }

    public class when_run : concern
    {
      Establish c = () =>
      {
        display_engine = depends.on<IDisplayInformation>();
        request = fake.an<IProvideRequestDetails>();
        report = new SomeReport();

        query = depends.on<IFetchData<SomeReport>>(x =>
        {
          x.ShouldEqual(request);
          return report;
        });
      };

      Because b = () =>
        sut.process(request);

      It displays_the_report_found_using_the_query = () =>
        display_engine.received(x => x.display(report));

      static IFetchData<SomeReport> query;
      static IProvideRequestDetails request;
      static IDisplayInformation display_engine;
      static SomeReport report;
    }
  }
}