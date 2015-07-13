using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheremeClient
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream streamToServer;
        private TcpClient clientReceive;
        private NetworkStream streamToServerReceive;
        delegate void AppendTextCallback(string text);

        public Form1()
        {
            InitializeComponent();
        }

        private void appendText(string text)
        {
            if (this.textBoxServer.InvokeRequired)
            {
                AppendTextCallback d = new AppendTextCallback(appendText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBoxServer.Text += text + Environment.NewLine;
            }

            //this.textBoxServer.Text += text + Environment.NewLine;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            appendText("Connecting");
            client = new TcpClient();
            clientReceive = new TcpClient();
            try
            {
                client.Connect("192.168.1.46", 31036);
                appendText("Connected Send" + client.Client.LocalEndPoint + " -> " + client.Client.RemoteEndPoint);
                clientReceive.Connect("192.168.1.46", 31037);
                appendText("Connected Receive" + clientReceive.Client.LocalEndPoint + " -> " + clientReceive.Client.RemoteEndPoint);
            }
            catch (Exception ex)
            {
                appendText(ex.Message);
                return;
            }

            textBoxSend.Visible = true;
            buttonSend.Visible = true;

            streamToServer = client.GetStream();
            streamToServerReceive = clientReceive.GetStream();

            Thread listenThread = new Thread(new ThreadStart(handleServerMessage));
            listenThread.Start();
        }

        public void handleServerMessage()
        {
            appendText("Listening");
            int BufferSize = 8196;

            while (true)
            {
                int bytesRead;
                byte[] buffer = new byte[BufferSize];
                bytesRead = streamToServerReceive.Read(buffer, 0, BufferSize);

                if (bytesRead == 0)
                    continue;

                string msg = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                appendText(msg);
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (textBoxSend.Text == "")
            {
                MessageBox.Show("Message is empty.");
                return;
            }
            

            byte[] buffer = Encoding.Unicode.GetBytes(textBoxSend.Text); 
    
            lock(streamToServer)
            {
                streamToServer.Write(buffer, 0, buffer.Length);
            }

            textBoxSend.Text = "";
        }

        private void textBoxSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                buttonSend_Click(null, null);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
