using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static AsilCinemaTicketAutomation.Operations;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AsilCinemaTicketAutomation
{
    /// <summary>
    /// Interaction logic for LoginPanel.xaml
    /// </summary>
    public partial class LoginPanel : Window
    {
        public string dir;
        public string dirpp;
        public string dirmb;
        public string diretvp;
        public string dirhpf;
        public string ppath;
        List<string> etvvisions = new List<string>();
        List<string> fragpaths = new List<string>();
        SqlConnection ActiveConnection = new SqlConnection("Server=79.123.228.28,5501;Database=Gunduz_HAcku;User Id=hacku;Password=x7a45tt55;");
        public LoginPanel()
        {
            InitializeComponent();
            dir = Directory.GetCurrentDirectory();
            dirpp = dir + @"\Images\profilpictures\";
            dirmb = dir + @"\Images\moviebanners\";
            diretvp = dir + @"\Images\enterthevisionphotos\";
            dirhpf = dir + @"\Videos\homepagefragmans\";

            MessageBox.Show("Checking The Updated Files For Application Security..!", "Security..!", MessageBoxButton.OK, MessageBoxImage.Information);
            SqlCommand cmd = new SqlCommand("select etvisionphotopath from EnterTheVision", ActiveConnection);
            SqlDataReader drqaa;
            ActiveConnection.Open();
            drqaa = cmd.ExecuteReader();
            while (drqaa.Read())
            {
                etvvisions.Add(drqaa["etvisionphotopath"].ToString());
            }
            for (int i = 0; i < etvvisions.Count; i++)
            {
                if (!File.Exists(diretvp + etvvisions[i]))
                {
                    MessageBox.Show("Visionary Banner Is Being Downloading..!" + Environment.NewLine + " This May Take Some Time(1-5 Minute)", "Downloading..!", MessageBoxButton.OK, MessageBoxImage.Information);
                    MessageBox.Show("You Will Be Informed When The Operation Is Completed ..!", "Information..!", MessageBoxButton.OK, MessageBoxImage.Information);
                    string inputfilepath = @"" + diretvp + etvvisions[i] + "";
                    string ftphost = "ackuasil.cloudaccess.host";
                    string ftpfilepath = "/httpdocs/Images/enterthevisionphotos/" + etvvisions[i] + "";

                    string ftpfullpath = "ftp://" + ftphost + ftpfilepath;

                    using (WebClient request = new WebClient())
                    {
                        request.Credentials = new NetworkCredential("ackuasil", "Bx84qgP5g9");
                        byte[] fileData = request.DownloadData(ftpfullpath);

                        using (FileStream file = File.Create(inputfilepath))
                        {
                            file.Write(fileData, 0, fileData.Length);
                            file.Close();
                        }
                    }
                }
            }
            drqaa.Close();
            cmd = new SqlCommand("select fragpath from mainFrag", ActiveConnection);
            SqlDataReader drqaaa;
            drqaaa = cmd.ExecuteReader();
            while (drqaaa.Read())
            {
                fragpaths.Add(drqaaa["fragpath"].ToString());
            }
            for (int i = 0; i < fragpaths.Count; i++)
            {
                if (!File.Exists(dirhpf + fragpaths[i]))
                {
                    MessageBox.Show("Downloading Current Trailers..! " + Environment.NewLine + " This May Take Some Time(5-10 Minute)", "Downloading..!", MessageBoxButton.OK, MessageBoxImage.Information);
                    MessageBox.Show("You Will Be Informed When The Operation Is Completed ..!", "Information..!", MessageBoxButton.OK, MessageBoxImage.Information);
                    string inputfilepath = @"" + dirhpf + fragpaths[i] + "";
                    string ftphost = "ackuasil.cloudaccess.host";
                    string ftpfilepath = "/httpdocs/Videos/homepagefragmans/" + fragpaths[i] + "";

                    string ftpfullpath = "ftp://" + ftphost + ftpfilepath;

                    using (WebClient request = new WebClient())
                    {
                        request.Credentials = new NetworkCredential("ackuasil", "Bx84qgP5g9");
                        byte[] fileData = request.DownloadData(ftpfullpath);

                        using (FileStream file = File.Create(inputfilepath))
                        {
                            file.Write(fileData, 0, fileData.Length);
                            file.Close();
                        }
                    }
                }
            }
            MessageBox.Show("Required Controls Performed You Will Be Redirecting To The Home Page..!", "Security..!", MessageBoxButton.OK, MessageBoxImage.Information);
            ActiveConnection.Close();
        }
        private void Download(string filePath, string fileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                //filePath: The full path where the file is to be created.
                //fileName: Name of the file to be createdNeed not name on
                //          the FTP server. name name()
                FileStream outputStream = new FileStream(filePath + "\\" +
                   fileName, FileMode.Create);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://"
                   + "ackuasil.cloudaccess.host" + "/" + "httpdocs" + "/" + "Images" + "/" + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential("ackuasil",
                                                           "Bx84qgP5g9");
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static class İnfo
        {
            public static int uid;
        }
        private void ge_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Loging As Guest!","Information..!",MessageBoxButton.OK,MessageBoxImage.Information);
            MainWindow m = new MainWindow();
            m.Show();
            UserLoginId = 5;
            this.Close();
        }

        public static string MD5Sifrele(string sifrelenecekMetin)
        {

            // MD5CryptoServiceProvider sınıfının bir örneğini oluşturduk.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //Parametre olarak gelen veriyi byte dizisine dönüştürdük.
            byte[] dizi = Encoding.UTF8.GetBytes(sifrelenecekMetin);
            //dizinin hash'ini hesaplattık.
            dizi = md5.ComputeHash(dizi);
            //Hashlenmiş verileri depolamak için StringBuilder nesnesi oluşturduk.
            StringBuilder sb = new StringBuilder();
            //Her byte'i dizi içerisinden alarak string türüne dönüştürdük.

            foreach (byte ba in dizi)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }

            //hexadecimal(onaltılık) stringi geri döndürdük.
            return sb.ToString();
        }

        public string deger;
        private void lg_Click(object sender, RoutedEventArgs e)
        {
            deger = MD5Sifrele(password.Password);
            ActiveConnection.Open();
            SqlDataAdapter Sda = new SqlDataAdapter("select usertype,id from userinfo where username=@uname and password=@dgr", ActiveConnection);
            Sda.SelectCommand.Parameters.AddWithValue("@uname",username.Text);
            Sda.SelectCommand.Parameters.AddWithValue("@dgr",deger);
            DataSet Ds = new DataSet();
            Sda.Fill(Ds, "usertype");
            SqlCommand Sc = new SqlCommand("select usertype,id from userinfo where username=@uname and password=@dgr", ActiveConnection);
            Sc.Parameters.AddWithValue("@uname",username.Text);
            Sc.Parameters.AddWithValue("@dgr",deger);
            SqlDataReader dr = Sc.ExecuteReader();
            if (dr.Read())
            {
                UserLoginId = Convert.ToInt32(Ds.Tables[0].Rows[0]["id"]);
                UserType = Ds.Tables[0].Rows[0]["usertype"].ToString();
            }
            dr.Close();
            Sc.Dispose();

            deger = MD5Sifrele(password.Password);


            if (String.IsNullOrWhiteSpace(username.Text) && String.IsNullOrWhiteSpace(password.Password))
            {
                MessageBox.Show("Login Failed! Please Fill In All Fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (ActiveConnection.State == ConnectionState.Closed)
                ActiveConnection.Open();
            SqlCommand sorgula = new SqlCommand("select * from userinfo where username = @uname and password=@dgr", ActiveConnection);
            sorgula.Parameters.AddWithValue("@uname", username.Text);
            sorgula.Parameters.AddWithValue("@dgr", deger);
            SqlDataReader drr = sorgula.ExecuteReader();
            if (drr.Read())
            {
                MainWindow frm = new MainWindow();
                frm.Show();
                this.Close();
                drr.Close();
                //--------------------------------------------
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM userinfo where username=@uname";
                cmd.Parameters.AddWithValue("@uname",username.Text);
                cmd.Connection = ActiveConnection;
                cmd.CommandType = CommandType.Text;

                SqlDataReader drqa;
                drqa = cmd.ExecuteReader();
                if (drqa.Read())
                {
                    ppath = (drqa["photopath"].ToString());
                }
                if (!File.Exists(dirpp + ppath))
                {
                    MessageBox.Show("Downloading Your Profil Picture..!  Please Wait...", "Downloading..!", MessageBoxButton.OK, MessageBoxImage.Information);
                    string inputfilepath = @"" + dirpp + ppath + "";
                    string ftphost = "ackuasil.cloudaccess.host";
                    string ftpfilepath = "/httpdocs/Images/profilpictures/" + ppath + "";

                    string ftpfullpath = "ftp://" + ftphost + ftpfilepath;

                    using (WebClient request = new WebClient())
                    {
                        request.Credentials = new NetworkCredential("ackuasil", "Bx84qgP5g9");
                        byte[] fileData = request.DownloadData(ftpfullpath);

                        using (FileStream file = File.Create(inputfilepath))
                        {
                            file.Write(fileData, 0, fileData.Length);
                            file.Close();
                        }
                    }
                }
                //--------------------------------------------

            }
            else
            {
                MessageBox.Show("Login Failed! Wrong Username or Password", "Error..!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            drr.Close();
            ActiveConnection.Close();
            sorgula.Dispose();

        }

        private void rg_Click(object sender, RoutedEventArgs e)
        {
            registerp.Visibility = Visibility.Visible;
            RegisterPanel Rp = new RegisterPanel();
            registerp.Children.Clear();
            registerp.Children.Add(Rp);
            lg.Margin = new Thickness(0, 115, 750, 0);
            rg.Margin = new Thickness(0, 175, 650, 0);
            ge.Margin = new Thickness(100, 115, 650, 0);
            username.Margin = new Thickness(15, -10, 650, 0);
            usernamei.Margin = new Thickness(0, -10, 750, 0);
            password.Margin = new Thickness(15, 51, 650, 0);
            passwordi.Margin = new Thickness(0, 49, 750, 0);
            otomationi.Margin = new Thickness(0, -20, 625, 0);
            lg.IsEnabled = false;
            rg.IsEnabled = false;
            ge.IsEnabled = false;
            username.IsEnabled = false;
            usernamei.IsEnabled = false;
            password.IsEnabled = false;
            passwordi.IsEnabled = false;
            otomationi.IsEnabled = false;
            lg.Opacity = Opacity * 0.2;
            rg.Opacity = Opacity * 0.2;
            ge.Opacity = Opacity * 0.2;
            username.Opacity = Opacity * 0.2;
            usernamei.Opacity = Opacity * 0.2;
            password.Opacity = Opacity * 0.2;
            passwordi.Opacity = Opacity * 0.2;
            otomationi.Opacity = Opacity * 0.2;
            TLoginPanel.Visibility = Visibility.Visible;
        }

        private void TLoginPanel_Click(object sender, RoutedEventArgs e)
        {
            registerp.Visibility = Visibility.Collapsed;
            RegisterPanel Rp = new RegisterPanel();
            registerp.Children.Clear();
            registerp.Children.Add(Rp);
            lg.Margin = new Thickness(0, 115, 100, 0);
            rg.Margin = new Thickness(0, 175, 0, 0);
            ge.Margin = new Thickness(100, 115, 0, 0);
            username.Margin = new Thickness(15, -10, 0, 0);
            usernamei.Margin = new Thickness(0, -10, 100, 0);
            password.Margin = new Thickness(15, 51, 0, 0);
            passwordi.Margin = new Thickness(0, 49, 100, 0);
            otomationi.Margin = new Thickness(0, -20, 0, 0);
            lg.IsEnabled = true;
            rg.IsEnabled = true;
            ge.IsEnabled = true;
            username.IsEnabled = true;
            usernamei.IsEnabled = true;
            password.IsEnabled = true;
            passwordi.IsEnabled = true;
            otomationi.IsEnabled = true;
            lg.Opacity = Opacity * 1;
            rg.Opacity = Opacity * 1;
            ge.Opacity = Opacity * 1;
            username.Opacity = Opacity * 1;
            usernamei.Opacity = Opacity * 1;
            password.Opacity = Opacity * 1;
            passwordi.Opacity = Opacity * 1;
            otomationi.Opacity = Opacity * 1;
            TLoginPanel.Visibility = Visibility.Collapsed;
        }

        private void lg_MouseEnter(object sender, MouseEventArgs e)
        {
            lg.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void lg_MouseLeave(object sender, MouseEventArgs e)
        {
            lg.Foreground = new SolidColorBrush(Colors.DarkGray);
        }

        private void ge_MouseEnter(object sender, MouseEventArgs e)
        {
            ge.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void ge_MouseLeave(object sender, MouseEventArgs e)
        {
            ge.Foreground = new SolidColorBrush(Colors.DarkGray);
        }

        private void rg_MouseEnter(object sender, MouseEventArgs e)
        {
            rg.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void rg_MouseLeave(object sender, MouseEventArgs e)
        {
            rg.Foreground = new SolidColorBrush(Colors.DarkGray);
        }

        private void TLoginPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            TLoginPanel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void TLoginPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            TLoginPanel.Foreground = new SolidColorBrush(Colors.DarkGray);
        }
    }
}
