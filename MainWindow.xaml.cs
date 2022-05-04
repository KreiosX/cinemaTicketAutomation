using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
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
using static AsilCinemaTicketAutomation.Operations;

namespace AsilCinemaTicketAutomation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string dir;
        SqlConnection ActiveConnection = new SqlConnection("Server=79.123.228.28,5501;Database=Gunduz_HAcku;User Id=hacku;Password=x7a45tt55;");
        public MainWindow()
        {
            InitializeComponent();
            dir = Directory.GetCurrentDirectory();
            dir = dir + @"\Videos\homepagefragmans\";
            DTimer();
            DisplayInf();
            DisplayFrag();
            if (Perm(UserLoginId, 6) == true)
            {
                FOperations.Visibility = Visibility.Visible;
            }
            else
            {
                FOperations.Visibility = Visibility.Collapsed;
            }
            if (Perm(UserLoginId, 5) == true)
            {
                ATransactions.Visibility = Visibility.Visible;
            }
            else
            {
                ATransactions.Visibility = Visibility.Collapsed;
            }
            if (Perm(UserLoginId, 15) == true)
            {
                ETVision.Visibility = Visibility.Visible;
            }
            else
            {
                ETVision.Visibility = Visibility.Collapsed;
            }
            if (Perm(UserLoginId, 12) == true)
            {
                MProfile.Visibility = Visibility.Visible;
            }
            else
            {
                MProfile.Visibility = Visibility.Collapsed;
            }
            if (Perm(UserLoginId, 14) == true)
            {
                TTransaction.Visibility = Visibility.Visible;
            }
            else
            {
                TTransaction.Visibility = Visibility.Collapsed;
            }
            if (Perm(UserLoginId, 13) == true)
            {
                ticketcancellation.Visibility = Visibility.Visible;
            }
            else
            {
                ticketcancellation.Visibility = Visibility.Collapsed;
            }
            if (Perm(UserLoginId, 16) == true)
            {
                EditSeatz.Visibility = Visibility.Visible;
            }
            else
            {
                EditSeatz.Visibility = Visibility.Collapsed;
            }
            //Code Master
            if (Perm(UserLoginId, 999) == true)
            {
                TTransaction.Visibility = Visibility.Visible;
                MProfile.Visibility = Visibility.Visible;
                ETVision.Visibility = Visibility.Visible;
                ATransactions.Visibility = Visibility.Visible;
                FOperations.Visibility = Visibility.Visible;
                EditSeatz.Visibility = Visibility.Visible;
            }
            if (UserType == "Guest")
            {
                ETVision.Visibility = Visibility.Visible;
                TTransaction.Visibility = Visibility.Visible;
                ticketcancellation.Visibility = Visibility.Collapsed;
                FOperations.Visibility = Visibility.Collapsed;
                ATransactions.Visibility = Visibility.Collapsed;
                EditSeatz.Visibility = Visibility.Collapsed;
                MProfile.Visibility = Visibility.Collapsed;
            }
        }
        public bool Perm(int usid, int authid)
        {
            bool val = false;
            try
            {
                if (ActiveConnection.State == ConnectionState.Closed)
                {
                    ActiveConnection.Open();
                }
                SqlCommand SqlCom = new SqlCommand("SELECT COUNT(id) FROM Authorize where userid=@userid and authorizationid=@authorizationid", ActiveConnection);
                SqlCom.Parameters.AddWithValue("@userid", usid);
                SqlCom.Parameters.AddWithValue("@authorizationid", authid);
                SqlDataReader R = SqlCom.ExecuteReader();
                if (R.Read())
                {
                    if ((int)R[0] == 0)
                    {
                        val = false;
                    }
                    else
                    {
                        val = true;
                    }
                }
                R.Close();
                SqlCom.Dispose();
                ActiveConnection.Close();

            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Error.Message);
            }
            return val;
        }

        public void DTimer()
        {
            DispatcherTimer DTimer = new DispatcherTimer();
            DTimer.Tick += DTimer_Tick;
            DTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            DTimer.Start();
        }

        private void DTimer_Tick(object sender, EventArgs e)
        {
            string date;
            date = DateTime.Now.Date.DayOfWeek.ToString();
            date = date + Environment.NewLine + DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year;
            date = date + Environment.NewLine + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            date1.Text = date;
            CommandManager.InvalidateRequerySuggested();
        }

        private void enterthevision_Click(object sender, RoutedEventArgs e)
        {
            EnterTheVision Etv = new EnterTheVision();
            mainprocess.Children.Clear();
            mainprocess.Children.Add(Etv);
        }

        private void cutticket_Click(object sender, RoutedEventArgs e)
        {
            CutTicket Ct = new CutTicket();
            mainprocess.Children.Clear();
            mainprocess.Children.Add(Ct);
        }

        private void ticketcancellation_Click(object sender, RoutedEventArgs e)
        {
            TicketCancellation Tc = new TicketCancellation();
            mainprocess.Children.Clear();
            mainprocess.Children.Add(Tc);
        }
        private void myprofile_Click(object sender, RoutedEventArgs e)
        {
            MyProfile Mp = new MyProfile();
            mainprocess.Children.Clear();
            mainprocess.Children.Add(Mp);
        }
        private void authorization_Click(object sender, RoutedEventArgs e)
        {
            Authorization A = new Authorization();
            mainprocess.Children.Clear();
            mainprocess.Children.Add(A);
        }

        private void filmoperations_Click(object sender, RoutedEventArgs e)
        {
            FilmTransactions Ft = new FilmTransactions();
            mainprocess.Children.Clear();
            mainprocess.Children.Add(Ft);
        }
        private void DisplayInf()
        {
            LoginUserName = ((LoginPanel)Application.Current.MainWindow).username.Text;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT name,surname,location,photopath FROM userinfo where username=@lguser";
            cmd.Parameters.AddWithValue("@lguser",LoginUserName);
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            SqlDataReader dr;
            ActiveConnection.Open();

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string nsl;
                nsl = (dr["name"].ToString());
                nsl = nsl + " " + (dr["surname"].ToString());
                nsl = nsl + Environment.NewLine + (dr["location"].ToString());
                userinformation.Text = nsl;
            }
            else
            {
                userinformation.Text = "Logged Is As Guest!";
                UserType = "Guest";
            }
            ActiveConnection.Close();
        }

        private void frag_Click(object sender, RoutedEventArgs e)
        {
            Home H = new Home();
            mainprocess.Children.Clear();
            mainprocess.Children.Add(H);
        }

        private void EditSeatz_Click(object sender, RoutedEventArgs e)
        {
            EditSeats ES = new EditSeats();
            mainprocess.Children.Clear();
            mainprocess.Children.Add(ES);
        }

        private void myMediaElement_MouseEnter(object sender, MouseEventArgs e)
        {
            myMediaElement.Width = 900;
            myMediaElement.Height = 450;
            myMediaElement.Volume = 100;
            myMediaElement1.Visibility = Visibility.Hidden;
            myMediaElement2.Visibility = Visibility.Hidden;
            myMediaElement3.Visibility = Visibility.Hidden;
        }

        private void myMediaElement_MouseLeave(object sender, MouseEventArgs e)
        {
            myMediaElement.Width = 400;
            myMediaElement.Height = 200;
            myMediaElement1.Visibility = Visibility.Visible;
            myMediaElement2.Visibility = Visibility.Visible;
            myMediaElement3.Visibility = Visibility.Visible;
            myMediaElement.Volume = 0;
        }

        private void myMediaElement1_MouseEnter(object sender, MouseEventArgs e)
        {
            myMediaElement1.Width = 900;
            myMediaElement1.Height = 450;
            myMediaElement1.Volume = 100;
            myMediaElement.Visibility = Visibility.Collapsed;
            myMediaElement2.Visibility = Visibility.Collapsed;
            myMediaElement3.Visibility = Visibility.Collapsed;
        }

        private void myMediaElement1_MouseLeave(object sender, MouseEventArgs e)
        {
            myMediaElement1.Width = 400;
            myMediaElement1.Height = 200;
            myMediaElement.Visibility = Visibility.Visible;
            myMediaElement2.Visibility = Visibility.Visible;
            myMediaElement3.Visibility = Visibility.Visible;
            myMediaElement1.Volume = 0;
        }

        private void myMediaElement2_MouseEnter(object sender, MouseEventArgs e)
        {
            myMediaElement2.Width = 900;
            myMediaElement2.Height = 450;
            myMediaElement2.Volume = 100;
            myMediaElement1.Visibility = Visibility.Collapsed;
            myMediaElement.Visibility = Visibility.Collapsed;
            myMediaElement3.Visibility = Visibility.Collapsed;
        }

        private void myMediaElement2_MouseLeave(object sender, MouseEventArgs e)
        {
            myMediaElement2.Width = 400;
            myMediaElement2.Height = 200;
            myMediaElement.Visibility = Visibility.Visible;
            myMediaElement1.Visibility = Visibility.Visible;
            myMediaElement3.Visibility = Visibility.Visible;
            myMediaElement2.Volume = 0;
        }

        private void myMediaElement3_MouseEnter(object sender, MouseEventArgs e)
        {
            myMediaElement3.Width = 900;
            myMediaElement3.Height = 450;
            myMediaElement3.Volume = 100;
            myMediaElement1.Visibility = Visibility.Collapsed;
            myMediaElement.Visibility = Visibility.Collapsed;
            myMediaElement2.Visibility = Visibility.Collapsed;
        }

        private void myMediaElement3_MouseLeave(object sender, MouseEventArgs e)
        {
            myMediaElement3.Width = 400;
            myMediaElement3.Height = 200;
            myMediaElement.Visibility = Visibility.Visible;
            myMediaElement1.Visibility = Visibility.Visible;
            myMediaElement2.Visibility = Visibility.Visible;
            myMediaElement3.Volume = 0;
        }

        void DisplayFrag()
        {
            string dirz = Directory.GetCurrentDirectory();
            dirz += @"\Images\needupdate\needupdatehpf.jpg";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT fragpath FROM mainFrag where id=1";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            SqlDataReader dr;
            ActiveConnection.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string me1;
                me1 = dr["fragpath"].ToString();
                if (!File.Exists(dir + me1))
                {
                    myMediaElement.Source = new Uri(dirz, UriKind.RelativeOrAbsolute);
                }
                else
                {
                    myMediaElement.Source = new Uri(dir + me1, UriKind.RelativeOrAbsolute);
                }
                dr.Close();
                ActiveConnection.Close();
            }

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT fragpath FROM mainFrag where id=2";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            SqlDataReader dr1;
            ActiveConnection.Open();
            dr1 = cmd.ExecuteReader();
            if (dr1.Read())
            {
                string me2;
                me2 = dr1["fragpath"].ToString();
                if (!File.Exists(dir + me2))
                {
                    myMediaElement1.Source = new Uri(dirz, UriKind.RelativeOrAbsolute);
                }
                else
                {
                    myMediaElement1.Source = new Uri(dir + me2, UriKind.RelativeOrAbsolute);
                }
                dr1.Close();
                ActiveConnection.Close();
            }

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT fragpath FROM mainFrag where id=3";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            SqlDataReader dr2;
            ActiveConnection.Open();
            dr2 = cmd.ExecuteReader();
            if (dr2.Read())
            {
                string me3;
                me3 = dr2["fragpath"].ToString();
                if (!File.Exists(dir + me3))
                {
                    myMediaElement2.Source = new Uri(dirz, UriKind.RelativeOrAbsolute);
                }
                else
                {
                    myMediaElement2.Source = new Uri(dir + me3, UriKind.RelativeOrAbsolute);
                }
                dr2.Close();
                ActiveConnection.Close();
            }

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT fragpath FROM mainFrag where id=4";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            SqlDataReader dr3;
            ActiveConnection.Open();
            dr3 = cmd.ExecuteReader();
            if (dr3.Read())
            {
                string me4;
                me4 = dr3["fragpath"].ToString();
                if (!File.Exists(dir + me4))
                {
                    myMediaElement3.Source = new Uri(dirz, UriKind.RelativeOrAbsolute);
                }
                else
                {
                    myMediaElement3.Source = new Uri(dir + me4, UriKind.RelativeOrAbsolute);
                }
                dr3.Close();
                ActiveConnection.Close();
            }
            myMediaElement.Play();
            myMediaElement1.Play();
            myMediaElement2.Play();
            myMediaElement3.Play();
        }

        private void myMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            myMediaElement.Position = new TimeSpan(0, 0, 0, 0, 1);
            myMediaElement.Play();
        }

        private void myMediaElement1_MediaEnded(object sender, RoutedEventArgs e)
        {
            myMediaElement1.Position = new TimeSpan(0, 0, 0, 0, 1);
            myMediaElement1.Play();
        }

        private void myMediaElement2_MediaEnded(object sender, RoutedEventArgs e)
        {
            myMediaElement2.Position = new TimeSpan(0, 0, 0, 0, 1);
            myMediaElement2.Play();
        }

        private void myMediaElement3_MediaEnded(object sender, RoutedEventArgs e)
        {
            myMediaElement3.Position = new TimeSpan(0, 0, 0, 0, 1);
            myMediaElement3.Play();
        }

        private void txtScrolling_MouseEnter(object sender, MouseEventArgs e)
        {
            abtus.Visibility = Visibility.Visible;
            mainprocess.Visibility = Visibility.Collapsed;
            txtScrolling.Visibility = Visibility.Visible;
            txtScrolling.FontSize = 25;
            txtScrolling.Text = "Programmer" + Environment.NewLine + Environment.NewLine + "Asil AÇKU" +
            Environment.NewLine + Environment.NewLine + "Contact: " + "ackuofficial@icloud.com" + Environment.NewLine + Environment.NewLine +
            "Graphics" + Environment.NewLine + Environment.NewLine + "Aleyna Çalık" + Environment.NewLine + Environment.NewLine + "Contact: " + "calikaleyna0@gmail.com" + Environment.NewLine + Environment.NewLine +"                         © 2017-2018";
        }

        private void txtScrolling_MouseLeave(object sender, MouseEventArgs e)
        {
            abtus.Visibility = Visibility.Collapsed;
            mainprocess.Visibility = Visibility.Visible;
            txtScrolling.Visibility = Visibility.Collapsed;
        }

        private void feedback_Click(object sender, RoutedEventArgs e)
        {
            FeedBack FB = new FeedBack();
            mainprocess.Children.Clear();
            mainprocess.Children.Add(FB);
        }
    }
}