using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SynchronousSocketClient
{

    public static void StartClient()
    {
        // Data buffer for incoming data. 传入数据的数据缓冲区
        byte[] bytes = new byte[1024];

        // Connect to a remote device. 连接远程服务 
        try
        {
            // Establish the remote endpoint for the socket. 为套接字建立远程端点 
            // This example uses port 11000 on the local computer. 使用本地计算机的端口11000 
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP  socket.  
            Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.
            // 套接字连接到远程端点。捕捉任何错误
            try
            {
                sender.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());

                // Encode the data string into a byte array.  将数据字符串编码为字节数组。
                byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                // Send the data through the socket.  通过套接字发送数据。
                int bytesSent = sender.Send(msg);

                // Receive the response from the remote device.  从远程设备接收响应。
                int bytesRec = sender.Receive(bytes);
                Console.WriteLine("Echoed test = {0}",
                    Encoding.ASCII.GetString(bytes, 0, bytesRec));

                // Release the socket. 释放 socket
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static int Main(String[] args)
    {
        StartClient();
        return 0;
    }
}