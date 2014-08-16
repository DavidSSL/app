 using app.web;
 using app.web.core;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace app.specs
{  
  public class RequestCommandSpecs
  {
    public abstract class concern : Observes<IProcessOneRequest,
      RequestCommand>
    {
        
    }

   
    public class when_determining_if_it_can_process_a_request  : concern
    {
      Establish c = () =>
      {
        request = fake.an<IProvideRequestDetails>();
        depends.on<IMatchARequest>(x =>
        {
          x.ShouldEqual(request);
          return true;
        });
      };

      Because b = () =>
        result = sut.can_process(request);

      It makes_its_determination_by_using_its_request_specification = () =>
        result.ShouldBeTrue();

      static bool result;
      static IProvideRequestDetails request;
    }

    public class when_processing_the_request : concern
    {
      Establish c = () =>
      {
        request = fake.an<IProvideRequestDetails>();
        feature = depends.on<ISupportAUserStory>();
      };

      Because b = () =>
        sut.process(request);

      It runs_the_application_feature = () =>
        feature.received(x => x.process(request));

      static ISupportAUserStory feature;
      static IProvideRequestDetails request;
    }
  }
}
