using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Interaction logic for FeedBack.xaml
    /// </summary>
    public partial class FeedBack : UserControl
    {
        public FeedBack()
        {
            InitializeComponent();
            FillTopic();
        }

        private void FillTopic()
        {
            SqlConnection ActiveConnection = new SqlConnection("--");
            SqlDataAdapter da = new SqlDataAdapter("Select * FROM Feedback", ActiveConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "Feedback");
            topc.ItemsSource = ds.Tables[0].DefaultView;
            topc.DisplayMemberPath = ds.Tables[0].Columns["topichead"].ToString();
            topc.SelectedValuePath = ds.Tables[0].Columns["id"].ToString();
            da.Dispose();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(topc.Text != "" && explanation.Text != "")
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new NetworkCredential("asilackux@gmail.com", "--");
                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress("ackuofficial@icloud.com"));
                msg.From = new MailAddress("asilackux@gmail.com");
                msg.Subject = topc.Text;
                msg.IsBodyHtml = true;
                msg.Body = explanation.Text + "<br><br><br>" + "<h3>The Date & Time It Was Sent<h3>" + "<h4>" + DateTime.Now.ToString() + "<h4>";
                client.EnableSsl = true;
                client.Send(msg);
                MessageBox.Show("Thanks For Your Feedback..!", "Information..!", MessageBoxButton.OK, MessageBoxImage.Information);
                topc.SelectedIndex = -1;
                explanation.Text = "";
            }
            else
            {
                MessageBox.Show("You Need The Fill Required Fields..!","Information..!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
