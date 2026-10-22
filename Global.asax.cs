// Decompiled with JetBrains decompiler
// Type: webApiCinexpassDigital.WebApiApplication
// Assembly: webApi - Cinexpass Digital, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E6879F7D-F6C7-4F6D-A234-14D332A50BC7
// Assembly location: C:\cinex\restApiCinexpassImagen\app.publish\bin\webApi - Cinexpass Digital.dll

using System;
using System.Web;
using System.Web.Http;

namespace webApiCinexpassDigital
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
