﻿using System;
using System.Diagnostics;
using System.Windows.Forms;

using HuyaLive;

namespace HuyaLiveHelper
{
    public partial class MainForm : Form, ClientListener
    {
        private HuyaLiveClient client = null;
        private Logger logger = null;

        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            logger = new Logger(new HuyaLive.Debugger());
        }

        public void FlushLog()
        {
            Debug.Flush();
        }

        public void CloseLog()
        {
            Debug.Close();
        }

        public void Print(string message)
        {
            Debug.Print(message);
        }

        public void Print(string format, params object[] args)
        {
            Debug.Print(format, args);
        }

        public void Write(string message)
        {
            Debug.Write(message);
        }

        public void Write(string format, params object[] args)
        {
            Debug.Print(format, args);
        }

        public void WriteLine(string message)
        {
            Debug.WriteLine(message);
        }

        public void WriteLine(string format, params object[] args)
        {
            Debug.WriteLine(format, args);
        }

        public void OnClientStart(object sender)
        {
            Debug.WriteLine("  MainForm::OnClientStart()");
        }

        public void OnClientClose(object sender)
        {
            Debug.WriteLine("  MainForm::OnClientClose()");
        }

        public void OnClientError(object sender, Exception exception, string message)
        {
            Debug.WriteLine("--------------------------------------------------------");
            Debug.WriteLine("  MainForm::OnClientError()");
            Debug.WriteLine("  Eexception: " + exception.ToString());
            Debug.WriteLine("  Message: " + message);
            Debug.WriteLine("--------------------------------------------------------");
        }

        public void OnClientEnter(object sender, EnterMessage message)
        {
            Debug.WriteLine("  MainForm::OnClientEnter()");
        }

        public void OnClientChat(object sender, ChatMessage message)
        {
            Debug.WriteLine("  MainForm::OnClientChat()");
            Debug.WriteLine("  uid = {0}, nickname = {1}, timestamp = {2}, content = {3}, length = {4}.",
                            message.uid, message.nickname, message.timestamp, message.content, message.length);
        }

        public void OnClientGift(object sender, GiftMessage message)
        {
            Debug.WriteLine("  MainForm::OnClientGift()");
        }

        public void OnClientGiftList(object sender, GiftListMessage message)
        {
            Debug.WriteLine("  MainForm::OnClientGiftList()");
        }

        public void OnClientOnline(object sender, OnlineMessage message)
        {
            Debug.WriteLine("  MainForm::OnClientOnline()");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                client = new HuyaLiveClient(this);
                // Shen tu
                //client.Start("666007");
                // Yang qi huang
                client.Start("18001");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.ToString());
                Debug.WriteLine("");
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
            {
                if (client.IsRunning())
                {
                    client.Stop();
                }

                client.Dispose();
                client.SetListener(null);
                client = null;
            }
        }
    }
}
