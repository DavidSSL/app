 using System;
 using System.Data;
 using app.basic_container;
 using app.container;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace app.specs
{  
  [Subject(typeof(Container))]  
  public class ContainerSpecs
  {
    public abstract class concern : Observes<IFetchDependencies,
      Container>
    {
        
    }

    public class when_fetching_a_dependency :concern
    {
      public class and_the_factory_creates_the_dependency_successfully : concern
      {
        Establish c = () =>
        {
          factories = depends.on<IGetTheFactoryForADependency>();
          the_connection = fake.an<IDbConnection>();
          the_factory = fake.an<ICreateOneDependency>();

          factories.setup(x => x.get_the_factory_that_can_create(typeof(IDbConnection))).Return(the_factory);

          the_factory.setup(x => x.create()).Return(the_connection);
        };

        Because b = () =>
          result = sut.an<IDbConnection>();

        It returns_the_dependency_created_by_the_factory_for_that_dependency = () =>
          result.ShouldEqual(the_connection);

        static IDbConnection result;
        static IDbConnection the_connection;
        static IGetTheFactoryForADependency factories;
        static ICreateOneDependency the_factory;
      }

      public class in_a_non_generic_context : concern
      {
        Establish c = () =>
        {
          factories = depends.on<IGetTheFactoryForADependency>();
          the_connection = fake.an<IDbConnection>();
          the_factory = fake.an<ICreateOneDependency>();

          factories.setup(x => x.get_the_factory_that_can_create(typeof(IDbConnection))).Return(the_factory);

          the_factory.setup(x => x.create()).Return(the_connection);
        };

        Because b = () =>
          result = sut.an(typeof(IDbConnection));

        It returns_the_dependency_created_by_the_factory_for_that_dependency = () =>
          result.ShouldEqual(the_connection);

        static object result;
        static IDbConnection the_connection;
        static IGetTheFactoryForADependency factories;
        static ICreateOneDependency the_factory;
      }
      public class and_the_factory_for_the_dependency_fails_to_create : concern
      {
        Establish c = () =>
        {
          factories = depends.on<IGetTheFactoryForADependency>();
          the_factory = fake.an<ICreateOneDependency>();
          error = new Exception();
          inner_exception = new Exception();

          depends.on<ICreateAnErrorWhenCreationFails>((inner, type) =>
          {
            inner.ShouldEqual(inner_exception);
            type.ShouldEqual(typeof(IDbConnection));
            return error;
          });

          factories.setup(x => x.get_the_factory_that_can_create(typeof(IDbConnection))).Return(the_factory);

          the_factory.setup(x => x.create()).Throw(inner_exception);
        };

        Because b = () =>
          spec.catch_exception(() => sut.an<IDbConnection>());

        It throws_a_dependency_creation_created_by_the_builder = () =>
          spec.exception_thrown.ShouldEqual(error);

        static IGetTheFactoryForADependency factories;
        static ICreateOneDependency the_factory;
        static Exception error;
        static Exception inner_exception;
      }
    }
  }
}
