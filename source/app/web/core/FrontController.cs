namespace app.web.core
{
  public class FrontController : IProcessWebRequests
  {
    IFindCommandsThatCanProcessRequests command_registry;

    public FrontController(IFindCommandsThatCanProcessRequests command_registry)
    {
      this.command_registry = command_registry;
    }

    public void process(IProvideRequestDetails request)
    {
      var command = command_registry.get_the_command_that_can_process(request);
      command.process(request);
    }
  }
}