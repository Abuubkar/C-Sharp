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
using System.Windows.Threading;
using AIMLbot;
namespace ChatBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bot Ai;
        User user;
        Request request;
        Result result;
        bool flag;
        bool visit = false;
        DispatcherTimer t;
        DateTime start;
        string remainder;
        public MainWindow()
        {
            Ai = new Bot();
            Ai.loadSettings();
            Ai.loadAIMLFromFiles();
            flag = false;
            InitializeComponent();
            botText.Text= "hello";
            }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            string[] text = userText.Text.ToLower().Split();
            if (!text.Contains("reminder") && flag == false)
            {
                Ai.isAcceptingUserInput = false;
                user = new User("Abubakar: ", Ai);
                Ai.isAcceptingUserInput = true;
                request = new Request(userText.Text, user, Ai);
                result = Ai.Chat(request);
                botText.Text = "Bot: " + result.Output;
            }
            else if (flag == true)
            {
                botText.Text= "Enter Date:\nIn Format (Month/Day/Year HH:MM:SS AM)\n(02/27/2020 02:30:60 PM) ";
                DateTime userDate;
                if(visit == false)
                    goto right;
                if (DateTime.TryParse(userText.Text, out userDate))
                {
                    botText.Text = "Reminder for: " + userDate;
                    start = userDate;
                    t = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 50), DispatcherPriority.Background,
              t_Tick, Dispatcher.CurrentDispatcher); t.IsEnabled = true;
                    start = DateTime.Now;

                    flag = false;
                    goto right;
                }
                botText.Text = "Enter Date:\nIn Format (Month/Day/Year HH:MM:SS AM)\n(02/27/2020 02:30:60 PM) ";

            right:
                visit = true;
            }
            else if (flag == false)
            {
                botText.Text = "ok";
                botText.Text = "Enter Date:\nIn Format (Month/Day/Year HH:MM:SS AM)\n(02/27/2020 02:30:60 PM) ";
                flag = true;
            }
                 
        }
        private void t_Tick(object sender, EventArgs e)
        {
            TimerDisplay.Text = Convert.ToString(DateTime.Now - start);
            if(DateTime.Now == start)
            {
                botText.Text = "You have reminder ";  
            }
        }
    }
}
