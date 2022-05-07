using Microsoft.Win32;
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
using System.Net;
using static AsilCinemaTicketAutomation.Operations;

namespace AsilCinemaTicketAutomation
{
    /// <summary>
    /// Interaction logic for FilmTransactions.xaml
    /// </summary>
    public partial class FilmTransactions : UserControl
    {
        string path;
        public string pathv;
        public string pathi;
        public string dir1;
        public string dirc;
        int ct = 0;
        SqlConnection ActiveConnection = new SqlConnection("----");
        public FilmTransactions()
        {
            InitializeComponent();
            dir1 = Directory.GetCurrentDirectory();
            dirc = Directory.GetCurrentDirectory();
            dir1 = dir1 + @"\Images\moviebanners\";
            getsesdate.BlackoutDates.AddDatesInPast();
            getsesdate.SelectedDate = DateTime.Today;
            sdate.BlackoutDates.AddDatesInPast();
            sdate.SelectedDate = DateTime.Today;
            BindComboBox(ETVisions);
            BindComboBoxFra(fra);
            UpdateSaloon();
            Controlz();
            UpdateMT();
            SSalonForSession();
            UpdateSaloonForSession();

        }

        public void Controlz()
        {
            ev.BlackoutDates.AddDatesInPast();
            ev.SelectedDate = DateTime.Today;
            ov.BlackoutDates.AddDatesInPast();
            if (Perm(UserLoginId, 8) == true)
            {
                insfilm.Visibility = Visibility.Visible;
            }
            else
            {
                insfilm.Visibility = Visibility.Collapsed;
            }
            if (Perm(UserLoginId, 9) == true)
            {
                inssession.Visibility = Visibility.Visible;
                updordelsession.Visibility = Visibility.Visible;
            }
            else
            {
                inssession.Visibility = Visibility.Collapsed;
                updordelsession.Visibility = Visibility.Collapsed;
            }
            if (Perm(UserLoginId, 10) == true)
            {
                inssaloon.Visibility = Visibility.Visible;
                updordelsaloon.Visibility = Visibility.Visible;
            }
            else
            {
                inssaloon.Visibility = Visibility.Collapsed;
                updordelsaloon.Visibility = Visibility.Collapsed;
            }
            if (Perm(UserLoginId, 11) == true)
            {
                updetvision.Visibility = Visibility.Visible;
            }
            else
            {
                updetvision.Visibility = Visibility.Collapsed;
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

        public void BindComboBox(ComboBox ETVisions)
        {
            SqlConnection ActiveConnection = new SqlConnection("---");
            SqlDataAdapter da = new SqlDataAdapter("Select id,etvisionphotopath FROM EnterTheVision", ActiveConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "EnterTheVision");
            ETVisions.ItemsSource = ds.Tables[0].DefaultView;
            ETVisions.DisplayMemberPath = ds.Tables[0].Columns["id"].ToString();
            ETVisions.SelectedValuePath = ds.Tables[0].Columns["id"].ToString();
        }
        public void BindComboBoxFra(ComboBox fra)
        {
            SqlConnection ActiveConnection = new SqlConnection("----");
            SqlDataAdapter da = new SqlDataAdapter("Select * FROM mainFrag", ActiveConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "mainFrag");
            fra.ItemsSource = ds.Tables[0].DefaultView;
            fra.DisplayMemberPath = ds.Tables[0].Columns["id"].ToString();
            fra.SelectedValuePath = ds.Tables[0].Columns["fragpath"].ToString();
        }
        private void ETVisions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string dir = Directory.GetCurrentDirectory();
            dir = dir + @"\Images\enterthevisionphotos\";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT etvisionphotopath FROM EnterTheVision where id=@etvsv";
            cmd.Parameters.AddWithValue("@etvsv", ETVisions.SelectedValue);
            cmd.Connection = ActiveConnection;
            cmd.CommandType = CommandType.Text;

            SqlDataReader dr;
            ActiveConnection.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string ppathz;
                ppathz = (dr["etvisionphotopath"].ToString());
                BitmapImage logoA = new BitmapImage();
                logoA.BeginInit();
                logoA.UriSource = new Uri(dir + ppathz);
                logoA.EndInit();
                upphoto.Source = logoA;
            }
            ActiveConnection.Close();
        }
        private void fragup_Click(object sender, RoutedEventArgs e)
        {
            string dir = Directory.GetCurrentDirectory();
            dir = dir + @"\Videos\homepagefragmans\";
            int i = 0;
            FileDialog FileDialog = new OpenFileDialog();   /* dosya konumunu alma */
            FileDialog.Filter = "Video Files (*.mp4)|*.mp4";
            FileDialog.ShowDialog();
            pathv = FileDialog.FileName;
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
            if (myMediaElement.Source != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE mainFrag SET fragpath = @fragpath WHERE id='" + fra.Text + "'";
                cmd.Connection = ActiveConnection;
                cmd.CommandType = CommandType.Text;
                ActiveConnection.Open();
                cmd.Parameters.AddWithValue("@fragpath", onlyFileName);
                cmd.ExecuteNonQuery();
                ActiveConnection.Close();
                myMediaElement.Stop();
                myMediaElement.Source = new Uri(dir + onlyFileName, UriKind.RelativeOrAbsolute);
                myMediaElement.Play();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string dir = Directory.GetCurrentDirectory();
            dir = dir + @"\Images\enterthevisionphotos\";
            int i = 0;
            FileDialog FileDialog = new OpenFileDialog();   /* dosya konumunu alma */
            FileDialog.Filter = "Image files (*.jpg)|*.jpg";
            FileDialog.ShowDialog();
            pathi = FileDialog.FileName;
            string onlyFileName = System.IO.Path.GetFileName(FileDialog.FileName);
            while (File.Exists(dir + i + onlyFileName))
            {
                i++;
            }
            onlyFileName = i + onlyFileName;
            if (pathi != "" && dir != "" && onlyFileName != "")
            {
                if (!File.Exists(dir + onlyFileName))
                {
                    File.Copy(pathi, dir + onlyFileName);
                }
            }
            if (upphoto.Source != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE EnterTheVision SET etvisionphotopath = @etvisionphotopath WHERE id=@etvsv";
                cmd.Parameters.AddWithValue("@etvsv",ETVisions.SelectedValue);
                cmd.Connection = ActiveConnection;
                cmd.CommandType = CommandType.Text;
                ActiveConnection.Open();
                cmd.Parameters.AddWithValue("@etvisionphotopath", onlyFileName);
                cmd.ExecuteNonQuery();
                ActiveConnection.Close();
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri(dir + onlyFileName);
                logo.EndInit();
                upphoto.Source = logo;
            }
        }

        private void UpdateSaloon()
        {
            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }
            SqlDataAdapter SqlDA = new SqlDataAdapter("SELECT * FROM saloonz", ActiveConnection);
            DataSet ds = new DataSet();
            SqlDA.Fill(ds, "saloonz");
            SSaloon.DataContext = ds.Tables["saloonz"].DefaultView;
            SSaloon.DisplayMemberPath = ds.Tables["saloonz"].Columns["salonnumber"].ToString();
            SSaloon.SelectedValuePath = ds.Tables["saloonz"].Columns["seatcapacity"].ToString();
            ActiveConnection.Close();
        }

