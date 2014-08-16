using System;

namespace app.basic_container
{
  public class SimpleFactory :ICreateADependency
  {
    Func<object> builder;

    public SimpleFactory(Func<object> builder)
    {
      this.builder = builder;
    }

    public object create()
    {
      return builder();
    }
  }
}