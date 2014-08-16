using System;
using app.basic_container;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.specs
{
  [Subject(typeof(SimpleFactory))]
  public class SimpleFactorySpecs
  {
    public abstract class concern : Observes<ICreateADependency,
      SimpleFactory>
    {
    }

    public class when_creating_its_object : concern
    {
      Establish c = () =>
      {
        the_object = new SomeObject();
        depends.on<Func<object>>(() => the_object);
      };
      Because b = () =>
        result = sut.create();

      It returns_the_object_created_by_the_provided_factory_delegate_ = () =>
        result.ShouldEqual(the_object);

      static object result;
      static SomeObject the_object;
    }

    public class SomeObject
    {
    }
  }
}