using System;
using System.Text;
using GcnSharp.Core;
using GcnSharp.Logging;
using GcnSharp.Util;

namespace CrownSharpCli
{
    class Program
    {
        private enum CrownLinkCommands : byte
        {
            PING = 10,
            REGISTER = 11,
            LOGIN = 12
        };

        static void EventHandlerDolphinClientRequest(object sender, DolphinClientRequestEventArgs e)
        {
            DolphinServer server = sender as DolphinServer;
            byte[] metaBytes = BitOperation.GetEncodedBytes(e.Metadata);
            CrownLinkCommands command = (CrownLinkCommands)metaBytes[0];
            switch(command)
            {
                case CrownLinkCommands.PING:
                    server.RecvRequest(e.Watch, e.ClockStream, e.DataStream, BitOperation.GetDecodedUInt(Encoding.ASCII.GetBytes("ACK")));
                    break;
                case CrownLinkCommands.REGISTER:
                    byte[] buffer = server.ReceiveBuffer(e.Watch, e.ClockStream, e.DataStream, 8);
                    Logger.Instance.Info(string.Join(",", buffer));
                    break;

            }
        }
        static void Main(string[] args)
        {
            DolphinServer server = new DolphinServer();
            server.DolphinClientRequest += EventHandlerDolphinClientRequest;
            server.StartAccept();
        }
    }
}
