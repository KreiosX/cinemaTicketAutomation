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
    /// Interaction logic for TicketCancellation.xaml
    /// </summary>
    public partial class TicketCancellation : UserControl
    {
        double SPrice = 0;
        double FPrice = 0;
        int SNumber = 0;
        int FNumber = 0;
        string MName;
        DateTime MDate;
        string MSession;
        List<string> seatz = new List<string>();
        SqlConnection ActiveConnection = new SqlConnection("cs");
        public TicketCancellation()
        {
            InitializeComponent();
            rcanc.Text = "If You Enter The Cancellation Code, You Don't Need To Select The Date And Session Time";
            rcanc0.Text = "If You Enter The Reservation Code, You Don't Need To Select The Date And Session Time";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> seatname = new List<string>();
            List<string> sestime = new List<string>();
            List<string> movdate = new List<string>();
            List<string> movname = new List<string>();
            if (cancses.Text == "" && cancdate.Text =="")
            {
                SqlCommand cmdz = new SqlCommand("select * from SelledTicketInf where cancticked=@cancticked", ActiveConnection);
                cmdz.Parameters.Add("@cancticked", SqlDbType.VarChar).Value = cticket.Text;
                ActiveConnection.Open();
                SqlDataReader drz = cmdz.ExecuteReader();
                while (drz.Read())
                {
                    seatname.Add(drz["seatname"].ToString());
                    sestime.Add(drz["sestime"].ToString());
                    movdate.Add(Convert.ToDateTime(drz["movdate"]).ToString("dd/MM/yyyy"));
                    movname.Add(drz["movname"].ToString());
                }
                drz.Close();
                for (int i = 0; i < seatname.Count; i++)
                {
                    cmdz = new SqlCommand("update seatz set seatstate=1 where seatname=@seatname and howsestime=@sestime and sessiondate=@movdate", ActiveConnection);
                    cmdz.Parameters.AddWithValue("@seatname", seatname[i]);
                    cmdz.Parameters.AddWithValue("@sestime", sestime[i]);
                    cmdz.Parameters.AddWithValue("@movdate", movdate[i]);
                    cmdz.ExecuteNonQuery();
                    cmdz = new SqlCommand("delete from SelledTicketInf where seatname=@seatname and sestime=@sestime and movdate=@movdate", ActiveConnection);
                    cmdz.Parameters.AddWithValue("@seatname", seatname[i]);
                    cmdz.Parameters.AddWithValue("@sestime", sestime[i]);
                    cmdz.Parameters.AddWithValue("@movdate", movdate[i]);
                    cmdz.ExecuteNonQuery();
                }
                ActiveConnection.Close();
                cticket.Text = "";
                MessageBox.Show("Process Completed!", "Congratulations..!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("select * from SelledTicketInf where custcitnumber=@custcitnumber and movdate=@movdate and sestime=@sestime", ActiveConnection);
                cmd.Parameters.Add("@custcitnumber", SqlDbType.BigInt).Value = cticket.Text;
                cmd.Parameters.Add("@movdate", SqlDbType.Date).Value = cancdate.Text;
                cmd.Parameters.Add("@sestime", SqlDbType.VarChar).Value = cancses.Text;
                ActiveConnection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    seatname.Add(dr["seatname"].ToString());
                    sestime.Add(dr["sestime"].ToString());
                    movdate.Add(Convert.ToDateTime(dr["movdate"]).ToString("dd/MM/yyyy"));
                    movname.Add(dr["movname"].ToString());
                }
                dr.Close();
                for (int i = 0; i < seatname.Count; i++)
                {
                    cmd = new SqlCommand("update seatz set seatstate=1 where seatname=@seatname and howsestime=@sestime and sessiondate=@movdate and seatstate=2 or seatstate=0 ", ActiveConnection);
                    cmd.Parameters.AddWithValue("@seatname", seatname[i]);
                    cmd.Parameters.AddWithValue("@sestime", sestime[i]);
                    cmd.Parameters.AddWithValue("@movdate", movdate[i]);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("delete from SelledTicketInf where seatname=@seatname and sestime=@sestime and movdate=@movdate", ActiveConnection);
                    cmd.Parameters.AddWithValue("@seatname", seatname[i]);
                    cmd.Parameters.AddWithValue("@sestime", sestime[i]);
                    cmd.Parameters.AddWithValue("@movdate", movdate[i]);
                    cmd.ExecuteNonQuery();
                }
                cticket.Text = "";
                cancdate.SelectedDate = null;
                cancses.Text = "";
                MessageBox.Show("Process Completed!", "Congratulations..!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            ActiveConnection.Close();
        }

        private void SearchReserv_Click(object sender, RoutedEventArgs e)
        {
            if (RDate.Text  == null || RDate.Text == "" && RSessionTime.Text == "")
            {
                SqlCommand cmd = new SqlCommand("select * from SelledTicketInf where cancticked=@cancticked", ActiveConnection);
                cmd.Parameters.Add("@cancticked", SqlDbType.BigInt).Value = RCNumberG.Text;
                ActiveConnection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    rname.Text = dr["custname"].ToString();
                    rsurname.Text = dr["custsurname"].ToString();
                    seatz.Add(dr["seatname"].ToString());
                    MName = dr["movname"].ToString();
                    SNumber = Convert.ToInt32(dr["stutic"]);
                    FNumber = Convert.ToInt32(dr["fulltic"]);
                    MDate = Convert.ToDateTime(dr["movdate"]);
                    MSession = dr["sestime"].ToString();
                }
                ActiveConnection.Close();
                cmd = new SqlCommand("select * from moviez where moviename=@mname", ActiveConnection);
                cmd.Parameters.AddWithValue("@mname",MName);
                ActiveConnection.Open();
                SqlDataReader drz = cmd.ExecuteReader();
                while (drz.Read())
                {
                    SPrice = Convert.ToDouble(drz["moviepricestudent"]);
                    FPrice = Convert.ToDouble(drz["moviepricefull"]);
                }
                RAccount.Text = Convert.ToString(SPrice * SNumber + FPrice * FNumber);
                drz.Close();
                cmd.Dispose();
                ActiveConnection.Close();
            }
            else if(RCNumberG.Text != "" && RDate.Text != "" && RSessionTime.Text != "")
            {
                SqlCommand cmd = new SqlCommand("select * from SelledTicketInf where custcitnumber=@custcitnumber and movdate=@movdate and sestime=@sestime", ActiveConnection);
                cmd.Parameters.Add("@custcitnumber", SqlDbType.BigInt).Value = RCNumberG.Text;
                cmd.Parameters.Add("@movdate", SqlDbType.VarChar).Value = RDate.Text;
                cmd.Parameters.Add("@sestime", SqlDbType.VarChar).Value = RSessionTime.Text;
                ActiveConnection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    rname.Text = dr["custname"].ToString();
                    rsurname.Text = dr["custsurname"].ToString();
                    seatz.Add(dr["seatname"].ToString());
                    MName = dr["movname"].ToString();
                    SNumber = Convert.ToInt32(dr["stutic"]);
                    FNumber = Convert.ToInt32(dr["fulltic"]);
                    MDate = Convert.ToDateTime(dr["movdate"]);
                    MSession = dr["sestime"].ToString();
                }
                ActiveConnection.Close();
                dr.Close();
                cmd = new SqlCommand("select * from moviez where moviename=@mname", ActiveConnection);
                cmd.Parameters.AddWithValue("@mname",MName);
                ActiveConnection.Open();
                SqlDataReader drz = cmd.ExecuteReader();
                while (drz.Read())
                {
                    SPrice = Convert.ToDouble(drz["moviepricestudent"]);
                    FPrice = Convert.ToDouble(drz["moviepricefull"]);
                }
                RAccount.Text = Convert.ToString(SPrice * SNumber + FPrice * FNumber);
                drz.Close();
                cmd.Dispose();
                ActiveConnection.Close();
            }
            else
            {
                MessageBox.Show("Please Fill In The Required Fields..!", "Information..!", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void RBuy_Click(object sender, RoutedEventArgs e)
        {
            if (RDate.Text == null || RDate.Text == "" && RSessionTime.Text == "")
            {
                for (int i = 0; i < seatz.Count; i++)
                {
                    MDate.ToString("dd/MM/yyyy");
                    SqlCommand cmd = new SqlCommand("update seatz set seatstate=0 where seatname=@seatz and seatstate=2 and sessiondate=@mdate and howsestime=@msession", ActiveConnection);
                    cmd.Parameters.AddWithValue("@seatz", seatz[i]);
                    cmd.Parameters.AddWithValue("@mdate", MDate);
                    cmd.Parameters.AddWithValue("@msession", MSession);
                    ActiveConnection.Open();
                    cmd.ExecuteNonQuery();
                    ActiveConnection.Close();
                }
                RCNumberG.Text = "";
                MessageBox.Show("Your Reservation Is Confirmed..!", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if(RDate.Text != null || RDate.Text != "" && RSessionTime.Text != "" && RCNumberG.Text != "")
            {
                for (int i = 0; i < seatz.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand("update seatz set seatstate=0 where seatname=@seatz and seatstate=2 and sessiondate=@mdate and howsestime=@msession", ActiveConnection);
                    cmd.Parameters.AddWithValue("@seatz", seatz[i]);
                    cmd.Parameters.AddWithValue("@mdate", MDate);
                    cmd.Parameters.AddWithValue("@msession", MSession);
                    ActiveConnection.Open();
                    cmd.ExecuteNonQuery();
                    ActiveConnection.Close();
                }
                RDate.Text = null;
                RSessionTime.Text = "";
                RCNumberG.Text = "";
                MessageBox.Show("Your Reservation Is Confirmed..!", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please Fill In The Required Fields..!", "Information..!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
