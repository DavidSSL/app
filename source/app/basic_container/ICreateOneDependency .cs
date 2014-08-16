using System;

namespace app.basic_container
{
	public interface ICreateOneDependency : ICreateADependency
	{
		bool can_create(Type type);
	}

	public interface ICreateADependency
	{
		object create();
	}

  public class CreateOneDependency : ICreateOneDependency
  {
    ICreateADependency factory;
    Predicate<Type> type_specification;

    public CreateOneDependency(ICreateADependency factory, Predicate<Type> type_specification)
    {
      this.factory = factory;
      this.type_specification = type_specification;
    }

    public object create()
    {
      return factory.create();
    }

    public bool can_create(Type type)
    {
      return type_specification(type);
    }
  }
}