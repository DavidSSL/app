namespace app.startup
{
  public class ConfiguringRoutes : IRunAStartupStep
  {
    IProvideStartupServices startup_services;

    public ConfiguringRoutes(IProvideStartupServices startup_services)
    {
      this.startup_services = startup_services;
    }

    public void run()
    {
    }
  }
}