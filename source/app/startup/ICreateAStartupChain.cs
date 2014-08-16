using System;

namespace app.startup
{
  public delegate IBuildAStartupChain ICreateAStartupChain(Type first_step);
}