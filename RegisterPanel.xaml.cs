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
using System.Text.RegularExpressions;
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
    /// Interaction logic for RegisterPanel.xaml
    /// </summary>
    public partial class RegisterPanel : UserControl
    {
        public string dir;
        SqlConnection ActiveConnection = new SqlConnection("Server=79.123.228.28,5501;Database=Gunduz_HAcku;User Id=hacku;Password=x7a45tt55;");
        public string path;
        public string npath;
        public string onlyFileNames;
        public string onlyFileNameL;
        public RegisterPanel()
        {
            InitializeComponent();
            dir = Directory.GetCurrentDirectory();
            dir = dir + @"\Images\profilpictures\";
        }

        private void RLPanel_Click(object sender, RoutedEventArgs e)
        {
            LoginPanel LPanel = new LoginPanel();
        }
        private void OnlyNumeric(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmdUN = new SqlCommand("Select id from userinfo where username= @username", ActiveConnection);
            cmdUN.Parameters.AddWithValue("@username", this.uusername.Text);
            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }

            var nameid = cmdUN.ExecuteScalar();

            if (nameid != null)
            {
                MessageBox.Show("This Username Already Has Taken.. Please Select Another One..", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (upassword.Password == upassworda.Password && uname.Text != "" && usurname.Text != "" && ucitizenship.Text != "" && uusername.Text != "" && ulocation.Text != "" && utype.Text != "" && uphone.Text != "" && umail.Text != "" && ugender.Text != "" && uage.Text != "" && uphotopath.Source != null)
            {
                int i = 0;
                string textz = ((ComboBoxItem)ugender.SelectedItem).Content.ToString();
                string deger;
                deger = MD5Sifrele(upassword.Password);
                SqlCommand cmd = new SqlCommand("INSERT INTO userinfo (name,surname,citizenshipnumber,username,password,location,phone,mail,gender,age,photopath,usertype,usertypeid) VALUES " +
                    "(@uname,@usurname,@ucitizenship,@uusername,@upassword,@ulocation,@uphone,@umail,@ugender,@uage,@uphotopath,@utype,@utypeid)", ActiveConnection);
                if (ActiveConnection.State == ConnectionState.Closed)
                {
                    ActiveConnection.Open();
                }
                cmd.Parameters.AddWithValue("@uname", uname.Text);
                cmd.Parameters.AddWithValue("@usurname", usurname.Text);
                cmd.Parameters.AddWithValue("@ucitizenship", ucitizenship.Text);
                cmd.Parameters.AddWithValue("@uusername", uusername.Text);
                cmd.Parameters.AddWithValue("@upassword", deger);
                cmd.Parameters.AddWithValue("@ulocation", ulocation.Text);
                cmd.Parameters.AddWithValue("@uphone", uphone.Text);
                cmd.Parameters.AddWithValue("@umail", umail.Text);
                cmd.Parameters.AddWithValue("@ugender", ugender.Text);
                cmd.Parameters.AddWithValue("@uage", uage.Text);
                cmd.Parameters.AddWithValue("@uphotopath", onlyFileNameL);
                cmd.Parameters.AddWithValue("@utype", utype.Text);
                cmd.Parameters.AddWithValue("@utypeid", utypeid.Text);
                cmd.ExecuteNonQuery();
                ActiveConnection.Close();
                ActiveConnection.Dispose();
                WebClient wc = new WebClient();
                wc.Credentials = new System.Net.NetworkCredential("ackuasil", "Bx84qgP5g9");
                wc.UploadFile("ftp://ackuasil.cloudaccess.host/httpdocs/Images/profilpictures/" + onlyFileNameL + "", path);
                uname.Text = "";
                usurname.Text = "";
                ucitizenship.Text = "";
                uusername.Text = "";
                upassword.Password = null;
                upassworda.Password = null;
                ulocation.Text = "";
                uphone.Text = "";
                umail.Text = "";
                ugender.SelectedIndex = -1;
                uage.Text = "";
                uphotopath.Source = null;
                MessageBox.Show("Congrulations Welcome Our Family...", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("You Need To Fill All Fieds ...", "Error..!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string dir = Directory.GetCurrentDirectory();
            dir = dir + @"\Images\profilpictures\";
            int i = 0;
            FileDialog FileDialog = new OpenFileDialog();   /* dosya konumunu alma */
            FileDialog.Filter = "Image files (*.jpg)|*.jpg";
            FileDialog.ShowDialog();
            path = FileDialog.FileName;
            onlyFileNameL = System.IO.Path.GetFileName(FileDialog.FileName);
            while (File.Exists(dir + i + onlyFileNameL))
            {
                i++;
            }
            onlyFileNameL = i + onlyFileNameL;
            if (path != "" && dir != "" && onlyFileNameL != "")
            {
                if (!File.Exists(dir + onlyFileNameL))
                {
                    File.Copy(path, dir + onlyFileNameL);
                }
            }
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(dir + onlyFileNameL);
            logo.EndInit();
            uphotopath.Source = logo;
            MessageBox.Show(onlyFileNameL);
        }

        private void uusername_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SqlCommand cmdUN = new SqlCommand("Select id from userinfo where username= @username", ActiveConnection);
            cmdUN.Parameters.AddWithValue("@username", this.uusername.Text);
            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }

            var nameid = cmdUN.ExecuteScalar();

            if (nameid != null)
            {
                MessageBox.Show("This Username Already Has Taken.. Please Select Another One..", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                uusername.Text = "";
            }
        }
    }
}