using System.Collections;
using System.Collections.Generic;
using System.Linq;
using app.container;
using app.web.application.catalog_browsing;
using app.web.application.orders;
using app.web.core;

namespace app.web.aspnet.stubs
{
  public class StubSetOfCommands : IEnumerable<IProcessOneRequest>
  {
    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public IEnumerator<IProcessOneRequest> GetEnumerator()
    {
      yield return command_for<SubmitOrder, SubmitOrderInput>();
      yield return report_for(new GetTheProductsInADepartment());
      yield return report_for(new GetDepartmentsInDepartment());
      yield return report_for(new GetTheMainDepartments());
    }

    IProcessOneRequest command_for<Command, Input>() where Command : ISupportAUserStory, new()
    {
      return new RequestCommand(x => true,
        new ValidatingFeature<Input>(new Command(), null));
    }

    public IProcessOneRequest report_for<Output>(IFetchData<Output> query)
    {
      return new RequestCommand(x => true, new ViewA<Output>(Dependencies.fetch.an<IDisplayInformation>(), query));
    }

    public IProcessOneRequest report_for<Output>(IRunAQuery<Output> query)
    {
      return report_for(query.run);
    }

    public class GetTheMainDepartments : IRunAQuery<IEnumerable<MainDepartmentLineItem>>
    {
      public IEnumerable<MainDepartmentLineItem> run(IProvideRequestDetails request)
      {
        return Enumerable.Range(1, 1000).Select(x => new MainDepartmentLineItem
        {
          name = x.ToString("Department 0")
        });
      }
    }

    public class GetDepartmentsInDepartment : IRunAQuery<IEnumerable<MainDepartmentLineItem>>
    {
      public IEnumerable<MainDepartmentLineItem> run(IProvideRequestDetails request)
      {
        var input = request.map<DeparmentsInDepartmentInput>();
        return Enumerable.Range(1, 1000).Select(x => new MainDepartmentLineItem
        {
          name = x.ToString("Sub Department 0")
        });
      }
    }
  }

  public class GetTheProductsInADepartment : IRunAQuery<IEnumerable<ProductInfoItem>>
  {
    public IEnumerable<ProductInfoItem> run(IProvideRequestDetails request)
    {
      return Enumerable.Range(1, 1000).Select(x => new ProductInfoItem
      {
        name = x.ToString("Product 0")
      });
    }
  }
}