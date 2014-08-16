namespace app.web.core
{
  public delegate Report IFetchData<out Report>(IProvideRequestDetails request);

  public interface IRunAQuery<Report>
  {
    Report run(IProvideRequestDetails request);
  }
}