namespace Logic
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Collections.Generic;
    using System.Threading;


    public class UDPComm
    {
        private static IPAddress broadcast;
        private static Socket client = null;
        private static IPEndPoint ep;

        private static TransBuff buffer;

       

        class TransBuff
        {
            private readonly Queue<byte[]> buffQueue = new Queue<byte[]>();

            public void Enqueue(byte[] cmd)
            {
                lock (this)
                {
                    buffQueue.Enqueue(cmd);                  
                }
            }

            //读取
            public byte[] Dequeue()
            {
                byte[] ret = null;
                lock (this)
                {
                    ret = buffQueue.Dequeue();
                    return (byte[])ret.Clone(); 
                }
            }

            public bool IsEmpty
            {
                get
                {
                    return buffQueue.Count > 0;
                }
            }
        }

        public static void Send(byte[] b)
        {
            buffer.Enqueue(b);            
        }


        public static bool init(string broadIP)
        {

            buffer = new TransBuff();

            int index = broadIP.LastIndexOf('.');
            string ipaddr = broadIP.Substring(0, index) + ".255";           
            broadcast = IPAddress.Parse(ipaddr);

            if (client == null)
            {
                client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                client.EnableBroadcast = true;
                ep = new IPEndPoint(broadcast, 0x4414);
            }           

            return true;
        }


        public void transThread()
        {

            while (true)
            {
                Thread.Sleep(80);
                if (buffer.IsEmpty)
                {
                    byte[] a = buffer.Dequeue();
                    if (a != null)
                    {
                        if (a.Length > 0)
                        {
                            try
                            {
                                client.SendTo(a, ep);
                            }
                            catch (Exception ee)
                            {
                                Console.WriteLine(ee.ToString());
                            }
                        }
                    }
                }


            }

        }     

        public static void SendOLD(byte[] b)
        {
            if (client == null)
            {
                client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                client.EnableBroadcast = true;
               // broadcast = IPAddress.Parse("192.168.0.255");
                ep = new IPEndPoint(broadcast, 0x4414);
            }
            try
            {
                client.SendTo(b, ep);
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
            }
        }
    }
}

