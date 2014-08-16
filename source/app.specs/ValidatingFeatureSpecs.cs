using app.validation;
using app.web.application.catalog_browsing;
using app.web.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.specs
{
  [Subject(typeof(ValidatingFeature<>))]
  public class ValidatingFeatureSpecs
  {
    public abstract class concern : Observes<ISupportAUserStory,
      ValidatingFeature<SomeInputModel>>
    {
    }

    public class when_run : concern
    {
      public class and_the_input_is_valid : concern
      {
        Establish c = () =>
        {
          request = fake.an<IProvideRequestDetails>();
          original = depends.on<ISupportAUserStory>();
          validator = depends.on<IValidateAn<SomeInputModel>>();
          input = new SomeInputModel();

          request.setup(x => x.map<SomeInputModel>()).Return(input);

          validator.setup(x => x.is_valid(input)).Return(true);

        };

        Because b = () =>
          sut.process(request);

        It delegates_to_the_original_feature = () =>
          original.received(x => x.process(request));

        static ISupportAUserStory original;
        static IProvideRequestDetails request;
        static IValidateAn<SomeInputModel> validator;
        static SomeInputModel input;
      }
    }

    public class SomeInputModel
    {
    }
  }
}