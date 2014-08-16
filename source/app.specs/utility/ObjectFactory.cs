using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using developwithpassion.specifications.extensions;

namespace app.specs.utility
{
  public class ObjectFactory
  {
    public class expressions
    {
      public static ExpressionUtil<T> to_target<T>()
      {
        return new ExpressionUtil<T>();
      }

      public class ExpressionUtil<T>
      {
        public ConstructorInfo get_ctor_using(Expression<Func<T>> func)
        {
          return func.Body.downcast_to<NewExpression>().Constructor;


        }
      }
    }

    public class web
    {
      public static HttpContext create_http_context()
      {
        return new HttpContext(create_request(), create_response());
      }

      static HttpRequest create_request()
      {
        return new HttpRequest("DepartmentBrowser.aspx", "http://localhost/department_browser.aspx", String.Empty);
      }

      static HttpResponse create_response()
      {
        return new HttpResponse(new StringWriter());
      }
    }
  }
}