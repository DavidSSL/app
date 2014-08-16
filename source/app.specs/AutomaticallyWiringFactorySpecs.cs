using System;
using System.Data;
using System.Reflection;
using app.basic_container;
using app.container;
using app.specs.utility;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace app.specs
{
  [Subject(typeof(AutomaticallyWiringFactory))]
  public class AutomaticallyWiringFactorySpecs
  {
    public abstract class concern : Observes<ICreateADependency,
      AutomaticallyWiringFactory>
    {
    }

    public class when_creating_a_component : concern
    {
      Establish c = () =>
      {
        the_other = new SomeOtherObject();
        the_reader = fake.an<IDataReader>();
        the_connection = fake.an<IDbConnection>();
        container = depends.on<IFetchDependencies>();

        greediest_constructor = ObjectFactory.expressions.to_target<ItemWithLotsOfCollaborators>()
          .get_ctor_using(() => new ItemWithLotsOfCollaborators(null, null, null));

        depends.on(typeof(ItemWithLotsOfCollaborators));
        depends.on<IPickTheCtorForAType>(x => greediest_constructor);

        container.setup(x => x.an(typeof(IDbConnection))).Return(the_connection);
        container.setup(x => x.an(typeof(IDataReader))).Return(the_reader);
        container.setup(x => x.an(typeof(SomeOtherObject))).Return(the_other);
      };

      Because b = () =>
        result = sut.create();

      It return_an_instance_of_the_component_with_all_of_its_dependencies = () =>
      {
        var item = result.ShouldBeAn<ItemWithLotsOfCollaborators>();
        item.connection.ShouldEqual(the_connection);
        item.reader.ShouldEqual(the_reader);
        item.reader.ShouldEqual(the_reader);
        item.other.ShouldEqual(the_other);
      };

      static object result;
      static IDbConnection the_connection;
      static IDataReader the_reader;
      static SomeOtherObject the_other;
      static ConstructorInfo greediest_constructor;
      static IFetchDependencies container;
    }

    public class ItemWithLotsOfCollaborators
    {
      public IDbConnection connection;
      public IDataReader reader;
      public SomeOtherObject other;

      public ItemWithLotsOfCollaborators(IDbConnection connection, SomeOtherObject other, IDataReader reader)
      {
        this.connection = connection;
        this.other = other;
        this.reader = reader;
      }

      public ItemWithLotsOfCollaborators(IDbConnection connection, SomeOtherObject other)
      {
        this.connection = connection;
        this.other = other;
      }

      public ItemWithLotsOfCollaborators(IDbConnection connection)
      {
        this.connection = connection;
      }
    }

    public class SomeOtherObject
    {
    }
  }
}