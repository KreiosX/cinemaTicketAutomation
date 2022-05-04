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
using static AsilCinemaTicketAutomation.Operations;

namespace AsilCinemaTicketAutomation
{
    /// <summary>
    /// Interaction logic for EnterTheVision.xaml
    /// </summary>
    public partial class EnterTheVision : UserControl
    {
        public static int i = 1;
        public string dir;
        SqlConnection ActiveConnection = new SqlConnection("Server=79.123.228.28,5501;Database=Gunduz_HAcku;User Id=hacku;Password=x7a45tt55;");
        public EnterTheVision()
        {
            InitializeComponent();
            dir = Directory.GetCurrentDirectory();
            dir = dir + @"\Images\enterthevisionphotos\";
            DisplayData();
            List();
            ControlPanel();
            if (UserType == "Guest")
            {
                st2.Visibility = Visibility.Collapsed;
                st3.Visibility = Visibility.Collapsed;
            }
        }
        public void ControlPanel()
        {
            if (Perms(UserLoginId, 7) == true)
            {
                st2.Visibility = Visibility.Visible;
                st3.Visibility = Visibility.Visible;
            }
            else
            {
                st2.Visibility = Visibility.Collapsed;
                st3.Visibility = Visibility.Collapsed;
            }
            if (Perm(1, 1) == true)
            {
                m1.Visibility = Visibility.Visible;
            }
            else
            {
                m1.Visibility = Visibility.Collapsed;
            }
            if (Perm(2, 1) == true)
            {
                m2.Visibility = Visibility.Visible;
            }
            else
            {
                m2.Visibility = Visibility.Collapsed;
            }
            if (Perm(3, 1) == true)
            {
                m3.Visibility = Visibility.Visible;
            }
            else
            {
                m3.Visibility = Visibility.Collapsed;
            }
            if (Perm(4, 1) == true)
            {
                m4.Visibility = Visibility.Visible;
            }
            else
            {
                m4.Visibility = Visibility.Collapsed;
            }
            if (Perm(5, 1) == true)
            {
                m5.Visibility = Visibility.Visible;
            }
            else
            {
                m5.Visibility = Visibility.Collapsed;
            }
            if (Perm(6, 1) == true)
            {
                m6.Visibility = Visibility.Visible;
            }
            else
            {
                m6.Visibility = Visibility.Collapsed;
            }
            if (Perm(7, 1) == true)
            {
                m7.Visibility = Visibility.Visible;
            }
            else
            {
                m7.Visibility = Visibility.Collapsed;
            }
            if (Perm(8, 1) == true)
            {
                m8.Visibility = Visibility.Visible;
            }
            else
            {
                m8.Visibility = Visibility.Collapsed;
            }
            if (Perm(9, 1) == true)
            {
                m9.Visibility = Visibility.Visible;
            }
            else
            {
                m9.Visibility = Visibility.Collapsed;
            }
            if (Perm(10, 1) == true)
            {
                m10.Visibility = Visibility.Visible;
            }
            else
            {
                m10.Visibility = Visibility.Collapsed;
            }
            if (Perm(11, 1) == true)
            {
                m11.Visibility = Visibility.Visible;
            }
            else
            {
                m11.Visibility = Visibility.Collapsed;
            }
            if (Perm(12, 1) == true)
            {
                m12.Visibility = Visibility.Visible;
            }
            else
            {
                m12.Visibility = Visibility.Collapsed;
            }
        }

        public bool Perm(int i, int ii)
        {
            bool val = false;
            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }
            SqlCommand SqlCom = new SqlCommand("SELECT COUNT(id) FROM ETVisionPanelVis where id=@id and etvstate=@etvstate", ActiveConnection);
            SqlCom.Parameters.AddWithValue("@id", i);
            SqlCom.Parameters.AddWithValue("@etvstate", ii);
            SqlDataReader DR = SqlCom.ExecuteReader();
            if (DR.Read())
            {
                if ((int)DR[0] == 0)
                {
                    val = false;
                }
                else
                {
                    val = true;
                }
            }
            DR.Close();
            SqlCom.Dispose();
            ActiveConnection.Close();
            return val;
        }

