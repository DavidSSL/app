using System;
using System.Collections.Generic;
using app.basic_container;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.specs
{
  [Subject(typeof(DependencyFactories))]
  public class DependencyFactoriesSpecs
  {
    public abstract class concern : Observes<IGetTheFactoryForADependency, DependencyFactories>
    {
    }

    public class when_getting_the_factory_for_a_dependency : concern
    {
      public class and_it_has_the_factory : concern
      {
        Establish c = () =>
        {
          the_type = typeof(SomeObject);

          my_dependency_factory = fake.an<ICreateOneDependency>();
          my_dependency_factory.setup(x => x.can_create(the_type)).Return(true);
          var list = new List<ICreateOneDependency>();
          list.Add(my_dependency_factory);

          depends.on<IEnumerable<ICreateOneDependency>>(list);
        };

        Because b = () =>
          result = sut.get_the_factory_that_can_create(the_type);

        It returns_the_factory_for_the_type = () =>
          result.ShouldEqual(my_dependency_factory);

        static Type the_type;
        static ICreateOneDependency result;
        static ICreateOneDependency my_dependency_factory;
      }

      public class and_it_does_not_have_the_factory : concern
      {
        Establish c = () =>
        {
          the_type = typeof(SomeObject);
          special_case = fake.an<ICreateOneDependency>();

          var list = new List<ICreateOneDependency>();

          depends.on<IEnumerable<ICreateOneDependency>>(list);
          depends.on<ICreateAFactoryWhenOneIsMissing>(x =>
          {
            x.ShouldEqual(the_type);
            return special_case;
          });
        };

        Because b = () =>
          result = sut.get_the_factory_that_can_create(the_type);

        It returns_the_missing_factory = () =>
          result.ShouldEqual(special_case);

        static Type the_type;
        static ICreateOneDependency result;
        static ICreateOneDependency special_case;
      }

    }
    class SomeObject
    {
    }
  }
}