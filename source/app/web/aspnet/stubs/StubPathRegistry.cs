using System;
using System.Collections.Generic;
using app.web.application.catalog_browsing;

namespace app.web.aspnet.stubs
{
  public class StubPathRegistry : IFindPathsToViews
  {
    public string get_path_to_view_that_can_display<Report>()
    {
      if (typeof(Report) == typeof(IEnumerable<MainDepartmentLineItem>))
        return "~/views/DepartmentBrowser.aspx";
      else if (typeof (Report) == typeof (IEnumerable<ProductInfoItem>))
          return "~/views/ProductBrowser.aspx";

      throw new NotImplementedException();
    }
  }
}