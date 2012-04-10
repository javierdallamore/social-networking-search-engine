using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf;

namespace QueryExcecutionEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Host host = HostFactory.New(x =>
            {
                x.Service<IExcecutionEngineService>(s =>
                {
                    s.SetServiceName("socialNetworking.search.Worker");
                    s.ConstructUsing(builder =>
                    {
                        var service = new DBQueryExecutionImpl();
                        service.Initialize();
                        return service;
                    });
                    s.WhenStarted(server => server.Start());
                    s.WhenPaused(server => server.Pause());
                    s.WhenContinued(server => server.Resume());
                    s.WhenStopped(server => server.Stop());
                });

                x.RunAsLocalSystem();

                //x.SetDescription(Configuration.ServiceDescription);
                x.SetDisplayName("Social Networking Searche Engine");
                x.SetServiceName("socialNetworking.search.Worker");
            });

            host.Run();
        }
    }
}