        private void UpdateSaloonForSession()
        {
            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }
            SqlDataAdapter SqlDA = new SqlDataAdapter("SELECT * FROM saloonz", ActiveConnection);
            DataSet ds = new DataSet();
            SqlDA.Fill(ds, "saloonz");
            SSal.DataContext = ds.Tables["saloonz"].DefaultView;
            SSal.DisplayMemberPath = ds.Tables["saloonz"].Columns["salonnumber"].ToString();
            SSal.SelectedValuePath = ds.Tables["saloonz"].Columns["seatcapacity"].ToString();
            ActiveConnection.Close();
        }
        private void UpdateMT()
        {
            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }
            SqlDataAdapter SqlDA = new SqlDataAdapter("SELECT * FROM movtypez", ActiveConnection);
            DataSet ds = new DataSet();
            SqlDA.Fill(ds, "movtypez");
            smt.DataContext = ds.Tables["movtypez"].DefaultView;
            smt.DisplayMemberPath = ds.Tables["movtypez"].Columns["typename"].ToString();
            smt.SelectedValuePath = ds.Tables["movtypez"].Columns["typeid"].ToString();
            ActiveConnection.Close();
        }
        private void CSession_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("if not exists (select sessiontime from sessionz where sessiontime=@estime and sessiondate=@gsesdate and salonnumber=@ss) INSERT INTO  sessionz (salonnumber,sessiontime,sessiondate,onsesfilmname) VALUES (@salonnumber,@sessiontime,@sessiondate,@onsesfilmname)", ActiveConnection);
            cmd.Parameters.AddWithValue("@estime",ESTime.Text);
            cmd.Parameters.AddWithValue("@gsesdate",getsesdate.Text);
            cmd.Parameters.AddWithValue("@ss",ss.Text);
            cmd.Parameters.AddWithValue("@salonnumber", ss.Text);
            cmd.Parameters.AddWithValue("@sessiontime", ESTime.Text);
            cmd.Parameters.AddWithValue("@sessiondate", getsesdate.Text);
            cmd.Parameters.AddWithValue("@onsesfilmname", ssfilm.Text);
            ActiveConnection.Open();
            cmd.ExecuteNonQuery();
            ActiveConnection.Close();
        }
        void CSaloon_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = ActiveConnection.CreateCommand();
            sqlComm.CommandText = @"INSERT INTO saloonz (salonnumber,seatcapacity) VALUES (@salonnumber,@seatcapacity)";
            sqlComm.Parameters.AddWithValue("@salonnumber", Convert.ToInt16(isaloon.Text));
            sqlComm.Parameters.AddWithValue("@seatcapacity", Convert.ToInt16(iseat.Text));
            ActiveConnection.Open();
            sqlComm.ExecuteNonQuery();
            ActiveConnection.Close();
            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }
            SqlDataAdapter SqlDA = new SqlDataAdapter("SELECT * FROM saloonz", ActiveConnection);
            DataSet ds = new DataSet();
            SqlDA.Fill(ds, "saloonz");
            ss.DataContext = ds.Tables["saloonz"].DefaultView;
            ss.DisplayMemberPath = ds.Tables["saloonz"].Columns["salonnumber"].ToString();
            ss.SelectedValuePath = ds.Tables["saloonz"].Columns["seatcapacity"].ToString();
            ActiveConnection.Close();
            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }
            SqlDA = new SqlDataAdapter("SELECT * FROM saloonz", ActiveConnection);
            ds = new DataSet();
            SqlDA.Fill(ds, "saloonz");
            SSal.DataContext = ds.Tables["saloonz"].DefaultView;
            SSal.DisplayMemberPath = ds.Tables["saloonz"].Columns["salonnumber"].ToString();
            SSal.SelectedValuePath = ds.Tables["saloonz"].Columns["seatcapacity"].ToString();
            ActiveConnection.Close();
            SqlDA = new SqlDataAdapter("SELECT * FROM saloonz", ActiveConnection);
            ds = new DataSet();
            SqlDA.Fill(ds, "saloonz");
            SSaloon.DataContext = ds.Tables["saloonz"].DefaultView;
            SSaloon.DisplayMemberPath = ds.Tables["saloonz"].Columns["salonnumber"].ToString();
            SSaloon.SelectedValuePath = ds.Tables["saloonz"].Columns["seatcapacity"].ToString();
            ActiveConnection.Close();
        }
        private void SSalonForSession()
        {
            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }
            SqlDataAdapter SqlDA = new SqlDataAdapter("SELECT * FROM saloonz", ActiveConnection);
            DataSet ds = new DataSet();
            SqlDA.Fill(ds, "saloonz");
            ss.DataContext = ds.Tables["saloonz"].DefaultView;
            ss.DisplayMemberPath = ds.Tables["saloonz"].Columns["salonnumber"].ToString();
            ss.SelectedValuePath = ds.Tables["saloonz"].Columns["seatcapacity"].ToString();
            ActiveConnection.Close();
            string dtz = DateTime.Now.ToString("dd/MM/yyyy");
            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }
            SqlDA = new SqlDataAdapter("Select * FROM moviez where @gsesdate between movieetvision and movieqtvision", ActiveConnection);
            SqlDA.SelectCommand.Parameters.AddWithValue("@gsesdate",getsesdate.Text);
            ds = new DataSet();
            SqlDA.Fill(ds, "moviez");
            ssfilm.DataContext = ds.Tables["moviez"].DefaultView;
            ssfilm.DisplayMemberPath = ds.Tables["moviez"].Columns["moviename"].ToString();
            ssfilm.SelectedValuePath = ds.Tables["moviez"].Columns["movieid"].ToString();
            ActiveConnection.Close();
        }
        private void DSaloon_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("If You Delete A Room, It Will Delete All The Sessions That Belong To The Saloon", "Information..!", MessageBoxButton.YesNo,MessageBoxImage.Information);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ActiveConnection.Open();
                    SqlCommand comm = new SqlCommand("DELETE FROM saloonz WHERE salonnumber=@ssalon", ActiveConnection);
                    comm.Parameters.AddWithValue("@ssalon",SSaloon.Text);
                    comm.ExecuteNonQuery();
                    ActiveConnection.Close();
                    MessageBox.Show("Process Completed..!!", "Information..!", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (ActiveConnection.State == ConnectionState.Closed)
                    {
                        ActiveConnection.Open();
                    }
                    SqlDataAdapter SqlDA = new SqlDataAdapter("SELECT * FROM saloonz", ActiveConnection);
                    DataSet ds = new DataSet();
                    SqlDA.Fill(ds, "saloonz");
                    SSaloon.DataContext = ds.Tables["saloonz"].DefaultView;
                    SSaloon.DisplayMemberPath = ds.Tables["saloonz"].Columns["salonnumber"].ToString();
                    SSaloon.SelectedValuePath = ds.Tables["saloonz"].Columns["seatcapacity"].ToString();
                    ActiveConnection.Close();
                    if (ActiveConnection.State == ConnectionState.Closed)
                    {
                        ActiveConnection.Open();
                    }
                    SqlDA = new SqlDataAdapter("SELECT * FROM saloonz", ActiveConnection);
                    ds = new DataSet();
                    SqlDA.Fill(ds, "saloonz");
                    ss.DataContext = ds.Tables["saloonz"].DefaultView;
                    ss.DisplayMemberPath = ds.Tables["saloonz"].Columns["salonnumber"].ToString();
                    ss.SelectedValuePath = ds.Tables["saloonz"].Columns["seatcapacity"].ToString();
                    ActiveConnection.Close();
                    if (ActiveConnection.State == ConnectionState.Closed)
                    {
                        ActiveConnection.Open();
                    }
                    SqlDA = new SqlDataAdapter("SELECT * FROM saloonz", ActiveConnection);
                    ds = new DataSet();
                    SqlDA.Fill(ds, "saloonz");
                    SSal.DataContext = ds.Tables["saloonz"].DefaultView;
                    SSal.DisplayMemberPath = ds.Tables["saloonz"].Columns["salonnumber"].ToString();
                    SSal.SelectedValuePath = ds.Tables["saloonz"].Columns["seatcapacity"].ToString();
                    ActiveConnection.Close();
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("Process Cancelled..!", "Information..!", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }

        private void DSession_Click(object sender, RoutedEventArgs e)
        {
            if (SSal.Text != "" && USessionCB.Text != "")
            {
                ActiveConnection.Open();
                SqlCommand comm = new SqlCommand("DELETE FROM sessionz WHERE sessiontime=@usessioncb and sessiondate=@sdate and salonnumber=@ssal", ActiveConnection);
                comm.Parameters.AddWithValue("@usessioncb",USessionCB.Text);
                comm.Parameters.AddWithValue("@sdate",sdate.Text);
                comm.Parameters.AddWithValue("@ssal",SSal.Text);
                comm.ExecuteNonQuery();
                ActiveConnection.Close();
                MessageBox.Show("Session Deleted..!", "Information!", MessageBoxButton.OK, MessageBoxImage.Information);
                if (ActiveConnection.State == ConnectionState.Closed)
                {
                    ActiveConnection.Open();
                }
                SqlDataAdapter SqlDA = new SqlDataAdapter("SELECT * FROM sessionz where salonnumber=@ssal and sessiondate=@sdate", ActiveConnection);
                SqlDA.SelectCommand.Parameters.AddWithValue("@ssal",SSal.Text);
                SqlDA.SelectCommand.Parameters.AddWithValue("@sdate",sdate.Text);
                DataSet ds = new DataSet();
                SqlDA.Fill(ds, "sessionz");
                USessionCB.DataContext = ds.Tables["sessionz"].DefaultView;
                USessionCB.DisplayMemberPath = ds.Tables["sessionz"].Columns["sessiontime"].ToString();
                USessionCB.SelectedValuePath = ds.Tables["sessionz"].Columns["sessionid"].ToString();
                ActiveConnection.Close();
            }
        }

        private void sbanner_Click(object sender, RoutedEventArgs e)
        {
            FileDialog FileDialog = new OpenFileDialog();   /* dosya konumunu alma */
            FileDialog.Filter = "Image files (*.jpg)|*.jpg";
            FileDialog.ShowDialog();
            path = FileDialog.FileName;
            int i = 0;
            string onlyFileName = System.IO.Path.GetFileName(FileDialog.FileName);
            while (File.Exists(dir1 + i + onlyFileName))
            {
                i++;
            }
            onlyFileName = i + onlyFileName;
                if (!File.Exists(dir1 + onlyFileName))
                {
                    File.Copy(path, dir1 + onlyFileName);
                }
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri(dir1+onlyFileName,UriKind.RelativeOrAbsolute);
                logo.EndInit();
                ifphoto.Source = logo;
        }

        private void SSal_DropDownClosed(object sender, EventArgs e)
        {
            if (SSal.Text != null && SSal.Text != "")
            {
                DateTime dt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                if (sdate.SelectedDate > dt)
                {
                    if (ActiveConnection.State == ConnectionState.Closed)
                    {
                        ActiveConnection.Open();
                    }
                    SqlDataAdapter SqlDA = new SqlDataAdapter("SELECT * FROM sessionz where salonnumber=@ssal and sessiondate=@sdate", ActiveConnection);
                    SqlDA.SelectCommand.Parameters.AddWithValue("@ssal",SSal.Text);
                    SqlDA.SelectCommand.Parameters.AddWithValue("@sdate", sdate.Text);
                    DataSet ds = new DataSet();
                    SqlDA.Fill(ds, "sessionz");
                    USessionCB.DataContext = ds.Tables["sessionz"].DefaultView;
                    USessionCB.DisplayMemberPath = ds.Tables["sessionz"].Columns["sessiontime"].ToString();
                    USessionCB.SelectedValuePath = ds.Tables["sessionz"].Columns["sessionid"].ToString();
                    ActiveConnection.Close();
                }
                else
                {
                    if (ActiveConnection.State == ConnectionState.Closed)
                    {
                        ActiveConnection.Open();
                    }
                    SqlDataAdapter SqlDA = new SqlDataAdapter("SELECT * FROM sessionz where salonnumber=@ssal and sessiondate=@sdate and sessiontime>=@stime", ActiveConnection);
                    SqlDA.SelectCommand.Parameters.AddWithValue("@ssal",SSal.Text);
                    SqlDA.SelectCommand.Parameters.AddWithValue("@sdate",sdate.Text);
                    SqlDA.SelectCommand.Parameters.AddWithValue("@stime", DateTime.Now.ToString("HH:mm"));
                    DataSet ds = new DataSet();
                    SqlDA.Fill(ds, "sessionz");
                    USessionCB.DataContext = ds.Tables["sessionz"].DefaultView;
                    USessionCB.DisplayMemberPath = ds.Tables["sessionz"].Columns["sessiontime"].ToString();
                    USessionCB.SelectedValuePath = ds.Tables["sessionz"].Columns["sessionid"].ToString();
                    ActiveConnection.Close();
                }
            }
        }
        private void fra_DropDownClosed(object sender, EventArgs e)
        {
            string dir = Directory.GetCurrentDirectory();
            dir = dir + @"\Videos\homepagefragmans\";
            if (fra.Text != null || fra.Text != "")
            {
                if (fra.SelectedValue != null)
                {
                    myMediaElement.Stop();
                    myMediaElement.Source = new Uri(dir + fra.SelectedValue.ToString(), UriKind.RelativeOrAbsolute);
                    myMediaElement.Play();
                }
            }
        }
        private void AddF_Click(object sender, RoutedEventArgs e)
        {
            if (mis3d.IsChecked == true)
            {
                ct = 1;
            }
            else
            {
                ct = 0;
            }
            String query = "INSERT INTO moviez (moviename,movieinfo,movietime,moviepricefull,moviepricestudent,movieis3d,moviebanner,movietype,movieetvision,movieqtvision)" +
                "VALUES (@moviename,@movieinfo,@movietime,@moviepricefull,@moviepricestudent,@movieis3d,@moviebanner,@movietype,@movieetvision,@movieqtvision)";

            SqlCommand command = new SqlCommand(query, ActiveConnection);
            ActiveConnection.Open();
            command.Parameters.AddWithValue("@moviename", mname.Text);
            command.Parameters.AddWithValue("@movieinfo", minfo.Text);
            command.Parameters.AddWithValue("@movietime", mtime.Text);
            command.Parameters.AddWithValue("@moviepricefull", mpf.Text);
            command.Parameters.AddWithValue("@moviepricestudent", mps.Text);
            command.Parameters.AddWithValue("@movieis3d", ct);
            command.Parameters.AddWithValue("@moviebanner", path);
            command.Parameters.AddWithValue("@movietype", smt.Text);
            command.Parameters.AddWithValue("@movieetvision", ev.Text);
            command.Parameters.AddWithValue("@movieqtvision", ov.Text);
            command.ExecuteNonQuery();
            ActiveConnection.Close();

            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }
            SqlDataAdapter SqlDA = new SqlDataAdapter("select * from moviez where @gsdate between movieetvision and movieqtvision", ActiveConnection);
            SqlDA.SelectCommand.Parameters.AddWithValue("@gsdate",getsesdate.Text);
            DataSet ds = new DataSet();
            SqlDA.Fill(ds, "moviez");
            ssfilm.DataContext = ds.Tables["moviez"].DefaultView;
            ssfilm.DisplayMemberPath = ds.Tables["moviez"].Columns["moviename"].ToString();
            ssfilm.SelectedValuePath = ds.Tables["moviez"].Columns["movieid"].ToString();
            ActiveConnection.Close();
        }

        private void upfftps_Click(object sender, RoutedEventArgs e)
        {
            string absoluteFileName = System.IO.Path.GetFileName(upphoto.Source.ToString());
            WebClient wc = new WebClient();
            wc.Credentials = new NetworkCredential("ackuasil", "Bx84qgP5g9");
            wc.UploadFile("ftp://ackuasil.cloudaccess.host/httpdocs/Images/enterthevisionphotos/" + absoluteFileName + "", pathi);
        }

        private void umf_Click(object sender, RoutedEventArgs e)
        {
            string absoluteFileName = System.IO.Path.GetFileName(myMediaElement.Source.ToString());
            WebClient wc = new WebClient();
            wc.Credentials = new NetworkCredential("ackuasil", "Bx84qgP5g9");
            wc.UploadFile("ftp://ackuasil.cloudaccess.host/httpdocs/Videos/homepagefragmans/" + absoluteFileName + "", pathv);
        }

        private void sdate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }
            SqlDataAdapter SqlDA = new SqlDataAdapter("SELECT * FROM saloonz", ActiveConnection);
            DataSet ds = new DataSet();
            SqlDA.Fill(ds, "saloonz");
            SSal.DataContext = ds.Tables["saloonz"].DefaultView;
            SSal.DisplayMemberPath = ds.Tables["saloonz"].Columns["salonnumber"].ToString();
            SSal.SelectedValuePath = ds.Tables["saloonz"].Columns["seatcapacity"].ToString();
            ActiveConnection.Close();
        }

        private void getsesdate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }
            SqlDataAdapter SqlDA = new SqlDataAdapter("select * from moviez where @gsdate between movieetvision and movieqtvision", ActiveConnection);
            SqlDA.SelectCommand.Parameters.AddWithValue("@gsdate",getsesdate.Text);
            DataSet ds = new DataSet();
            SqlDA.Fill(ds, "moviez");
            ssfilm.DataContext = ds.Tables["moviez"].DefaultView;
            ssfilm.DisplayMemberPath = ds.Tables["moviez"].Columns["moviename"].ToString();
            ssfilm.SelectedValuePath = ds.Tables["moviez"].Columns["movieid"].ToString();
            ActiveConnection.Close();
        }
    }
}
