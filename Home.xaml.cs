using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

namespace AsilCinemaTicketAutomation
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public string dir;
        SqlConnection ActiveConnection = new SqlConnection("CS");
        public Home()
        {
            InitializeComponent();
            dir = Directory.GetCurrentDirectory();
            dir = dir + @"\Videos\homepagefragmans\";
            DisplayFrag();
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
    }
}
