namespace app.web.core
{
  public interface IProcessOneRequest : ISupportAUserStory
  {
    bool can_process(IProvideRequestDetails request);
  }
}