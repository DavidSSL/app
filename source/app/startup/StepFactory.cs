using System;

namespace app.startup
{
  public class StepFactory : ICreateAStartupStep
  {
    Func<IProvideStartupServices> get_startup_services;

    public StepFactory(Func<IProvideStartupServices> get_startup_services)
    {
      this.get_startup_services = get_startup_services;
    }

    public IRunAStartupStep create(Type type)
    {
      return (IRunAStartupStep)Activator.CreateInstance(type, get_startup_services()); 
    }
  }
}