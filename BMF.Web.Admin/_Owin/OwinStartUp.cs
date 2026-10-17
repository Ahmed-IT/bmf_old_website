using System;
using System.Configuration;
using System.Data.SqlClient;
using Owin;
using Microsoft.Owin;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(_Owin.OwinStartUp))]
namespace _Owin
{
    public class OwinStartUp
    {
        public void Configuration(IAppBuilder app)
        {
            try
            {
                //Any connection or hub wire up and configuration should go here
                app.MapSignalR();
            }
            catch (Exception LogError)
            {
                throw;
            }
        }
    }
}