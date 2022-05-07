using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for EditSeats.xaml
    /// </summary>
    public partial class EditSeats : UserControl
    {
        SqlConnection ActiveConnection = new SqlConnection("--");
        public EditSeats()
        {
            InitializeComponent();
            GetToday();
            BindComboBox();
        }
        void GetToday()
        {
            edate.BlackoutDates.AddDatesInPast();
            edate.SelectedDate = DateTime.Today;
        }

        void Create(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if(btn.Background.ToString() == "#FFFF0000" || btn.Background.ToString() == "#FF87CEFA")
            {
                MessageBox.Show("Can't Process This Seat."+Environment.NewLine+"Description: Seat Purchased or Reserved.","Information..!",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            if (btn.Background.ToString() == "#FF90EE90")
            {
                SqlCommand cmd = new SqlCommand("update seatz set seatstate=3 where seatname=@btn and sessiondate=@edate and howsestime=@esession", ActiveConnection);
                cmd.Parameters.AddWithValue("@btn",btn.Content);
                cmd.Parameters.AddWithValue("@edate",edate.Text);
                cmd.Parameters.AddWithValue("@esession",esession.Text);
                ActiveConnection.Open();
                cmd.ExecuteNonQuery();
                ActiveConnection.Close();
            }
            if (btn.Background.ToString() == "#FFD3D3D3")
            {
                SqlCommand cmd = new SqlCommand("update seatz set seatstate=1 where seatname=@btn and sessiondate=@edate and howsestime=@esession", ActiveConnection);
                cmd.Parameters.AddWithValue("@btn", btn.Content);
                cmd.Parameters.AddWithValue("@edate", edate.Text);
                cmd.Parameters.AddWithValue("@esession", esession.Text);
                ActiveConnection.Open();
                cmd.ExecuteNonQuery();
                ActiveConnection.Close();
            }
            if (Convert.ToInt16(esession.SelectedValue) != -1 && esession.SelectedValue != null)
            {
                a.Children.Clear();
                List<string> seatnamez = new List<string>();
                List<int> seatstatez = new List<int>();
                SqlCommand cmd = new SqlCommand("select seatname,seatstate from seatz where salonid=@sid and howsestime=@hstime and sessiondate=@sedate", ActiveConnection);
                cmd.Parameters.Add("@sid", SqlDbType.Int).Value = esession.SelectedValue;
                cmd.Parameters.Add("@hstime", SqlDbType.VarChar).Value = esession.Text;
                cmd.Parameters.Add("@sedate", SqlDbType.Date).Value = edate.Text;
                ActiveConnection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    seatnamez.Add(dr["seatname"].ToString());
                    seatstatez.Add(Convert.ToInt32(dr["seatstate"]));
                }
                for (int iaz = 0; iaz < seatnamez.Count; iaz++)
                {
                    string n = seatnamez[iaz].ToString();
                    int zx = seatstatez[iaz];
                    btn = new Button();

                    if (zx == 0)
                    {   //satıldı
                        btn.Background = new SolidColorBrush(Colors.Red);
                        btn.Tag = "fullseat";
                    }
                    if (zx == 1)
                    {   //boş
                        btn.Background = new SolidColorBrush(Colors.LightGreen);
                        btn.Tag = null;
                    }
                    if (zx == 2)
                    {   //rezervasyon alınmış
                        btn.Background = new SolidColorBrush(Colors.LightSkyBlue);
                        btn.Tag = "reserved";
                    }
                    if (zx == 3)
                    {   //gizlenmiş
                        btn.Background = new SolidColorBrush(Colors.LightGray);
                        btn.Tag = "outofservice";
                    }

                    btn.Content = n;

                    btn.Height = 30;

                    btn.Width = 40;

                    btn.Foreground = new SolidColorBrush(Colors.Black);

                    btn.Click += new RoutedEventHandler(Create);

                    btn.Margin = new Thickness(10);

                    a.Children.Add(btn);
                }
                ActiveConnection.Close();
                dr.Close();
            }
        }

        public void BindComboBox()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select moviename FROM moviez where @edate between movieetvision and movieqtvision", ActiveConnection);
            da.SelectCommand.Parameters.AddWithValue("@edate",edate.Text);
            DataSet ds = new DataSet();
            da.Fill(ds, "moviez");
            efilm.ItemsSource = ds.Tables[0].DefaultView;
            efilm.DisplayMemberPath = ds.Tables[0].Columns["moviename"].ToString();
            da.Dispose();
        }

        private void efilm_DropDownClosed(object sender, EventArgs e)
        {
            a.Children.Clear();
            SqlConnection ActiveConnection = new SqlConnection("Server=79.123.228.28,5501;Database=Gunduz_HAcku;User Id=hacku;Password=x7a45tt55;");
            SqlDataAdapter da = new SqlDataAdapter("Select * FROM sessionz where onsesfilmname=@efilm and sessiontime>=(SELECT CONVERT(VARCHAR(5),getdate(),108)) and sessiondate=@edate ", ActiveConnection);
            da.SelectCommand.Parameters.AddWithValue("@efilm",efilm.Text);
            da.SelectCommand.Parameters.AddWithValue("@edate",edate.Text);
            DataSet ds = new DataSet();
            da.Fill(ds, "sessionz");
            esession.ItemsSource = ds.Tables[0].DefaultView;
            esession.DisplayMemberPath = ds.Tables[0].Columns["sessiontime"].ToString();
            esession.SelectedValuePath = ds.Tables[0].Columns["salonnumber"].ToString();
            da.Dispose();
            DateTime dt = Convert.ToDateTime(DateTime.Now.ToString("yyyy - MM - dd"));
            if (edate.SelectedDate > dt)
            {
                a.Children.Clear();
                ActiveConnection.Open();
                da = new SqlDataAdapter("Select * FROM sessionz where onsesfilmname=@efilm and sessiondate=@edate ", ActiveConnection);
                da.SelectCommand.Parameters.AddWithValue("@efilm", efilm.Text);
                da.SelectCommand.Parameters.AddWithValue("@edate", edate.Text);
                ds = new DataSet();
                da.Fill(ds, "sessionz");
                esession.ItemsSource = ds.Tables[0].DefaultView;
                esession.DisplayMemberPath = ds.Tables[0].Columns["sessiontime"].ToString();
                esession.SelectedValuePath = ds.Tables[0].Columns["salonnumber"].ToString();
                da.Dispose();
                ActiveConnection.Close();
            }
        }

        private void esession_DropDownClosed(object sender, EventArgs e)
        {
            if (Convert.ToInt16(esession.SelectedValue) != -1 && esession.SelectedValue != null)
            {
                a.Children.Clear();
                List<string> seatnamez = new List<string>();
                List<int> seatstatez = new List<int>();
                SqlCommand cmd = new SqlCommand("select seatname,seatstate from seatz where salonid=@sid and howsestime=@hstime and sessiondate=@sedate", ActiveConnection);
                cmd.Parameters.Add("@sid", SqlDbType.Int).Value = esession.SelectedValue;
                cmd.Parameters.Add("@hstime", SqlDbType.VarChar).Value = esession.Text;
                cmd.Parameters.Add("@sedate", SqlDbType.Date).Value = edate.Text;
                ActiveConnection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    seatnamez.Add(dr["seatname"].ToString());
                    seatstatez.Add(Convert.ToInt32(dr["seatstate"]));
                }
                for (int iaz = 0; iaz < seatnamez.Count; iaz++)
                {
                    string n = seatnamez[iaz].ToString();
                    int zx = seatstatez[iaz];
                    Button btn = new Button();

                    if (zx == 0)
                    {   //satıldı
                        btn.Background = new SolidColorBrush(Colors.Red);
                        btn.Tag = "fullseat";
                    }
                    if (zx == 1)
                    {   //boş
                        btn.Background = new SolidColorBrush(Colors.LightGreen);
                        btn.Tag = null;
                    }
                    if (zx == 2)
                    {   //rezervasyon alınmış
                        btn.Background = new SolidColorBrush(Colors.LightSkyBlue);
                        btn.Tag = "reserved";
                    }
                    if (zx == 3)
                    {   //gizlenmiş
                        btn.Background = new SolidColorBrush(Colors.LightGray);
                        btn.Tag = "outofservice";
                    }

                    btn.Content = n;

                    btn.Height = 30;

                    btn.Width = 40;

                    btn.Foreground = new SolidColorBrush(Colors.Black);

                    btn.Click += new RoutedEventHandler(Create);

                    btn.Margin = new Thickness(10);

                    a.Children.Add(btn);
                }
                ActiveConnection.Close();
                dr.Close();
            }
        }
        private void edate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            esession.ItemsSource = null;
            a.Children.Clear();
            SqlConnection ActiveConnection = new SqlConnection("Server=79.123.228.28,5501;Database=Gunduz_HAcku;User Id=hacku;Password=x7a45tt55;");
            SqlDataAdapter da = new SqlDataAdapter("Select * FROM moviez where @edate between movieetvision and movieqtvision", ActiveConnection);
            da.SelectCommand.Parameters.AddWithValue("@edate",edate.Text);
            DataSet ds = new DataSet();
            da.Fill(ds, "moviez");
            efilm.ItemsSource = ds.Tables[0].DefaultView;
            efilm.DisplayMemberPath = ds.Tables[0].Columns["moviename"].ToString();
            da.Dispose();
        }
    }
}
