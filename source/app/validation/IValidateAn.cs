namespace app.validation
{
  public interface IValidateAn<InputModel>
  {
    bool is_valid(InputModel input);
  }
}