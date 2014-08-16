namespace app.web.core
{
  public interface IFindCommandsThatCanProcessRequests
  {
    IProcessOneRequest get_the_command_that_can_process(IProvideRequestDetails request);
  }
}