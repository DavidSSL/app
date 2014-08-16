namespace app.web.core
{
  public interface IProvideRequestDetails
  {
    InputModel map<InputModel>();
  }

  public interface IProvideRequestDetailsOnADepartment
  {
    string get_department_name();
  }
}