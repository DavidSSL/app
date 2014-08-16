using System;
using System.Reflection;

namespace app.basic_container
{
  public delegate ConstructorInfo IPickTheCtorForAType(Type type);
}