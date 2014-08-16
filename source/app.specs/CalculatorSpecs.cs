using System;
using System.Data;
using System.Security.Principal;
using System.Threading;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using Rhino.Mocks;

namespace app.specs
{
  [Subject(typeof(Calculator))]
  public class CalculatorSpecs
  {
    public abstract class context : Observes<Calculator>
    {
      Establish c = () =>
        connection = depends.on<IDbConnection>();

      protected static IDbConnection connection;
    }

    public class when_created:context
    {
      It does_not_open_its_connection = () =>
        connection.never_received(x => x.Open());
        
    }

    public class when_shutting_off : context
    {
      public class and_they_are_in_the_correct_security_group
      {
        Establish c = () =>
        {
          principal = fake.an<IPrincipal>();
          principal.setup(x => x.IsInRole(Arg<string>.Is.Anything)).Return(true);

          spec.change(() => Thread.CurrentPrincipal).to(principal);
        };

        Because b = () =>
          sut.shut_off();


        It allows_them_to_shut_it_down = () =>
        {

        };

        static IPrincipal principal;
      }
    }
    public class when_adding
    {
      public class two_positive_numbers : context
      {
        Establish c = () =>
        {
          command = fake.an<IDbCommand>();

          connection.setup(x => x.CreateCommand()).Return(command);
        };

        Because b = () =>
          result = sut.add(2, 3);

        It opens_a_connection_to_the_database = () =>
          connection.received(x => x.Open());

        It runs_a_query = () =>
          command.received(x => x.ExecuteNonQuery());
          
        It returns_the_sum = () =>
          result.ShouldEqual(5);

        static int result;
        static IDbCommand command;
      }

      public class a_negative_with_a_positive : context
      {
        Because b = () =>
          spec.catch_exception(() => sut.add(2, -3));

        It does_not_work = () =>
          spec.exception_thrown.ShouldBeAn<ArgumentException>();
      }
    }
  }
}