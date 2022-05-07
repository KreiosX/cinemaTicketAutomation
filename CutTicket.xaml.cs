using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
using System.Windows.Threading;

namespace AsilCinemaTicketAutomation
{
    /// <summary>
    /// Interaction logic for CutTicket.xaml
    /// </summary>
    public partial class CutTicket : UserControl
    {
        double is3d = 0;
        double sprice, fprice;
        private SqlConnection ActiveConnection = new SqlConnection("---");
        int studenty = 0;
        int fully = 0;
        public CutTicket()
        {
            InitializeComponent();
            BlokDatendCountEmptyFullSeat();
            DTimer();
            BindComboBox();
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

        //Boş Koltukları Saydırma ve DatePicker'da Geçmişi Bloklama

        public void DTimer()
        {
            DispatcherTimer DTimer = new DispatcherTimer();
            DTimer.Tick += DTimer_Tick;
            DTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            DTimer.Start();
        }

        private void DTimer_Tick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(selectedseat.Text) == 0 || Convert.ToInt32(selectedseat.Text) == Convert.ToInt32(student.Text) + Convert.ToInt32(full.Text))
            {
                splus.IsEnabled = false;
                fplus.IsEnabled = false;
            }
            else
            {
                splus.IsEnabled = true;
                fplus.IsEnabled = true;
            }

            if (studenty >= 0)
            {
                sminus.IsEnabled = true;
            }

            if (studenty < 1)
            {
                sminus.IsEnabled = false;
            }
            if (fully >= 0)
            {
                fminus.IsEnabled = true;
            }

            if (fully < 1)
            {
                fminus.IsEnabled = false;
            }
            //------------------------------------

            int ReservedSeat = 0;
            foreach (Object aa in a.Children)
            {
                Button aab = (Button)aa;
                if (aab.Tag == "reserved")
                {
                    ReservedSeat++;
                }
            }
            rseats.Text = ReservedSeat.ToString();

            int EmptySeat = 0;
            foreach (Object aa in a.Children)
            {
                Button aab = (Button)aa;
                if (aab.Tag == null)
                {
                    EmptySeat++;
                }
            }
            eseats.Text = EmptySeat.ToString();

            int FullSeat = 0;
            foreach (Object aaa in a.Children)
            {
                Button aabb = (Button)aaa;
                if (aabb.Tag == "fullseat")
                {
                    FullSeat++;
                }
            }
            fseats.Text = FullSeat.ToString();

