using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatClient.ServiceChat;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,IServiceChatCallback
    {
        Boolean isConnected = false;
        ServiceChatClient client;
        Guid id;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void MsgCallback(string message)
        {
            lbChat.Items.Add(message);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
        }

        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                id = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                butCD.Content = "Disconnect";
                isConnected= true;
            }
        }
        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(id);
                client = null;
                tbUserName.IsEnabled = true;
                butCD.Content = "Connect";
                isConnected= false;
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
                DisconnectUser();
            else
                ConnectUser();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (client != null)
            {
                if (e.Key == Key.Enter)
                {
                    client.SendMsg(tbMessage.Text, id);
                    tbMessage.Text = String.Empty;
                }
            }
        }
    }
}
