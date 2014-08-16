using System;
using System.Linq;
using System.Web.Compilation;
using app.basic_container;
using app.core;
using app.web.core;

namespace app.web.aspnet.stubs
{
  public class WebStubs
  {
    public static ICreateAControllerRequest request_factory = (request) =>
    {
      return new StubRequest();
    };

    public static ICreateACommandWhenOneCantBeFound missing_command_factory = delegate
    {
      throw new NotImplementedException("You dont have a command to run this request");
    };

    public static ICreateAFactoryWhenOneIsMissing missing_dependency_factory = (type) =>
    {
      throw new NotImplementedException(string.Format("No factory for {0}", type.FullName));
    };

    public static ICreatePageInstances create_page = BuildManager.CreateInstanceFromVirtualPath;

    public static ICreateAnErrorWhenCreationFails dependency_creation_error = (inner, type) =>
    {
      throw new NotImplementedException(string.Format("The type {0} could not be created", type), inner); 
    };

    public static IPickTheCtorForAType greediest_picker =
      x => x.GetConstructors().OrderByDescending(y => y.GetParameters().Count()).First();

    public class StubRequest : IProvideRequestDetails
    {
      public InputModel map<InputModel>()
      {
        return Activator.CreateInstance<InputModel>();
      }
    }

    public static Predicate<Type> is_a<T>()
    {
      return x => x == typeof(T);
    }

    public static Predicate<Type> is_a(Type type)
    {
      return x => x == type;
    }
  }
}