            int SelectedSeat = 0;
            foreach (Object aaa in a.Children)
            {
                Button aabbb = (Button)aaa;
                if (aabbb.Tag == "selectedseat")
                {
                    SelectedSeat++;
                }
            }
            selectedseat.Text = SelectedSeat.ToString();
            CommandManager.InvalidateRequerySuggested();
        }
        private void BlokDatendCountEmptyFullSeat()
        {
            // sadece şimdiki ve gelecek zaman seçilebilir -- getdate datepicker adı
            //getdate.DisplayDateStart = DateTime.Today; -- bunu yaparsan geçmiş günler tamamen görünmez olur diğeri daha estetik X işareti filan var :D
            getdate.BlackoutDates.AddDatesInPast();
            getdate.SelectedDate = DateTime.Today;
        }
        private void OnlyNumeric(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public void BindComboBox()
        {
            SqlConnection ActiveConnection = new SqlConnection("Server=79.123.228.28,5501;Database=Gunduz_HAcku;User Id=hacku;Password=x7a45tt55;");
            SqlDataAdapter da = new SqlDataAdapter("Select moviename FROM moviez where @gdate between movieetvision and movieqtvision", ActiveConnection);
            da.SelectCommand.Parameters.AddWithValue("@gdate",getdate.Text);
            DataSet ds = new DataSet();
            da.Fill(ds, "moviez");
            sfilm.ItemsSource = ds.Tables[0].DefaultView;
            sfilm.DisplayMemberPath = ds.Tables[0].Columns["moviename"].ToString();
            da.Dispose();
        }
        void Create(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Background.ToString() == "#FFFF0000")
            {
                MessageBox.Show("This Seat Is Full. Please Select Another One ..", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (btn.Background.ToString() == "#FF87CEFA")
            {
                MessageBox.Show("This Seat Is Reserved. Please Select Another One ..", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("If The Ticket Is Still Not Bought For 6 Hours Before The Start of The Movie, The Reservation Is Automatically Canceling...", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (btn.Background.ToString() == "#FF90EE90")
            {
                btn.Background = new SolidColorBrush(Colors.Wheat);
                btn.Tag = "selectedseat";
            }
            else if (btn.Background.ToString() == "#FFF5DEB3")
            {
                btn.Background = new SolidColorBrush(Colors.LightGreen);
                btn.Tag = null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt16(student.Text) + Convert.ToInt16(full.Text) < Convert.ToInt16(selectedseat.Text))
            {
                MessageBox.Show("The Number Of Seats You Choose Can't Be Smaller Than The Specified Number Of Seats.", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Convert.ToInt16(student.Text) + Convert.ToInt16(full.Text) > Convert.ToInt16(selectedseat.Text))
            {
                MessageBox.Show("The Number Of Seats You Choose Can't Be Greater Than The Specified Number Of Seats.", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Convert.ToInt16(selectedseat.Text) == Convert.ToInt16(full.Text) + Convert.ToInt16(student.Text) && namec.Text != "" && surnamec.Text != "" && citizenshipc.Text != "" && namec.Text != "Name" && surnamec.Text != "Surname" && citizenshipc.Text != "Citizenship Number")
            {
                Random rastgele = new Random();
                string sayilar = citizenshipc.Text;
                string uret = "";
                for (int iiq = 0; iiq < 7; iiq++)
                {
                    uret += sayilar[rastgele.Next(sayilar.Length)];
                }
                int iazq;
                SqlCommand cmdaq = new SqlCommand("select max(id) from SelledTicketInf", ActiveConnection);
                ActiveConnection.Open();
                SqlDataReader rq = cmdaq.ExecuteReader();
                if (rq.Read())
                {
                    iazq = (int)rq[0];
                    uret += iazq;
                }
                rq.Close();
                cmdaq.Dispose();
                ActiveConnection.Close();
                foreach (Object x in a.Children)
                {
                    Button y = (Button)x;
                    if (y.Tag == "selectedseat")
                    {
                        List<string> sseatzx = new List<string>();
                        sseatzx.Add(y.Content.ToString());
                        for (int i = 0; i < sseatzx.Count; i++)
                        {
                            SqlCommand cmd = new SqlCommand("INSERT INTO SelledTicketInf (custname,custsurname,custcitnumber,seatname,salnumber," +
                            "sestime,movname,movdate,stutic,fulltic,cancticked) VALUES (@custname,@custsurname,@custcitnumber,@seatname,@salnumber,@sestime" +
                            ",@movname,@movdate,@stutic,@fulltic,@cancticked)", ActiveConnection);
                            if (ActiveConnection.State == ConnectionState.Closed)
                            {
                                ActiveConnection.Open();
                            }
                            cmd.Parameters.AddWithValue("@custname", namec.Text);
                            cmd.Parameters.AddWithValue("@custsurname", surnamec.Text);
                            cmd.Parameters.AddWithValue("@custcitnumber", citizenshipc.Text);
                            cmd.Parameters.AddWithValue("@seatname", sseatzx[i]);
                            cmd.Parameters.AddWithValue("@salnumber", ssession.SelectedValue);
                            cmd.Parameters.AddWithValue("@sestime", ssession.Text);
                            cmd.Parameters.AddWithValue("@movname", sfilm.Text);
                            cmd.Parameters.AddWithValue("@movdate", getdate.Text);
                            cmd.Parameters.AddWithValue("@stutic", student.Text);
                            cmd.Parameters.AddWithValue("@fulltic", full.Text);
                            cmd.Parameters.AddWithValue("@cancticked", uret);
                            cmd.ExecuteNonQuery();
                            cmd = new SqlCommand("update seatz set seatstate=0 where salonid=@sid and seatname=@sname and howsestime=@hstime", ActiveConnection);
                            cmd.Parameters.AddWithValue("@sid", ssession.SelectedValue);
                            cmd.Parameters.AddWithValue("@sname", sseatzx[i]);
                            cmd.Parameters.AddWithValue("@hstime", ssession.Text);
                            cmd.ExecuteNonQuery();
                            ActiveConnection.Close();
                        }
                        SqlCommand cmdct = new SqlCommand("select top 1 cancticked from SelledTicketInf order by id desc", ActiveConnection);
                        ActiveConnection.Open();
                        SqlDataReader drct = cmdct.ExecuteReader();
                        long canc = 0;
                        List<string> sst = new List<string>();
                        if (drct.Read())
                        {
                            canc = Convert.ToInt64(drct[0]);
                        }
                        ActiveConnection.Close();
                        foreach (Button ss in a.Children)
                        {
                            if (ss.Tag == "selectedseat")
                            {
                                sst.Add(ss.Content.ToString() + " |");
                            }
                        }
                        string ls = sst.Last();
                        if (ls.EndsWith("|"))
                        {
                            ls = ls.Substring(0, ls.Length - 1);
                        }
                        sst.Remove(sst.Last());
                        sst.Add(ls);
                        mi3d.Visibility = Visibility.Collapsed;
                        tcan.FontSize = 13;
                        tcan.Text = "Ticket Cancellation Code: ";
                        a1.Visibility = Visibility.Collapsed;
                        a2.Visibility = Visibility.Collapsed;
                        a3.Visibility = Visibility.Collapsed;
                        seats.Visibility = Visibility.Collapsed;
                        invoice.Visibility = Visibility.Visible;
                        ptrins.Visibility = Visibility.Visible;
                        tname.Text = namec.Text;
                        tsurname.Text = surnamec.Text;
                        tfilmname.Text = sfilm.Text;
                        tseats.ItemsSource = sst;
                        tsalonnumber.Text = ssession.SelectedValue.ToString();
                        tfilmdate.Text = getdate.Text;
                        tfilmsession.Text = ssession.Text;
                        taccount.Text = account.Text;
                        tcancelcode.Text = canc.ToString();
                        tprocess.Text = DateTime.Now.ToString("yyyy-MM-dd" + " - " + "HH:mm");
                    }
                }
                MessageBox.Show("Congrulations You Got Ticket..!", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                namec.Text = "Name";
                surnamec.Text = "Surname";
                citizenshipc.Text = "Citizenship Number";
                student.Text = "0";
                full.Text = "0";
                studenty = 0;
                fully = 0;
                account.Text = "0";
                sfilm.SelectedIndex = -1;
                ssession.SelectedIndex = -1;
                a.Children.Clear();
            }
            else
            {
                MessageBox.Show("Please Fill In The Required Fields..!","Information..!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        private void Splus_Click(object sender, RoutedEventArgs e)
        {
            studenty++;
            student.Text = studenty.ToString();
            if (Convert.ToInt16(full.Text) + Convert.ToInt16(student.Text) == Convert.ToInt16(selectedseat.Text))
            {
                splus.IsEnabled = false;
                fplus.IsEnabled = false;
            }
            account.Text = (sprice + Convert.ToDouble(account.Text)).ToString();
            account.Text = (Convert.ToDouble(account.Text) + is3d).ToString();
        }
        private void Fplus_Click(object sender, RoutedEventArgs e)
        {
            fully++;
            full.Text = fully.ToString();
            if ((Convert.ToInt16(full.Text) + Convert.ToInt16(student.Text)) == Convert.ToInt16(selectedseat.Text))
            {
                fplus.IsEnabled = false;
                splus.IsEnabled = false;
            }
            account.Text = (fprice + Convert.ToDouble(account.Text)).ToString();
            account.Text = (Convert.ToDouble(account.Text) + is3d).ToString();
        }
        private void Sminus_Click(object sender, RoutedEventArgs e)
        {
            studenty--;
            student.Text = studenty.ToString();
            if (Convert.ToInt16(full.Text) + Convert.ToInt16(student.Text) != Convert.ToInt16(selectedseat.Text))
            {
                splus.IsEnabled = true;
                fplus.IsEnabled = true;
            }
            account.Text = (Convert.ToDouble(account.Text) - sprice).ToString();
            account.Text = (Convert.ToDouble(account.Text) - is3d).ToString();
        }
        private void Fminus_Click(object sender, RoutedEventArgs e)
        {
            fully--;
            full.Text = fully.ToString();
            if ((Convert.ToInt16(full.Text) + Convert.ToInt16(student.Text)) != Convert.ToInt16(selectedseat.Text))
            {
                fplus.IsEnabled = true;
                splus.IsEnabled = true;
            }
            account.Text = (Convert.ToDouble(account.Text) - fprice).ToString();
            account.Text = (Convert.ToDouble(account.Text) - is3d).ToString();
        }

        private void namec_MouseEnter(object sender, MouseEventArgs e)
        {
            if (namec.Text == "Name")
            {
                namec.Text = "";
            }
        }

        private void namec_MouseLeave(object sender, MouseEventArgs e)
        {
            if (namec.Text == "" || namec.Text == null)
            {
                namec.Text = "Name";
            }
        }

        private void surnamec_MouseEnter(object sender, MouseEventArgs e)
        {
            if (surnamec.Text == "Surname")
            {
                surnamec.Text = "";
            }
        }

        private void surnamec_MouseLeave(object sender, MouseEventArgs e)
        {
            if (surnamec.Text == "" || namec.Text == null)
            {
                surnamec.Text = "Surname";
            }
        }

        private void citizenshipc_MouseEnter(object sender, MouseEventArgs e)
        {
            if (citizenshipc.Text == "Citizenship Number")
            {
                citizenshipc.Text = "";
            }
        }

        private void citizenshipc_MouseLeave(object sender, MouseEventArgs e)
        {
            if (citizenshipc.Text == "" || namec.Text == null)
            {
                citizenshipc.Text = "Citizenship Number";
            }
        }
        private void ssession_DropDownClosed(object sender, EventArgs e)
        {
            if (Convert.ToInt16(ssession.SelectedValue) != -1 && ssession.SelectedValue != null)
            {
                a.Children.Clear();
                List<string> seatnamez = new List<string>();
                List<int> seatstatez = new List<int>();
                SqlCommand cmd = new SqlCommand("select seatname,seatstate from seatz where salonid=@sid and howsestime=@hstime and sessiondate=@sedate", ActiveConnection);
                cmd.Parameters.Add("@sid", SqlDbType.Int).Value = ssession.SelectedValue;
                cmd.Parameters.Add("@hstime", SqlDbType.VarChar).Value = ssession.Text;
                cmd.Parameters.Add("@sedate", SqlDbType.Date).Value = getdate.Text;
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
                    btn.Content = n;
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
                        btn.IsEnabled = false;
                        btn.Content = "OOS";
                        btn.Tag = "outofservice";
                    }
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

        private void getdate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            ssession.ItemsSource = null;
            a.Children.Clear();
            SqlConnection ActiveConnection = new SqlConnection("Server=79.123.228.28,5501;Database=Gunduz_HAcku;User Id=hacku;Password=x7a45tt55;");
            SqlDataAdapter da = new SqlDataAdapter("Select moviename FROM moviez where @gdate between movieetvision and movieqtvision", ActiveConnection);
            da.SelectCommand.Parameters.AddWithValue("@gdate",getdate.Text);
            DataSet ds = new DataSet();
            da.Fill(ds, "moviez");
            sfilm.ItemsSource = ds.Tables[0].DefaultView;
            sfilm.DisplayMemberPath = ds.Tables[0].Columns["moviename"].ToString();
            da.Dispose();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt16(student.Text) + Convert.ToInt16(full.Text) < Convert.ToInt16(selectedseat.Text))
            {
                MessageBox.Show("The Number Of Seats You Choose Can't Be Smaller Than The Specified Number Of Seats.", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Convert.ToInt16(student.Text) + Convert.ToInt16(full.Text) > Convert.ToInt16(selectedseat.Text))
            {
                MessageBox.Show("The Number Of Seats You Choose Can't Be Greater Than The Specified Number Of Seats.", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Convert.ToInt16(selectedseat.Text) == Convert.ToInt16(full.Text) + Convert.ToInt16(student.Text) && namec.Text != "" && surnamec.Text != "" && citizenshipc.Text != "" && namec.Text != "Name" && surnamec.Text != "Surname" && citizenshipc.Text != "Citizenship Number")
            {
                Random rastgele = new Random();
                string sayilar = citizenshipc.Text;
                string uret = "";
                for (int iiq = 0; iiq < 7; iiq++)
                {
                    uret += sayilar[rastgele.Next(sayilar.Length)];
                }
                int iazq;
                SqlCommand cmdaq = new SqlCommand("select max(id) from SelledTicketInf", ActiveConnection);
                ActiveConnection.Open();
                SqlDataReader rq = cmdaq.ExecuteReader();
                if (rq.Read())
                {
                    iazq = (int)rq[0];
                    uret += iazq;
                }
                rq.Close();
                cmdaq.Dispose();
                ActiveConnection.Close();
                foreach (Object x in a.Children)
                {
                    Button y = (Button)x;
                    if (y.Tag == "selectedseat")
                    {
                        List<string> sseatzx = new List<string>();
                        sseatzx.Add(y.Content.ToString());
                        for (int i = 0; i < sseatzx.Count; i++)
                        {
                            SqlCommand cmd = new SqlCommand("INSERT INTO SelledTicketInf (custname,custsurname,custcitnumber,seatname,salnumber," +
                            "sestime,movname,movdate,stutic,fulltic,cancticked) VALUES (@custname,@custsurname,@custcitnumber,@seatname,@salnumber,@sestime" +
                            ",@movname,@movdate,@stutic,@fulltic,@cancticked)", ActiveConnection);
                            if (ActiveConnection.State == ConnectionState.Closed)
                            {
                                ActiveConnection.Open();
                            }
                            cmd.Parameters.AddWithValue("@custname", namec.Text);
                            cmd.Parameters.AddWithValue("@custsurname", surnamec.Text);
                            cmd.Parameters.AddWithValue("@custcitnumber", citizenshipc.Text);
                            cmd.Parameters.AddWithValue("@seatname", sseatzx[i]);
                            cmd.Parameters.AddWithValue("@salnumber", ssession.SelectedValue);
                            cmd.Parameters.AddWithValue("@sestime", ssession.Text);
                            cmd.Parameters.AddWithValue("@movname", sfilm.Text);
                            cmd.Parameters.AddWithValue("@movdate", getdate.Text);
                            cmd.Parameters.AddWithValue("@stutic", student.Text);
                            cmd.Parameters.AddWithValue("@fulltic", full.Text);
                            cmd.Parameters.AddWithValue("@cancticked", uret);
                            cmd.ExecuteNonQuery();
                            cmd = new SqlCommand("update seatz set seatstate=2 where salonid=@sid and seatname=@sname and howsestime=@hstime", ActiveConnection);
                            cmd.Parameters.AddWithValue("@sid", ssession.SelectedValue);
                            cmd.Parameters.AddWithValue("@sname", sseatzx[i]);
                            cmd.Parameters.AddWithValue("@hstime", ssession.Text);
                            cmd.ExecuteNonQuery();
                            ActiveConnection.Close();
                            cmd.Dispose();
                        }
                    }
                }
                SqlCommand cmdct = new SqlCommand("select top 1 cancticked from SelledTicketInf order by id desc", ActiveConnection);
                ActiveConnection.Open();
                SqlDataReader drct = cmdct.ExecuteReader();
                long canc = 0;
                List<string> sst = new List<string>();
                if (drct.Read())
                {
                    canc = Convert.ToInt64(drct[0]);
                }
                ActiveConnection.Close();
                foreach (Button ss in a.Children)
                {
                    if (ss.Tag == "selectedseat")
                    {
                        sst.Add(ss.Content.ToString() + " |");

                    }
                }
                string ls = sst.Last();
                if (ls.EndsWith("|"))
                {
                    ls = ls.Substring(0, ls.Length - 1);
                }
                sst.Remove(sst.Last());
                sst.Add(ls);
                mi3d.Visibility = Visibility.Collapsed;
                a1.Visibility = Visibility.Collapsed;
                a2.Visibility = Visibility.Collapsed;
                a3.Visibility = Visibility.Collapsed;
                seats.Visibility = Visibility.Collapsed;
                invoice.Visibility = Visibility.Visible;
                ptrins.Visibility = Visibility.Visible;
                cinf.Visibility = Visibility.Visible;
                tname.Text = namec.Text;
                tsurname.Text = surnamec.Text;
                tfilmname.Text = sfilm.Text;
                tseats.ItemsSource = sst;
                tsalonnumber.Text = ssession.SelectedValue.ToString();
                tfilmdate.Text = getdate.Text;
                tfilmsession.Text = ssession.Text;
                taccount.Text = account.Text;
                tcancelcode.Text = canc.ToString();
                tprocess.Text = DateTime.Now.ToString("yyyy-MM-dd" + " - " + "HH:mm");
                tcan.FontSize = 10;
                tcan.Text = "Confirm Reservation or Cancel Code: ";
                MessageBox.Show("Congrulations You Got Reservation..!", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                ActiveConnection.Close();
                namec.Text = "Name";
                surnamec.Text = "Surname";
                citizenshipc.Text = "Citizenship Number";
                student.Text = "0";
                full.Text = "0";
                studenty = 0;
                fully = 0;
                account.Text = "0";
                sfilm.SelectedIndex = -1;
                ssession.SelectedIndex = -1;
                a.Children.Clear();
            }
            
            else
            {
                MessageBox.Show("Please Fill In The Required Fields..!","Information..!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void tprint_Click(object sender, RoutedEventArgs e)
        {
            ptrins.Visibility = Visibility.Collapsed;
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(invoice, "Asil Cinema Ticket Information");
                a1.Visibility = Visibility.Visible;
                a2.Visibility = Visibility.Visible;
                a3.Visibility = Visibility.Visible;
                seats.Visibility = Visibility.Visible;
                invoice.Visibility = Visibility.Collapsed;
                ptrins.Visibility = Visibility.Collapsed;
                cinf.Visibility = Visibility.Collapsed;
            }
            else
            {
                a1.Visibility = Visibility.Visible;
                a2.Visibility = Visibility.Visible;
                a3.Visibility = Visibility.Visible;
                seats.Visibility = Visibility.Visible;
                invoice.Visibility = Visibility.Collapsed;
                ptrins.Visibility = Visibility.Collapsed;
                cinf.Visibility = Visibility.Collapsed;
            }
        }

        private void sfilm_DropDownClosed(object sender, EventArgs e)
        {
            a.Children.Clear();
            SqlConnection ActiveConnection = new SqlConnection("Server=79.123.228.28,5501;Database=Gunduz_HAcku;User Id=hacku;Password=x7a45tt55;");
            SqlDataAdapter da = new SqlDataAdapter("Select sessiontime,salonnumber FROM sessionz where  sessiontime>=(SELECT CONVERT(VARCHAR(5),getdate(),108)) and sessiondate=@gdate and onsesfilmname=@sflm order by sessiontime asc", ActiveConnection);
            da.SelectCommand.Parameters.AddWithValue("@gdate",getdate.Text);
            da.SelectCommand.Parameters.AddWithValue("@sflm",sfilm.Text);
            DataSet ds = new DataSet();
            da.Fill(ds, "sessionz");
            ssession.ItemsSource = ds.Tables[0].DefaultView;
            ssession.DisplayMemberPath = ds.Tables[0].Columns["sessiontime"].ToString();
            ssession.SelectedValuePath = ds.Tables[0].Columns["salonnumber"].ToString();
            da.Dispose();
            SqlCommand cmd = new SqlCommand("select * from moviez where moviename=@sflm", ActiveConnection);
            cmd.Parameters.AddWithValue("@sflm",sfilm.Text);
            ActiveConnection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                sprice = Convert.ToDouble(dr["moviepricestudent"]);
                fprice = Convert.ToDouble(dr["moviepricefull"]);
            }
            da.Dispose();
            ds.Dispose();
            ActiveConnection.Close();
            DateTime dt = Convert.ToDateTime(DateTime.Now.ToString("yyyy - MM - dd"));
            if (getdate.SelectedDate > dt)
            {
                a.Children.Clear();
                ActiveConnection.Open();
                da = new SqlDataAdapter("Select sessiontime,salonnumber FROM sessionz where sessiondate=@gdate and onsesfilmname=@sflm order by sessiontime asc", ActiveConnection);
                da.SelectCommand.Parameters.AddWithValue("@gdate",getdate.Text);
                da.SelectCommand.Parameters.AddWithValue("@sflm",sfilm.Text);
                ds = new DataSet();
                da.Fill(ds, "sessionz");
                ssession.ItemsSource = ds.Tables[0].DefaultView;
                ssession.DisplayMemberPath = ds.Tables[0].Columns["sessiontime"].ToString();
                ssession.SelectedValuePath = ds.Tables[0].Columns["salonnumber"].ToString();
                da.Dispose();
                ActiveConnection.Close();
            }
            SqlCommand cmz = new SqlCommand("select movieis3d,moviepricefull,moviepricestudent from moviez where moviename=@sflm", ActiveConnection);
            cmz.Parameters.AddWithValue("@sflm",sfilm.Text);
            ActiveConnection.Open();
            SqlDataReader dq = cmz.ExecuteReader();

            if (dq.Read())
            {
                sprc.Content = Convert.ToDouble(dq["moviepricestudent"]);
                fprc.Content = Convert.ToDouble(dq["moviepricefull"]);
                if (sfilm.Text == "" || sfilm.Text == null)
                {
                    mi3d.Text = "";
                }
                int i = 0;
                i = Convert.ToInt32(dq["movieis3d"]);
                if (i == 0)
                {
                    is3d = 0;
                    mi3d.Text = "";
                }
                else
                {
                    is3d = 7;
                    mi3d.Text = "This Movie is 3D and Will Be Reflected At 7 $ Per Ticket";
                }
            }
            ActiveConnection.Close();
        }
    }
}
