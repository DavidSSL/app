namespace app.web.core
{
  public class RequestCommand : IProcessOneRequest
  {
    IMatchARequest request_matcher;
    ISupportAUserStory feature;

    public RequestCommand(IMatchARequest request_matcher, ISupportAUserStory feature)
    {
      this.request_matcher = request_matcher;
      this.feature = feature;
    }

    public void process(IProvideRequestDetails input)
    {
      feature.process(input);
    }

    public bool can_process(IProvideRequestDetails request)
    {
      return request_matcher(request);
    }
  }
}