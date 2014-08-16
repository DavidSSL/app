using System;
using System.Web;

namespace app.web.aspnet
{
  public delegate object ICreatePageInstances(string path, Type type);
}