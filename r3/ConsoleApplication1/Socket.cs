using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Reflection;
using ConsoleApplication1.Packets;
using Storage;
using System.Data;

namespace ConsoleApplication1
{
    class NewSocket
    {
        private TcpListener tcpListener;
        private Thread listenThread;
        public static TcpClient tcpClient;
        public static NetworkStream clientStream;
        public static string pktData;
        public static string[] split;
        public static string[] slash;

        public void Server()
        {
            this.tcpListener = new TcpListener(IPAddress.Any, Program.Configuration.TryParseInt32("net.tcp.port"));
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();

            Console.WriteLine("Server is listening on port "+ Program.Configuration.TryParseInt32("net.tcp.port") + ".\n");
        }

        public void ListenForClients()
        {
            try
            {
                this.tcpListener.Start();

                while (true)
                {
                    TcpClient client = this.tcpListener.AcceptTcpClient();

                    Console.WriteLine("Open connection [" + client.Client.RemoteEndPoint.ToString().Split(':')[0] + "]\n");
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientThread.Start(client);
                }
            }
            catch
            {
                Console.WriteLine("SOME FUCKING PROGRAM IS USING PORT 90! RAWR!");
            }
        }

        public void HandleClientComm(object client)
        {
            tcpClient = (TcpClient)client;
            clientStream = tcpClient.GetStream();

            SendData(clientStream, "HELLO");

            byte[] message = new byte[4000];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = clientStream.Read(message, 0, 4000);
                }
                catch
                {
                    break;
                }

                if (bytesRead == 0)
                {
                    break;
                }

                ASCIIEncoding encoder = new ASCIIEncoding();
                string NewData = encoder.GetString(message, 0, bytesRead);
                pktData = NewData.Substring(3);
                split = NewSocket.pktData.Split(' ');
                slash = NewSocket.pktData.Split('/');
                string[] split13 = NewData.Split((Char)13);
                string[] RawSplit = NewData.Split(' ');

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[" + tcpClient.Client.RemoteEndPoint.ToString().Split(':')[0] + "]");
                Console.Write(" --> " + NewData.Substring(3) + "\n");
                Console.ResetColor();

                if (pktData.Contains("figure="))
                {

                    string name = split13[0].Split(' ')[2].Replace("name=", ""); ;
                    string password = split13[1].Replace("password=", "");
                    string email = split13[2].Replace("email=", "");
                    string figure = split13[3].Replace("figure=", "");
                    string bday = split13[5].Replace("birthday=", "");
                    string phone = split13[6].Replace("phonenumber=", "");
                    string motto = split13[7].Replace("customData=", "");
                    int hasReadAgreement = Convert.ToInt32(split13[8].Replace("has_read_agreement=", ""));
                    string sex = split13[9].Replace("sex=", "");

                    using (DatabaseClient dbClient = Program.GetDatabase().GetClient())
                    {
                        dbClient.AddParamWithValue("username", name);
                        Boolean dbRow = dbClient.ReadBoolean("SELECT * FROM members WHERE username = @username;");

                        if (dbRow == false) //User doesn't exist
                        {
                            PacketHandler.HasRegistered = true;
                            dbClient.AddParamWithValue("name", name);
                            dbClient.AddParamWithValue("pass", password);
                            dbClient.AddParamWithValue("email", email);
                            dbClient.AddParamWithValue("figure", figure);
                            dbClient.AddParamWithValue("motto", motto);
                            dbClient.AddParamWithValue("hasRead", hasReadAgreement);
                            dbClient.AddParamWithValue("sex", sex);
                            dbClient.AddParamWithValue("bday", bday);
                            dbClient.ExecuteQuery("INSERT INTO members (username, password, role, email, coins, motto, console_motto, figure, birthday, country, sex, badge, had_read_agreement) VALUES" 
                                                + " (@name, @pass, 1, @email, 1337,  @motto, '', @figure, @bday, '', @sex, 0, 1)");
                        }
                        else
                        {
                        }
                    }
                }

                try
                {
                    Invoke<PacketHandler>(NewData.Split(' ')[2]);
                }
                catch { }
            }

            Console.WriteLine("Close connection [" + tcpClient.Client.RemoteEndPoint.ToString().Split(':')[0] + "]");
            //tcpClient.Client.Close();
        }

        public void Invoke<T>(string methodName) where T : new()
        {
            T instance = new T();
            MethodInfo method = typeof(T).GetMethod(methodName);
            method.Invoke(instance, null);
        }

        public static void SendData(NetworkStream clientStream, string Data)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();

            byte[] buffer = encoder.GetBytes("#" + Data + (char)13 + "##");
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("["+ tcpClient.Client.RemoteEndPoint.ToString().Split(':')[0] + "]");
            Console.Write(" <-- " + Data + "\n");
            Console.ResetColor();
        }
        public static void SendData(NetworkStream clientStream, serverMessage Data)
        {
            NewSocket.SendData(clientStream, Data.ToString());
        }
    }
}
