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
using System.Windows.Threading;
using static AsilCinemaTicketAutomation.Operations;

namespace AsilCinemaTicketAutomation
{
    /// <summary>
    /// Interaction logic for Authorization.xaml
    /// </summary>
    public partial class Authorization : UserControl
    {
        public static int SelectedUserId = 0;
        public static string SelectedUserType;
        SqlConnection ActiveConnection = new SqlConnection("Server=79.123.228.28,5501;Database=Gunduz_HAcku;User Id=hacku;Password = x7a45tt55;");
        SqlCommand SqlCom;
        public Authorization()
        {
            InitializeComponent();
            Lists();
            if (Perm(UserLoginId, 2) == true)
            {
                AddUser.Visibility = Visibility.Visible;
            }
            else
            {
                AddUser.Visibility = Visibility.Collapsed;
            }
            if (Perm(UserLoginId, 3) == true)
            {
                st2.IsEnabled = true;
                Authorize.IsEnabled = true;
                TBAuthority.IsEnabled = true;
            }
            else
            {
                st2.IsEnabled = false;
                Authorize.IsEnabled = false;
                TBAuthority.IsEnabled = false;
            }
            if (Perm(UserLoginId, 4) == true)
            {
                AddUser.Visibility = Visibility.Visible;
            }
            else
            {
                AddUser.Visibility = Visibility.Collapsed;
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

        void ListAuthorizationName()
        {
            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }
            if (UserType == "Code Master")
            {
                if (SelectedUserType == "Code Master")
                {
                    SqlCommand komut = new SqlCommand("SELECT * FROM authorizationNames", ActiveConnection);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        AuthorizationCB = new CheckBox();
                        int sayac = 0;
                        AuthorizationCB.Foreground = new SolidColorBrush(Colors.White);
                        AuthorizationCB.Margin = new Thickness(5);
                        AuthorizationCB.Content = oku[sayac + 1];
                        AuthorizationCB.Tag = oku[sayac];
                        st2.Children.Add(AuthorizationCB);
                    }
                    oku.Close();
                    komut.Dispose();
                }
                else if (SelectedUserType == "Admin")
                {
                    SqlCommand komut = new SqlCommand("SELECT * FROM authorizationNames where not usertypeid=1", ActiveConnection);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        AuthorizationCB = new CheckBox();
                        int sayac = 0;
                        AuthorizationCB.Foreground = new SolidColorBrush(Colors.White);
                        AuthorizationCB.Margin = new Thickness(5);
                        AuthorizationCB.Content = oku[sayac + 1];
                        AuthorizationCB.Tag = oku[sayac];
                        st2.Children.Add(AuthorizationCB);
                    }
                    oku.Close();
                    komut.Dispose();
                }
                else
                {
                    SqlCommand komut = new SqlCommand("SELECT * FROM authorizationNames where usertypeid=3", ActiveConnection);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        AuthorizationCB = new CheckBox();
                        int sayac = 0;
                        AuthorizationCB.Foreground = new SolidColorBrush(Colors.White);
                        AuthorizationCB.Margin = new Thickness(5);
                        AuthorizationCB.Content = oku[sayac + 1];
                        AuthorizationCB.Tag = oku[sayac];
                        st2.Children.Add(AuthorizationCB);
                    }
                    oku.Close();
                    komut.Dispose();
                }
            }
            else if (UserType == "Admin")
            {
                if (SelectedUserType == "Code Master" || SelectedUserType == "Admin")
                {
                    SqlCommand komutq = new SqlCommand("SELECT * FROM authorizationNames where not usertypeid=1", ActiveConnection);
                    SqlDataReader okuq = komutq.ExecuteReader();
                    while (okuq.Read())
                    {
                        AuthorizationCB = new CheckBox();
                        int sayac = 0;
                        AuthorizationCB.Foreground = new SolidColorBrush(Colors.White);
                        AuthorizationCB.Margin = new Thickness(5);
                        AuthorizationCB.Content = okuq[sayac + 1];
                        AuthorizationCB.Tag = okuq[sayac];
                        st2.Children.Add(AuthorizationCB);
                    }
                    okuq.Close();
                    komutq.Dispose();
                }
                else
                {
                    SqlCommand komutqa = new SqlCommand("SELECT * FROM authorizationNames where usertypeid=3", ActiveConnection);
                    SqlDataReader okuqa = komutqa.ExecuteReader();
                    while (okuqa.Read())
                    {
                        AuthorizationCB = new CheckBox();
                        int sayac = 0;
                        AuthorizationCB.Foreground = new SolidColorBrush(Colors.White);
                        AuthorizationCB.Margin = new Thickness(5);
                        AuthorizationCB.Content = okuqa[sayac + 1];
                        AuthorizationCB.Tag = okuqa[sayac];
                        st2.Children.Add(AuthorizationCB);
                    }
                    okuqa.Close();
                    komutqa.Dispose();
                }
            }
            else
            {
                SqlCommand komut = new SqlCommand("SELECT * FROM authorizationNames where usertypeid=3", ActiveConnection);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    AuthorizationCB = new CheckBox();
                    int sayac = 0;
                    AuthorizationCB.Foreground = new SolidColorBrush(Colors.White);
                    AuthorizationCB.Margin = new Thickness(5);
                    AuthorizationCB.Content = oku[sayac + 1];
                    AuthorizationCB.Tag = oku[sayac];
                    st2.Children.Add(AuthorizationCB);
                }
                oku.Close();
                komut.Dispose();
            }
        }

        void Lists()
        {
            if (ActiveConnection.State != ConnectionState.Open)
            {
                ActiveConnection.Open();
            }
            SqlCom = new SqlCommand("SELECT (SELECT usertype from usertype where id=usertypeid) as 'User Type',id as 'Line', name as 'Name',surname as 'Surname',citizenshipnumber as 'Citizenship Number' FROM userinfo", ActiveConnection);
            SqlDataAdapter SqlDataAdapt = new SqlDataAdapter(SqlCom);
            DataTable DTable = new DataTable("userinfo");
            SqlDataAdapt.Fill(DTable);
            dg1.ItemsSource = "";
            int i = Convert.ToInt16(dg1.SelectedValue);
            dg1.ItemsSource = DTable.DefaultView;
            SqlCom.Dispose();
            SqlDataAdapt.Dispose();
            ActiveConnection.Close();
        }

        int AuthorityControl(int a)
        {
            int ReturnedValue = 0;
            if (ActiveConnection.State != ConnectionState.Open)
            {
                ActiveConnection.Open();
            }
            SqlCom = new SqlCommand("SELECT COUNT(id) from Authorize where userid =@userid and authorizationid=@authorizationid", ActiveConnection);
            SqlCom.Parameters.AddWithValue("@userid", UserLoginId);
            SqlCom.Parameters.AddWithValue("@authorizationid", a);
            SqlDataReader DataR = SqlCom.ExecuteReader();
            while (DataR.Read())
            {
                ReturnedValue = (int)DataR[0];
            }
            DataR.Close();
            SqlCom.Dispose();
            return ReturnedValue;
        }
        private void SearchUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ActiveConnection.State != ConnectionState.Open)
            {
                ActiveConnection.Open();
            }
            if (SearchUser.Text != "")
            {
                SqlCom = new SqlCommand("SELECT (SELECT usertype from usertype where id=usertypeid) as 'User Type',id as 'Line', name as 'Name' ,surname as 'Surname',citizenshipnumber as 'Citizenship Number' FROM userinfo WHERE citizenshipnumber LIKE '%' + @stext + '%' or name LIKE '%' + @stext + '%' or surname LIKE '%' + @stext + '%'", ActiveConnection);
                SqlCom.Parameters.AddWithValue("@stext",SearchUser.Text);
                SqlDataAdapter SqlDataAdapt = new SqlDataAdapter(SqlCom);
                DataTable DataTabl = new DataTable("userinfo");
                SqlDataAdapt.Fill(DataTabl);
                dg1.ItemsSource = "";
                dg1.ItemsSource = DataTabl.DefaultView;

            }
            else
            {
                Lists();
            }

            SqlCom.Dispose();
            ActiveConnection.Close();
        }


        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            st2.Children.Clear();
            DataGrid dg1 = (DataGrid)sender;
            DataRowView selected = dg1.SelectedItem as DataRowView;
            if (selected != null)
            {
                SelectedUserId = Convert.ToInt32(selected["Line"]);
                SelectedUserType = selected["User Type"].ToString();
                ListAuthorizationName();
                if (ActiveConnection.State != ConnectionState.Open)
                {
                    ActiveConnection.Open();
                }
                SqlCom = new SqlCommand("SELECT * FROM Authorize WHERE userid=@userid", ActiveConnection);
                SqlCom.Parameters.AddWithValue("@userid", SelectedUserId);
                SqlDataReader SqlDataR = SqlCom.ExecuteReader();
                List<string> MyList = new List<string>();
                while (SqlDataR.Read())
                {
                    MyList.Add(SqlDataR[2].ToString());
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
            if (selected != null)
            {
                SelectedUserId = Convert.ToInt32(selected["Line"]);
                SelectedUserType = selected["User Type"].ToString();
                if (UserType == "Code Master" && SelectedUserType == "Code Master")
                {
                    foreach (Object item in st2.Children)
                    {
                        CheckBox CB = (CheckBox)item;
                        CB.IsEnabled = false;
                    }
                }
                if (UserType == "Admin" && SelectedUserType == "Admin" || UserType == "Admin" && SelectedUserType == "Code Master")
                {
                    foreach (Object item in st2.Children)
                    {
                        CheckBox CB = (CheckBox)item;
                        CB.IsEnabled = false;
                    }
                }
            }
        }

        void Updates()
        {
            foreach (CheckBox item in st2.Children)
            {

                if (item.IsChecked == false)
                {
                    if (AuthorityControl(int.Parse(item.Tag.ToString())) == 1)
                    {
                        SqlCom = new SqlCommand("DELETE FROM Authorize WHERE userid=@userid ansd authorizationnameid=@authorizationnameid", ActiveConnection);
                        SqlCom.Parameters.AddWithValue("@userid", UserLoginId);
                        SqlCom.Parameters.AddWithValue("@authorizationnameid", item.Tag);

                        SqlCom.ExecuteNonQuery();
                    }
                }
            }

            foreach (CheckBox item in st2.Children)
            {

                if (item.IsChecked == true)
                {
                    if (AuthorityControl(int.Parse(item.Tag.ToString())) == 0)
                    {
                        SqlCom = new SqlCommand("INSERT INTO Authorize(userid,authorizationnameid)VALUES(@userid,@authorizationnameid)", ActiveConnection);
                        SqlCom.Parameters.AddWithValue("@userid", UserLoginId);
                        SqlCom.Parameters.AddWithValue("@authorizationnameid", item.Tag);
                        SqlCom.ExecuteNonQuery();
                    }
                }
            }

        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AdminRegPan RPanel = new AdminRegPan();
            mainp.Children.Clear();
            mainp.Children.Add(RPanel);
        }
        private void Authorize_Click(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox item in st2.Children)
            {

                if (item.IsChecked == true)
                {
                    SqlCommand komut = new SqlCommand("IF NOT EXISTS(select * from Authorize where userid=@userid and authorizationid=@authorizationid)INSERT INTO Authorize(userid,authorizationid)VALUES(@userid,@authorizationid)", ActiveConnection);
                    komut.Parameters.AddWithValue("@userid", SelectedUserId);
                    komut.Parameters.AddWithValue("@authorizationid", item.Tag);

                    komut.ExecuteNonQuery();
                }
            }
        }

        private void TBAuthority_Click(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox item in st2.Children)
            {

                if (item.IsChecked == false)
                {
                    SqlCommand komut = new SqlCommand("IF EXISTS(select * from Authorize where userid=@userid and authorizationid=@authorizationid)DELETE FROM Authorize WHERE userid=@userid and authorizationid=@authorizationid", ActiveConnection);
                    komut.Parameters.AddWithValue("@userid", SelectedUserId);
                    komut.Parameters.AddWithValue("@authorizationid", item.Tag);

                    komut.ExecuteNonQuery();
                }
            }
        }
    }
}