        public bool Perms(int usid, int authid)
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

        void List()
        {
            if (ActiveConnection.State != ConnectionState.Open)
            {
                ActiveConnection.Open();
            }
            SqlCommand SqlCom = new SqlCommand("SELECT id,etvstate FROM ETVisionPanelVis WHERE etvstate=1", ActiveConnection);
            SqlDataReader SqlDataR = SqlCom.ExecuteReader();
            List<string> MyList = new List<string>();
            while (SqlDataR.Read())
            {
                MyList.Add(SqlDataR[0].ToString());
            }
            foreach (CheckBox AuthorizationCB in st2.Children)
            {
                string ara = AuthorizationCB.Tag.ToString();
                bool var = false;
                foreach (string item1 in MyList)
                {
                    if (ara == item1) var = true;
                }
                if (var == false)
                {
                    AuthorizationCB.IsChecked = false;
                }
                else
                {
                    AuthorizationCB.IsChecked = true;
                }
            }
            SqlDataR.Close();
        }
        void DisplayData()
        {
            string dirz = Directory.GetCurrentDirectory();
            dirz += @"\Images\needupdate\needupdateetv.jpg";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT etvisionphotopath FROM EnterTheVision where id=1";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            SqlDataReader dr;
            ActiveConnection.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string mv1;
                mv1 = (dr["etvisionphotopath"].ToString());
                ImageBrush imgBrush = new ImageBrush();
                imgBrush.ImageSource = new BitmapImage(new Uri(dir + mv1, UriKind.Relative));
                if (!File.Exists(dir + mv1))
                {
                    imgBrush.ImageSource = new BitmapImage(new Uri(dirz, UriKind.Relative));
                    m1.Fill = imgBrush;
                    dr.Close();
                }
                else
                {
                    m1.Fill = imgBrush;
                    dr.Close();
                }
            }

