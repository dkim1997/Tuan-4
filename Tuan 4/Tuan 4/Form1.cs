using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace Tuan_4
{
    public partial class Form1 : Form
    {
        IPEndPoint ipep;
        public Form1()
        {
            InitializeComponent();
        }

        private void tcpbtn_Click(object sender, EventArgs e)
        {
            TcpClient server = new TcpClient();
            ipep = new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text));
            try
            {
                
                server.Connect(ipep);
                label2.Text = "Port mo";
            }
            catch (SocketException)
            {
                label2.Text = "Port khong mo";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1080];
            string k = "hello";
            data = Encoding.ASCII.GetBytes(k);
            UdpClient client = new UdpClient(int.Parse(textBox2.Text));
            ipep = new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text));
            var timeToWait = TimeSpan.FromSeconds(1);

            var asyncResult = client.BeginReceive(null, null);
            asyncResult.AsyncWaitHandle.WaitOne(timeToWait);
            client.Connect(ipep);
            if (asyncResult.IsCompleted)
            {
                try
                {
                    client.Send(data,data.Length);
                    client.Receive(ref ipep);
                    label2.Text = "port mo";
                }
                catch (Exception ex)
                {
                    label2.Text = "port ko mo";
                }
            }
            else
            {
                label2.Text = "time out";
            }
        }
    }
}
