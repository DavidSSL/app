using app.validation;

namespace app.web.core
{
  public class ValidatingFeature<InputModel> : ISupportAUserStory
  {
    ISupportAUserStory story;
    IValidateAn<InputModel> validator;

    public ValidatingFeature(ISupportAUserStory story, IValidateAn<InputModel> validator)
    {
      this.story = story;
      this.validator = validator;
    }


    public void process(IProvideRequestDetails input)
    {
      var input_model = input.map<InputModel>();
      if (validator.is_valid(input_model)) story.process(input);
    }
  }
}