using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheremeServer
{
    class Program
    {
        static List<TcpClient> clients, senders;
        static int BufferSize = 8192;
        static TcpListener listener, sender;

        static void Main(string[] args)
        {
            clients = new List<TcpClient>();
            senders = new List<TcpClient>();

            IPAddress ip = new IPAddress(new byte[] { 192, 168, 1, 46 });
            listener = new TcpListener(ip, 31036);
            sender = new TcpListener(ip, 31037);

            Console.WriteLine("Listening");

            Thread listenerThread = new Thread(new ThreadStart(ListenListener));
            listenerThread.Start();
            Thread senderThread = new Thread(new ThreadStart(ListenSender));
            senderThread.Start();
        }

        static void ListenSender()
        {
            sender.Start();
            while (true)
            {
                TcpClient remoteClient = sender.AcceptTcpClient();
                lock (senders)
                {
                    senders.Add(remoteClient);
                    Console.WriteLine("Connected {0} <- {1}", remoteClient.Client.LocalEndPoint, remoteClient.Client.RemoteEndPoint);
                }
            }
        }

        static void ListenListener()
        {
            listener.Start();
            while (true)
            {
                TcpClient remoteClient = listener.AcceptTcpClient();
                Thread handleThread = new Thread(new ParameterizedThreadStart(HandleListener));
                handleThread.Start(remoteClient);
            }
        }

        static void HandleListener(object value)
        {
            TcpClient remoteClient = (TcpClient)value;
            lock (remoteClient) 
            {
                clients.Add(remoteClient);
                Console.WriteLine("Connected {0} <- {1}", remoteClient.Client.LocalEndPoint, remoteClient.Client.RemoteEndPoint);
            }
            try
            {
                while (true)
                {
                    NetworkStream streamToClient = remoteClient.GetStream();
                    byte[] buffer = new byte[BufferSize];
                    int bytesRead = streamToClient.Read(buffer, 0, BufferSize);

                    if (bytesRead == 0)
                        continue;

                    Console.WriteLine("Reading data, {0} bytes ...", bytesRead);

                    string msg = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received: {0}", msg);

                    SendMessage(msg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void SendMessage(string msg)
        {
            lock (senders)
            {
                int no = 0;
                foreach (TcpClient sender in senders)
                {
                    try
                    {
                        Console.Write("Send {0}:", ++no);
                        byte[] temp = Encoding.Unicode.GetBytes(msg);
                        NetworkStream streamToClient = sender.GetStream();
                        streamToClient.Write(temp, 0, temp.Length);
                        streamToClient.Flush();
                        Console.WriteLine("'{0}' sent", msg);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
