using System.Collections.Generic;
using app.basic_container;

namespace app.startup
{
  public interface IProvideStartupServices : IEnumerable<ICreateOneDependency>
  {
    void register<Contract, Implementation>();
    void register<Contract>(Contract instance);
  }
}