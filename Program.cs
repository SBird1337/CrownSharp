using System;

namespace CrownSharp
{
    class Program
    {
        static void EventHandlerDolphinClientRequest(object sender, DolphinClientRequestEventArgs e)
        {
            DolphinServer server = sender as DolphinServer;
            server.RecvRequest(e.Watch, e.ClockStream, e.DataStream, 0x42);
            Logger.Instance.Info($"{e.Metadata}");
        }
        static void Main(string[] args)
        {
            DolphinServer server = new DolphinServer();
            server.DolphinClientRequest += EventHandlerDolphinClientRequest;
            server.StartAccept();
        }
    }
}
