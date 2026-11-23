// Decompiled with JetBrains decompiler
// Type: webApiCinexpassDigital.WebApiConfig
// Assembly: webApi - Cinexpass Digital, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E6879F7D-F6C7-4F6D-A234-14D332A50BC7
// Assembly location: C:\cinex\restApiCinexpassImagen\app.publish\bin\webApi - Cinexpass Digital.dll

using System.Web.Http;

namespace webApiCinexpassDigital
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}/{valor1}/{valor2}/{valor3}/{valor4}/{valor5}/{valor6}/{valor7}", new
            {
                valor1 = RouteParameter.Optional,
                valor2 = RouteParameter.Optional,
                valor3 = RouteParameter.Optional,
                valor4 = RouteParameter.Optional,
                valor5 = RouteParameter.Optional,
                valor6 = RouteParameter.Optional,
                valor7 = RouteParameter.Optional
            });
        }
    }
}
