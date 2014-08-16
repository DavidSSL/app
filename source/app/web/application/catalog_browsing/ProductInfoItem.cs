using app.web.core;

namespace app.web.application.catalog_browsing
{
  public class ProductInfoItem
  {
    public string name { get; set; }
  }

  public interface IFetchData
  {
    Report fetch_using<Report>(IRunAQuery<Report> query); 
  }
}