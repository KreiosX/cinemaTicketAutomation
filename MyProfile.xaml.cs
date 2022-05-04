using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static AsilCinemaTicketAutomation.Operations;

namespace AsilCinemaTicketAutomation
{
    /// <summary>
    /// Interaction logic for MyProfile.xaml
    /// </summary>
    public partial class MyProfile : UserControl
    {
        public string dir;
        SqlConnection ActiveConnection = new SqlConnection("Server=79.123.228.28,5501;Database=Gunduz_HAcku;User Id=hacku;Password=x7a45tt55;");
        public MyProfile()
        {
            InitializeComponent();
            dir = Directory.GetCurrentDirectory();
            dir = dir + @"\Images\profilpictures\";
            DisplayData();
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

        public bool Perm(int usid, int authid)
        {
            bool val = false;
            try
            {
                if (ActiveConnection.State == ConnectionState.Closed)
                {
                    ActiveConnection.Open();
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
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Error.Message);
            }
            return val;
        }

        public void ClearData()
        {
            npass.Password = null;
            npassa.Password = null;
        }

        private void updatep_Click(object sender, RoutedEventArgs e)
        {
            if (npass.Password == npassa.Password)
            {
                string value = MD5Sifrele(npass.Password);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE userinfo SET password=@password WHERE username=@loginusname;";
                cmd.Connection = ActiveConnection;
                cmd.CommandType = CommandType.Text;
                ActiveConnection.Open();
                cmd.Parameters.AddWithValue("@loginusname", LoginUserName);
                cmd.Parameters.AddWithValue("@password", value);
                cmd.ExecuteNonQuery();
                ActiveConnection.Close();
                ClearData();
                MessageBox.Show("Your Password Has Been Updated..!", "Congratulations..!", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                MessageBox.Show("Passwords Do Not Match..!", "Password Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void updateui_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE userinfo SET surname=@surname,location=@location,phone=@phone WHERE username=@lguser;";
            cmd.Parameters.AddWithValue("@lguser", LoginUserName);
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;
            ActiveConnection.Open();
            cmd.Parameters.AddWithValue("@surname", usurname.Text);
            cmd.Parameters.AddWithValue("@location", ulocation.Text);
            cmd.Parameters.AddWithValue("@phone", uphone.Text);
            cmd.ExecuteNonQuery();
            ActiveConnection.Close();
            DisplayData();
            MessageBox.Show("Your Information Has Been Updated..!", "Congratulations..!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void DisplayData()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM userinfo where username=@lguser";
            cmd.Parameters.AddWithValue("@lguser",LoginUserName);
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            SqlDataReader dr;
            ActiveConnection.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string ppath;
                ppath = (dr["photopath"].ToString());
                uname.Text = (dr["name"].ToString());
                usurname.Text = (dr["surname"].ToString());
                ucitizenship.Text = (dr["citizenshipnumber"].ToString());
                uusername.Text = (dr["username"].ToString());
                upassword.Password = (dr["password"].ToString());
                ulocation.Text = (dr["location"].ToString());
                utype.Text = (dr["usertype"].ToString());
                uphone.Text = (dr["phone"].ToString());
                ugender.Text = (dr["gender"].ToString());
                uage.Text = (dr["age"].ToString());
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri(dir + ppath);
                logo.EndInit();
                upphoto.Source = logo;
            }
            ActiveConnection.Close();
        }
        private void changePhoto_Click(object sender, RoutedEventArgs e)
        {
            string dirq = Directory.GetCurrentDirectory();
            string dir = Directory.GetCurrentDirectory();
            dir = dir + @"\Images\profilpictures\";
            int i = 0;
            string path;
            FileDialog FileDialog = new OpenFileDialog();   /* dosya konumunu alma */
            FileDialog.Filter = "Image files (*.jpg)|*.jpg";
            FileDialog.ShowDialog();
            path = FileDialog.FileName;
            string onlyFileName = System.IO.Path.GetFileName(FileDialog.FileName);
            while (File.Exists(dir + i + onlyFileName))
            {
                i++;
            }
            onlyFileName = i + onlyFileName;
            if (path != "" && dir != "" && onlyFileName != "")  
            {
                if (!File.Exists(dir + onlyFileName))
                {
                    File.Copy(path, dir + onlyFileName);
                }
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE userinfo SET photopath=@photopath WHERE username=@loginusname;";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;
            ActiveConnection.Open();
            cmd.Parameters.AddWithValue("@loginusname", LoginUserName);
            cmd.Parameters.AddWithValue("@photopath", onlyFileName);
            cmd.ExecuteNonQuery();
            ActiveConnection.Close();
            //////---------------------

            if (upphoto.Source != null)
            {
                WebClient wc = new WebClient();
                wc.Credentials = new System.Net.NetworkCredential("ackuasil", "Bx84qgP5g9");
                wc.UploadFile("ftp://ackuasil.cloudaccess.host/httpdocs/Images/profilpictures/" + onlyFileName + "", path);
            }
            MessageBox.Show("Your Photo Has Been Updated..!","Congratulations..!",MessageBoxButton.OK,MessageBoxImage.Information);
        }
    }
}