namespace app.startup
{
  public class Startup
  {
    public static void run()
    {
      StartTheApp.by<InitializingTheContainer>()
        .followed_by<RegisterWebComponents>()
        .finish_by<ConfiguringRoutes>();
    }
  }
}