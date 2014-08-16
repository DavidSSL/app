 using System.Web;
 using app.web.aspnet;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace app.specs
{  
  [Subject(typeof(WebFormFactory))]  
  public class WebFormFactorySpecs
  {
    public abstract class concern : Observes<ICreateViewsThatCanDisplayAReport,
      WebFormFactory>
    {
        
    }

   
    public class when_creating_the_view_that_can_display_a_report : concern
    {
      Establish c = () =>
      {
        the_view = fake.an<IDisplayA<SomeReport>>();
        the_path = "blah.aspx";
        report = new SomeReport();
        view_path_registry = depends.on<IFindPathsToViews>();

        view_path_registry.setup(x => x.get_path_to_view_that_can_display<SomeReport>()).Return(the_path);

        depends.on<ICreatePageInstances>((path, type) =>
        {
          path.ShouldEqual(the_path);
          type.ShouldEqual(typeof(IDisplayA<SomeReport>));

          return the_view;
        });
      };

      Because b = () =>
        result = sut.create_view_to_display(report);

      It provides_the_view_with_its_report = () =>
        the_view.report.ShouldEqual(report);
        
      It creates_the_page_instance_for_view = () =>
        result.ShouldEqual(the_view);

      static IFindPathsToViews view_path_registry;
      static SomeReport report;
      static IHttpHandler result;
      static IDisplayA<SomeReport> the_view;
      static string the_path;
    }
  }
}
