using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SynchronousSocketListener
{

    // Incoming data from the client.  来自客户端的传入数据。
    public static string data = null;

    public static void StartListening()
    {
        // Data buffer for incoming data.  传入数据的数据缓冲区。
        byte[] bytes = new Byte[1024];

        // Establish the local endpoint for the socket. 为套接字建立本地端点。 
        // Dns.GetHostName returns the name of the Dns。GetHostName返回
        // host running the application.  运行应用程序的主机。
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        // Create a TCP/IP socket.  创建TCP/IP套接字。
        Socket listener = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);


        // Bind the socket to the local endpoint and  将套接字绑定到本地端点，并侦听传入连接。
        // listen for incoming connections.  
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);

            // Start listening for connections. 开始侦听连接  
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                // Program is suspended while waiting for an incoming connection.  程序在等待传入连接时挂起。
                Socket handler = listener.Accept();
                data = null;

                // An incoming connection needs to be processed.  需要处理传入连接。
                while (true)
                {
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                // Show the data on the console.  在控制台上显示数据。
                Console.WriteLine("Text received : {0}", data);

                // Echo the data back to the client.  将数据回显到客户端。
                byte[] msg = Encoding.ASCII.GetBytes(data);

                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }

    public static int Main(String[] args)
    {
        StartListening();
        return 0;
    }
}