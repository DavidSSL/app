using System;
using System.Linq;
using app.container;

namespace app.basic_container
{
  public class AutomaticallyWiringFactory : ICreateADependency
  {
    Type type_to_create;
    IFetchDependencies container;
    IPickTheCtorForAType ctor_selection;

    public AutomaticallyWiringFactory(IPickTheCtorForAType ctor_selection, Type type_to_create,
      IFetchDependencies container)
    {
      this.ctor_selection = ctor_selection;
      this.type_to_create = type_to_create;
      this.container = container;
    }

    public object create()
    {
      var ctor = ctor_selection(type_to_create);
      var items = ctor.GetParameters().Select(x => container.an(x.ParameterType));
      return ctor.Invoke(items.ToArray());
    }
  }
}