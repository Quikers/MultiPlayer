using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using MultiPlayerLib;

namespace ConsoleApplication1 {
    class Program {
        public static Thread serverHandlerThread;

        public static Hashtable clientsList = new Hashtable();
        public const int maxBuffer = 1024;
        public static bool runServer = false;

        public static Dictionary<string, bool> clientPlayOKList = new Dictionary<string, bool>();

        private static bool playRequestPending = false;
        private static Timer playTimeout;

        protected static int origRow;
        protected static int origCol;

        protected static void WriteAt(string s, int x, int y) {
            try {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            } catch (ArgumentOutOfRangeException e) {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Convert a list to a string
        /// </summary>
        /// <param name="list">The list you want to use</param>
        /// <param name="start">Where the string should start</param>
        /// <param name="end">Where the string should end</param>
        /// <returns>A string containing list items.</returns>
        private static string ListToString(List<string> list, int start, int end) {
            string s = "";

            for (int i = start; i < end; i++) {
                s += (i != start ? " " + list[i] : list[i]);
            }

            return s;
        }

        static void Main (string[] args) {
            Console.ForegroundColor = ConsoleColor.Green;

            bool exit = false;
            while (exit == false) {
                Console.Write("Do you want to set up the default server?\n\"y\" = Default server options\n\"n\" = Manual server options\n\n(y/n): ");
                origRow = Console.CursorTop;
                origCol = Console.CursorLeft;

                ConsoleKeyInfo line = Console.ReadKey();

                char answer;
                if (line.Key == ConsoleKey.Y || line.Key == ConsoleKey.N) {
                    answer = line.Key.ToString()[0];
                    WriteAt((answer == 'Y' ? "y\n" : "n\n"), 0, 0);
                } else if (line.Key == ConsoleKey.Enter) {
                    WriteAt("y\n", 0, 0);
                    answer = 'Y';
                } else {
                    Console.Clear();
                    continue;
                }

                string ipRL = "";
                if (answer == 'Y') {
                    ipRL = "0.0.0.0:733";
                } else if (answer == 'N') {
                    Console.Write("\n======= MultiPlayer Server Options =======\n");

                    Console.Write("\nEnter ip address + port (ex. {127.0.0.1:733} press ENTER to bind to all ip's): ");
                    ipRL = Console.ReadLine();
                } else {
                    Console.Clear();
                    Console.WriteLine("\"" + line.Key.ToString() + "\" is not a valid answer, please enter \"y\" or \"n\".\n");
                    continue;
                }

                IPAddress ip;
                if (ipRL == "")
                    ipRL = "0.0.0.0:733";

                string[] ipPort = ipRL.Split(':');
                if (IPAddress.TryParse(ipPort[0], out ip)) {

                    TcpListener serverSocket = new TcpListener(ip, Convert.ToInt16(ipPort[1]));
                    TcpClient clientSocket = default(TcpClient);

                    serverHandlerThread = new Thread(() => serverHandler(serverSocket, clientSocket, ip, Convert.ToInt16(ipPort[1])));
                    serverHandlerThread.Start();

                    while (true) { // ======================= Commands go here =======================
                        List<string> ans = new List<string>();
                        ans.Add(Console.ReadLine());

                        try {
                            ans.AddRange(ans[0].Split(' '));

                            switch (ans[1]) {
                                default:
                                    Console.WriteLine("\"" + ans + "\" was not recognized as a MultiPlayerServer command.");
                                    break;
                                case "say":
                                    broadcast(new mpMessage("Server", mpMessage.Type.message, ListToString(ans, 2, ans.Count)));
                                    break;
                            }

                            Console.WriteLine("Server: " + ListToString(ans, 2, ans.Count) + "\n");
                        } catch(Exception ex) {
                            Console.WriteLine(ex.ToString());
                        }
                    }

                } else {
                    Console.Clear();
                    Console.WriteLine("Invalid ip address.");
                }
            }
        }

        private static void serverHandler(TcpListener serverSocket, TcpClient clientSocket, IPAddress ip, int port) {
            try {
                serverSocket.Start();
                runServer = true;
                
                Console.WriteLine("\n=========== MultiPlayer Server ===========\nServer started on " + serverSocket.LocalEndpoint + "\n");

                while (runServer == true) {
                    clientSocket = serverSocket.AcceptTcpClient();

                    byte[] bytesFrom = new byte[maxBuffer];
                    mpMessage dataFromClient = null;

                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, 1024);
                    dataFromClient = new mpMessage(Encoding.ASCII.GetString(bytesFrom));

                    if (clientsList.ContainsKey(dataFromClient.from)) {
                        mpMessage usernameInUse = new mpMessage(dataFromClient.from, mpMessage.Type.cmd, "inUse;");
                        sendData(clientSocket, usernameInUse);
                    } else {
                        clientsList.Add(dataFromClient.from, clientSocket);
                        clientPlayOKList.Add(dataFromClient.from, false);

                        if (dataFromClient.type == mpMessage.Type.cmd && dataFromClient.message.IndexOf("join") > -1) {
                            dataFromClient.type = mpMessage.Type.message;
                            dataFromClient.message = dataFromClient.from + " joined.";

                            handleClient client = new handleClient();
                            client.startClient(clientSocket, dataFromClient.from, clientsList);

                            broadcast(dataFromClient);
                        }
                    }
                }

                while (clientSocket.Connected == true)
                    clientSocket.Close();
                clientSocket = null;
                serverSocket.Stop();
                Console.WriteLine("Server stopped");
                Console.ReadLine();
            } catch (Exception ex) {
                if (ex.Message.IndexOf("Only one usage") > -1) {
                    Console.WriteLine("\nPort " + port + " is already in use on " + (ip.ToString() == "0.0.0.0" ? ip.ToString() + " (Any ip)" : ip.ToString()) + "\n");
                } else {
                    Console.WriteLine(ex.Message);
                    clientSocket = null;
                }
            }
        }

        public static void sendData (TcpClient clientSocket, mpMessage data) {
            try {
                NetworkStream writeStream = clientSocket.GetStream();
                byte[] message = data.ToBytes();

                writeStream.Write(message, 0, message.Length);
                writeStream.Flush();
            } catch (Exception ex) {
                DisconnectClient(clientSocket, data.from);
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("To " + data.from + ": " + data.ToString());
        }  //end broadcast function

        public static void DisconnectClient( TcpClient clientSocket, string username )
        {
            while( clientSocket.Connected == true )
                clientSocket.Close();
            clientSocket = null;
            clientsList.Remove( username );
            clientPlayOKList.Remove( username );

            broadcast( new mpMessage( username, mpMessage.Type.message, username + " left" ) );
        }

        public static void broadcast (mpMessage data) {
            if (data.from != "Server") {
                List<DictionaryEntry> remove = new List<DictionaryEntry>();
                foreach (DictionaryEntry Item in clientsList) {
                    TcpClient broadcastSocket = (TcpClient)Item.Value;
                    try {
                        NetworkStream broadcastStream = broadcastSocket.GetStream();
                        byte[] broadcastBytes = data.ToBytes();

                        broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                        broadcastStream.Flush();
                    } catch (Exception ex) {
                        while (broadcastSocket.Connected == true)
                            broadcastSocket.Close();
                        broadcastSocket = null;
                        remove.Add(Item);

                        Console.WriteLine(ex.ToString());
                    }
                }
                
                foreach (DictionaryEntry Item in remove) {
                    clientsList.Remove(Item.Key);
                    clientPlayOKList.Remove(Item.Key.ToString());
                }

                Console.WriteLine("Broadcast: " + data.ToString());
            }
        }  //end broadcast function

        private static List<KeyValuePair<string, bool>> checkPlayOK() {
            List<KeyValuePair<string, bool>> waitingOn = new List<KeyValuePair<string, bool>>();
            foreach (KeyValuePair<string, bool> user in clientPlayOKList) if (user.Value == false) waitingOn.Add(user);

            if (waitingOn.Count > 0) {
                return waitingOn;
            } else {
                List<KeyValuePair<string, bool>> ready = new List<KeyValuePair<string, bool>>();
                ready.Add(new KeyValuePair<string, bool>("ready", true));
                return ready;
            }
        }

        public static List<mpMessage> handleCommands(TcpClient clientSocket, mpMessage data) {
            List<mpMessage> result = new List<mpMessage>();
            string[] msgArr = data.message.Split(';');

            try {
                if (data.type != mpMessage.Type.message && data.type != mpMessage.Type.none) {
                    switch (msgArr[0]) {
                        default:
                            result.Add(new mpMessage(data.from, mpMessage.Type.error, data.from + " sent unrecognized command: " + data.message + ";"));
                            break;
                        case "disconnect":
                            result.Add(new mpMessage(data.from, mpMessage.Type.message, data.from + " left"));

                            DisconnectClient(clientSocket, data.from);
                            break;
                        case "play":
                            playRequestPending = true;
                            clientPlayOKList[data.from] = true;

                            result.Add(new mpMessage(data.from, mpMessage.Type.cmd, "play;" + msgArr[1] + ";"));
                            result.Add(new mpMessage(data.from, mpMessage.Type.message, data.from + " requested to play " + msgArr[1] + ", you have 10 seconds to reply"));

                            playTimeout = new Timer(playTimeout_Tick, null, 0, 10000);
                            break;
                        case "playOK":
                            if (playRequestPending) {
                                List<KeyValuePair<string, bool>> playOKList = checkPlayOK();

                                if (playOKList[0].Value) {

                                    string time = Convert.ToString((DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
                                    result.Add(new mpMessage(data.from, mpMessage.Type.message, "Media will start playing in 5 seconds"));
                                } else {
                                    for (int i = 0; i < playOKList.Count; i++) {
                                        result.Add(new mpMessage(playOKList[i].Key, mpMessage.Type.message, playOKList[i].Key + " didn't answer or denied the play request"));
                                    }
                                }
                            }
                            break;
                        case "playDenied":
                            playRequestPending = false;
                            foreach (KeyValuePair<string, bool> user in clientPlayOKList) {
                                KeyValuePair<string, bool> temp = user;
                                clientPlayOKList.Remove(user.Key);
                                clientPlayOKList.Add(temp.Key, false);
                            }

                            result.Add(new mpMessage(data.from, mpMessage.Type.message, data.from + " denied the play request"));
                            result.Add(new mpMessage(data.from, mpMessage.Type.message, "Play request was terminated"));
                            break;
                    }
                }
                else if (data.type == mpMessage.Type.message && data.type == mpMessage.Type.error) result.Add(data);
                else Console.WriteLine("An error ocurred with the received message.");
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        private static void playTimeout_Tick(object o) {
            playRequestPending = false;
            foreach (KeyValuePair<string, bool> user in clientPlayOKList) {
                KeyValuePair<string, bool> temp = user;
                clientPlayOKList.Remove(user.Key);
                clientPlayOKList.Add(temp.Key, false);

                broadcast(new mpMessage(temp.Key, mpMessage.Type.message, "Play request was terminated: Timeout"));
            }
        }

        public static void writeLine(string s) {
            Console.WriteLine(s);
        }
    }//end Main class


    public class handleClient {
        TcpClient clientSocket;
        NetworkStream networkStream;
        Hashtable clientsList;
        string clNo;

        public void startClient (TcpClient inClientSocket, string clientNo, Hashtable cList) {
            this.clientSocket = inClientSocket;
            this.clNo = clientNo;
            this.clientsList = cList;
            this.networkStream = clientSocket.GetStream();

            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }

        public void closeClientSocket() {
            while (clientSocket.Connected == true) clientSocket.Close();
            clientsList.Remove(clNo);
            Program.clientPlayOKList.Remove(clNo);
            clientSocket = null;
            networkStream = null;
        }

        private void doChat () {
            int requestCount = 0;
            byte[] bytesFrom = new byte[1024];
            mpMessage dataFromClient = null;
            requestCount = 0;

            while (clientSocket != null && clientSocket.Connected == true) {
                try {
                    requestCount = requestCount + 1;
                    networkStream.Read(bytesFrom, 0, 1024);

                    dataFromClient = new mpMessage(bytesFrom);
                    List<mpMessage> result = Program.handleCommands(clientSocket, dataFromClient);
                    for (int i = 0; i < result.Count; i++) Program.broadcast(result[i]);
                } catch (Exception ex) {
                    if (ex.Message.IndexOf("forcibly") > -1)
                        Program.broadcast(new mpMessage(clNo, mpMessage.Type.message, clNo + " left"));
                    else
                        Console.WriteLine(ex.ToString());
                    
                    closeClientSocket();
                }
            }
        }
    }
}