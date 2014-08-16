using System;

namespace app.startup
{
  public interface ICreateAStartupStep
  {
    IRunAStartupStep create(Type type);
  }
}