            cmd.CommandText = "SELECT etvisionphotopath FROM EnterTheVision where id=2";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string mv2;
                mv2 = (dr["etvisionphotopath"].ToString());
                ImageBrush imgBrush = new ImageBrush();
                imgBrush.ImageSource = new BitmapImage(new Uri(dir + mv2, UriKind.Relative));
                if (!File.Exists(dir + mv2))
                {
                    imgBrush.ImageSource = new BitmapImage(new Uri(dirz, UriKind.Relative));
                    m2.Fill = imgBrush;
                    dr.Close();
                }
                else
                {
                    m2.Fill = imgBrush;
                    dr.Close();
                }
            }

            cmd.CommandText = "SELECT etvisionphotopath FROM EnterTheVision where id=3";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string mv3;
                mv3 = (dr["etvisionphotopath"].ToString());
                ImageBrush imgBrush = new ImageBrush();
                imgBrush.ImageSource = new BitmapImage(new Uri(dir + mv3, UriKind.Relative));
                if (!File.Exists(dir + mv3))
                {
                    imgBrush.ImageSource = new BitmapImage(new Uri(dirz, UriKind.Relative));
                    m3.Fill = imgBrush;
                    dr.Close();
                }
                else
                {
                    m3.Fill = imgBrush;
                    dr.Close();
                }
            }

            cmd.CommandText = "SELECT etvisionphotopath FROM EnterTheVision where id=4";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string mv4;
                mv4 = (dr["etvisionphotopath"].ToString());
                ImageBrush imgBrush = new ImageBrush();
                imgBrush.ImageSource = new BitmapImage(new Uri(dir + mv4, UriKind.Relative));
                if (!File.Exists(dir + mv4))
                {
                    imgBrush.ImageSource = new BitmapImage(new Uri(dirz, UriKind.Relative));
                    m4.Fill = imgBrush;
                    dr.Close();
                }
                else
                {
                    m4.Fill = imgBrush;
                    dr.Close();
                }
            }

            cmd.CommandText = "SELECT etvisionphotopath FROM EnterTheVision where id=5";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string mv5;
                mv5 = (dr["etvisionphotopath"].ToString());
                ImageBrush imgBrush = new ImageBrush();
                imgBrush.ImageSource = new BitmapImage(new Uri(dir + mv5, UriKind.Relative));
                if (!File.Exists(dir + mv5))
                {
                    imgBrush.ImageSource = new BitmapImage(new Uri(dirz, UriKind.Relative));
                    m5.Fill = imgBrush;
                    dr.Close();
                }
                else
                {
                    m5.Fill = imgBrush;
                    dr.Close();
                }
            }

            cmd.CommandText = "SELECT etvisionphotopath FROM EnterTheVision where id=6";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string mv6;
                mv6 = (dr["etvisionphotopath"].ToString());
                ImageBrush imgBrush = new ImageBrush();
                imgBrush.ImageSource = new BitmapImage(new Uri(dir + mv6, UriKind.Relative));
                if (!File.Exists(dir + mv6))
                {
                    imgBrush.ImageSource = new BitmapImage(new Uri(dirz, UriKind.Relative));
                    m6.Fill = imgBrush;
                    dr.Close();
                }
                else
                {
                    m6.Fill = imgBrush;
                    dr.Close();
                }
            }

            cmd.CommandText = "SELECT etvisionphotopath FROM EnterTheVision where id=7";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string mv7;
                mv7 = (dr["etvisionphotopath"].ToString());
                ImageBrush imgBrush = new ImageBrush();
                imgBrush.ImageSource = new BitmapImage(new Uri(dir + mv7, UriKind.Relative));
                if (!File.Exists(dir + mv7))
                {
                    imgBrush.ImageSource = new BitmapImage(new Uri(dirz, UriKind.Relative));
                    m7.Fill = imgBrush;
                    dr.Close();
                }
                else
                {
                    m7.Fill = imgBrush;
                    dr.Close();
                }
            }

            cmd.CommandText = "SELECT etvisionphotopath FROM EnterTheVision where id=8";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string mv8;
                mv8 = (dr["etvisionphotopath"].ToString());
                ImageBrush imgBrush = new ImageBrush();
                imgBrush.ImageSource = new BitmapImage(new Uri(dir + mv8, UriKind.Relative));
                if (!File.Exists(dir + mv8))
                {
                    imgBrush.ImageSource = new BitmapImage(new Uri(dirz, UriKind.Relative));
                    m8.Fill = imgBrush;
                    dr.Close();
                }
                else
                {
                    m8.Fill = imgBrush;
                    dr.Close();
                }
            }

            cmd.CommandText = "SELECT etvisionphotopath FROM EnterTheVision where id=9";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string mv9;
                mv9 = (dr["etvisionphotopath"].ToString());
                ImageBrush imgBrush = new ImageBrush();
                imgBrush.ImageSource = new BitmapImage(new Uri(dir + mv9, UriKind.Relative));
                if (!File.Exists(dir + mv9))
                {
                    imgBrush.ImageSource = new BitmapImage(new Uri(dirz, UriKind.Relative));
                    m9.Fill = imgBrush;
                    dr.Close();
                }
                else
                {
                    m9.Fill = imgBrush;
                    dr.Close();
                }
            }

            cmd.CommandText = "SELECT etvisionphotopath FROM EnterTheVision where id=10";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string mv10;
                mv10 = (dr["etvisionphotopath"].ToString());
                ImageBrush imgBrush = new ImageBrush();
                imgBrush.ImageSource = new BitmapImage(new Uri(dir + mv10, UriKind.Relative));
                if (!File.Exists(dir + mv10))
                {
                    imgBrush.ImageSource = new BitmapImage(new Uri(dirz, UriKind.Relative));
                    m10.Fill = imgBrush;
                    dr.Close();
                }
                else
                {
                    m10.Fill = imgBrush;
                    dr.Close();
                }
            }

            cmd.CommandText = "SELECT etvisionphotopath FROM EnterTheVision where id=11";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string mv11;
                mv11 = (dr["etvisionphotopath"].ToString());
                ImageBrush imgBrush = new ImageBrush();
                imgBrush.ImageSource = new BitmapImage(new Uri(dir + mv11, UriKind.Relative));
                if (!File.Exists(dir + mv11))
                {
                    imgBrush.ImageSource = new BitmapImage(new Uri(dirz, UriKind.Relative));
                    m11.Fill = imgBrush;
                    dr.Close();
                }
                else
                {
                    m11.Fill = imgBrush;
                    dr.Close();
                }
            }

            cmd.CommandText = "SELECT etvisionphotopath FROM EnterTheVision where id=12";
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string mv12;
                mv12 = (dr["etvisionphotopath"].ToString());
                ImageBrush imgBrush = new ImageBrush();
                imgBrush.ImageSource = new BitmapImage(new Uri(dir + mv12, UriKind.Relative));
                if (!File.Exists(dir + mv12))
                {
                    imgBrush.ImageSource = new BitmapImage(new Uri(dirz, UriKind.Relative));
                    m12.Fill = imgBrush;
                    dr.Close();
                }
                else
                {
                    m12.Fill = imgBrush;
                    dr.Close();
                }
            }
        }

        private void SETVVisiblity_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }
            if (e1.IsChecked == false)
            {
                SqlCommand comm = new SqlCommand("DELETE FROM ETVisionPanelVis WHERE id=@e1", ActiveConnection);
                comm.Parameters.AddWithValue("@e1",e1.Tag);
                comm.ExecuteNonQuery();
                m1.Visibility = Visibility.Collapsed;
            }
            else if (e1.IsChecked == true)
            {
                SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT id,etvstate FROM ETVisionPanelVis where id=@id and etvstate=@etvstate) INSERT INTO ETVisionPanelVis (id,etvstate) VALUES (@id,@etvstate)", ActiveConnection);
                cmd.Parameters.AddWithValue("@id", e1.Tag);
                cmd.Parameters.AddWithValue("@etvstate", i);
                cmd.ExecuteNonQuery();
                m1.Visibility = Visibility.Visible;
            }
            else
            {

            }
            if (e2.IsChecked == false)
            {
                SqlCommand comm = new SqlCommand("DELETE FROM ETVisionPanelVis WHERE id=@e2", ActiveConnection);
                comm.Parameters.AddWithValue("@e2", e2.Tag);
                comm.ExecuteNonQuery();
                m2.Visibility = Visibility.Collapsed;
            }
            else if (e2.IsChecked == true)
            {
                SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM ETVisionPanelVis where id=@id and etvstate=@etvstate) INSERT INTO ETVisionPanelVis (id,etvstate) VALUES (@id,@etvstate)", ActiveConnection);
                cmd.Parameters.AddWithValue("@id", e2.Tag);
                cmd.Parameters.AddWithValue("@etvstate", i);
                cmd.ExecuteNonQuery();
                m2.Visibility = Visibility.Visible;
            }
            else
            {

            }
            if (e3.IsChecked == false)
            {
                SqlCommand comm = new SqlCommand("DELETE FROM ETVisionPanelVis WHERE id=@e3", ActiveConnection);
                comm.Parameters.AddWithValue("@e3", e3.Tag);
                comm.ExecuteNonQuery();
                m3.Visibility = Visibility.Collapsed;
            }
            else if (e3.IsChecked == true)
            {
                SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM ETVisionPanelVis where id=@id and etvstate=@etvstate) INSERT INTO ETVisionPanelVis (id,etvstate) VALUES (@id,@etvstate)", ActiveConnection);
                cmd.Parameters.AddWithValue("@id", e3.Tag);
                cmd.Parameters.AddWithValue("@etvstate", i);
                cmd.ExecuteNonQuery();
                m3.Visibility = Visibility.Visible;
            }
            else
            {

            }
            if (e4.IsChecked == false)
            {
                SqlCommand comm = new SqlCommand("DELETE FROM ETVisionPanelVis WHERE id=@e4", ActiveConnection);
                comm.Parameters.AddWithValue("@e4", e4.Tag);
                comm.ExecuteNonQuery();
                m4.Visibility = Visibility.Collapsed;
            }
            else if (e4.IsChecked == true)
            {
                SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM ETVisionPanelVis where id=@id and etvstate=@etvstate) INSERT INTO ETVisionPanelVis (id,etvstate) VALUES (@id,@etvstate)", ActiveConnection);
                cmd.Parameters.AddWithValue("@id", e4.Tag);
                cmd.Parameters.AddWithValue("@etvstate", i);
                cmd.ExecuteNonQuery();
                m4.Visibility = Visibility.Visible;
            }
            else
            {

            }
            if (e5.IsChecked == false)
            {
                SqlCommand comm = new SqlCommand("DELETE FROM ETVisionPanelVis WHERE id=@e5", ActiveConnection);
                comm.Parameters.AddWithValue("@e5", e5.Tag);
                comm.ExecuteNonQuery();
                m5.Visibility = Visibility.Collapsed;
            }
            else if (e5.IsChecked == true)
            {
                SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM ETVisionPanelVis where id=@id and etvstate=@etvstate) INSERT INTO ETVisionPanelVis (id,etvstate) VALUES (@id,@etvstate)", ActiveConnection);
                cmd.Parameters.AddWithValue("@id", e5.Tag);
                cmd.Parameters.AddWithValue("@etvstate", i);
                cmd.ExecuteNonQuery();
                m5.Visibility = Visibility.Visible;
            }
            else
            {

            }
            if (e6.IsChecked == false)
            {
                SqlCommand comm = new SqlCommand("DELETE FROM ETVisionPanelVis WHERE id=@e6", ActiveConnection);
                comm.Parameters.AddWithValue("@e6", e6.Tag);
                comm.ExecuteNonQuery();
                m6.Visibility = Visibility.Collapsed;
            }
            else if (e6.IsChecked == true)
            {
                SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM ETVisionPanelVis where id=@id and etvstate=@etvstate) INSERT INTO ETVisionPanelVis (id,etvstate) VALUES (@id,@etvstate)", ActiveConnection);
                cmd.Parameters.AddWithValue("@id", e6.Tag);
                cmd.Parameters.AddWithValue("@etvstate", i);
                cmd.ExecuteNonQuery();
                m6.Visibility = Visibility.Visible;
            }
            else
            {

            }
            if (e7.IsChecked == false)
            {
                SqlCommand comm = new SqlCommand("DELETE FROM ETVisionPanelVis WHERE id=@e7", ActiveConnection);
                comm.Parameters.AddWithValue("@e7", e7.Tag);
                comm.ExecuteNonQuery();
                m7.Visibility = Visibility.Collapsed;
            }
            else if (e7.IsChecked == true)
            {
                SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM ETVisionPanelVis where id=@id and etvstate=@etvstate) INSERT INTO ETVisionPanelVis (id,etvstate) VALUES (@id,@etvstate)", ActiveConnection);
                cmd.Parameters.AddWithValue("@id", e7.Tag);
                cmd.Parameters.AddWithValue("@etvstate", i);
                cmd.ExecuteNonQuery();
                m7.Visibility = Visibility.Visible;
            }
            else
            {

            }
            if (e8.IsChecked == false)
            {
                SqlCommand comm = new SqlCommand("DELETE FROM ETVisionPanelVis WHERE id=@e8", ActiveConnection);
                comm.Parameters.AddWithValue("@e8", e8.Tag);
                comm.ExecuteNonQuery();
                m8.Visibility = Visibility.Collapsed;
            }
            else if (e8.IsChecked == true)
            {
                SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM ETVisionPanelVis where id=@id and etvstate=@etvstate) INSERT INTO ETVisionPanelVis (id,etvstate) VALUES (@id,@etvstate)", ActiveConnection);
                cmd.Parameters.AddWithValue("@id", e8.Tag);
                cmd.Parameters.AddWithValue("@etvstate", i);
                cmd.ExecuteNonQuery();
                m8.Visibility = Visibility.Visible;
            }
            else
            {

            }
            if (e9.IsChecked == false)
            {
                SqlCommand comm = new SqlCommand("DELETE FROM ETVisionPanelVis WHERE id=@e9", ActiveConnection);
                comm.Parameters.AddWithValue("@e9", e9.Tag);
                comm.ExecuteNonQuery();
                m9.Visibility = Visibility.Collapsed;
            }
            else if (e9.IsChecked == true)
            {
                SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM ETVisionPanelVis where id=@id and etvstate=@etvstate) INSERT INTO ETVisionPanelVis (id,etvstate) VALUES (@id,@etvstate)", ActiveConnection);
                cmd.Parameters.AddWithValue("@id", e9.Tag);
                cmd.Parameters.AddWithValue("@etvstate", i);
                cmd.ExecuteNonQuery();
                m9.Visibility = Visibility.Visible;
            }
            else
            {

            }
            if (e10.IsChecked == false)
            {
                SqlCommand comm = new SqlCommand("DELETE FROM ETVisionPanelVis WHERE id=@e10", ActiveConnection);
                comm.Parameters.AddWithValue("@e10", e10.Tag);
                comm.ExecuteNonQuery();
                m10.Visibility = Visibility.Collapsed;
            }
            else if (e10.IsChecked == true)
            {
                SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM ETVisionPanelVis where id=@id and etvstate=@etvstate) INSERT INTO ETVisionPanelVis (id,etvstate) VALUES (@id,@etvstate)", ActiveConnection);
                cmd.Parameters.AddWithValue("@id", e10.Tag);
                cmd.Parameters.AddWithValue("@etvstate", i);
                cmd.ExecuteNonQuery();
                m10.Visibility = Visibility.Visible;
            }
            else
            {

            }
            if (e11.IsChecked == false)
            {
                SqlCommand comm = new SqlCommand("DELETE FROM ETVisionPanelVis WHERE id=@e11", ActiveConnection);
                comm.Parameters.AddWithValue("@e11", e11.Tag);
                comm.ExecuteNonQuery();
                m11.Visibility = Visibility.Collapsed;
            }
            else if (e11.IsChecked == true)
            {
                SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM ETVisionPanelVis where id=@id and etvstate=@etvstate) INSERT INTO ETVisionPanelVis (id,etvstate) VALUES (@id,@etvstate)", ActiveConnection);
                cmd.Parameters.AddWithValue("@id", e11.Content);
                cmd.Parameters.AddWithValue("@etvstate", i);
                cmd.ExecuteNonQuery();
                m11.Visibility = Visibility.Visible;
            }
            else
            {

            }
            if (e12.IsChecked == false)
            {
                SqlCommand comm = new SqlCommand("DELETE FROM ETVisionPanelVis WHERE id=@e12", ActiveConnection);
                comm.Parameters.AddWithValue("@e12", e12.Tag);
                comm.ExecuteNonQuery();
                m12.Visibility = Visibility.Collapsed;
            }
            else if (e12.IsChecked == true)
            {
                SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM ETVisionPanelVis where id=@id and etvstate=@etvstate) INSERT INTO ETVisionPanelVis (id,etvstate) VALUES (@id,@etvstate)", ActiveConnection);
                cmd.Parameters.AddWithValue("@id", e12.Content);
                cmd.Parameters.AddWithValue("@etvstate", i);
                cmd.ExecuteNonQuery();
                m12.Visibility = Visibility.Visible;
            }
            else
            {

            }
        }
    }
}