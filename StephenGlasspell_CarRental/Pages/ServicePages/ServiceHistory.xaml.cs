using System;
using System.Collections.Generic;
using System.Data;
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

namespace StephenGlasspell_CarRental
{
    /// <summary>
    /// Interaction logic for ServiceHistory.xaml
    /// </summary>
    public partial class ServiceHistory : Page
    {
        private string VehicleVIN;

        public ServiceHistory(string VIN)
        {
            VehicleVIN = VIN;
            InitializeComponent();
            displayAllServiceRecords();
        }

        private void displayAllServiceRecords()
        {
           DataSet d = Database.getInstance().customSQL("SELECT * FROM Service WHERE VehicleVIN = '"+VehicleVIN+"' ORDER BY ServiceDate ASC ");

            String strServiceID = "", strNotes = "", strTotalBill = "", strServiceDate = "", strReturnDate = "";


            foreach(DataTable table in d.Tables)
            {
                foreach(DataRow row in table.Rows)
                {
                    foreach(DataColumn column in table.Columns)
                    {
                        if(column.ColumnName == "ServiceID")
                        {
                            strServiceID = row[column].ToString();
                        }
                        if (column.ColumnName == "Notes")
                        {
                            strNotes = row[column].ToString();
                        }
                        if (column.ColumnName == "TotalBill")
                        {
                            strTotalBill = row[column].ToString();
                        }
                        if (column.ColumnName == "ServiceDate")
                        {
                            DateTime date = new DateTime();
                            if (DateTime.TryParse(row[column].ToString(), out date))
                            {
                                strServiceDate = date.ToString("dd MMM yyyy HH:mm");
                            }

                        }
                        if (column.ColumnName == "ReturnDate")
                        {

                            DateTime date = new DateTime();
                            if (DateTime.TryParse(row[column].ToString(), out date))
                            {
                                strReturnDate = date.ToString("dd MMM yyyy HH:mm");
                            }
                        }
                    }
                    String dates ="Service Date : \n"+ strServiceDate + "\n" +"Return Date :\n"+ strReturnDate;
                    
                    addResult(strServiceID, dates, strNotes, strTotalBill);
                }
            }
        }

        private void addResults(String[] s)
        {
            if (s.Length == 0)
            {
                return;
            }

            String[] t = new String[5];

            for (int i = 0; i < t.Length; i++)
            {
                if (s.Length > i)
                {
                    t[i] = s[i];
                }
                else
                {
                    t[i] = "";
                }
            }


            addResult(t[0], t[1], t[2], t[3]);


        }

        private void addResult(String ServiceID, String dates, String notes, String totalBill)
        {
            Button resultButton = new Button();
            resultButton.Name = "ID_" + ServiceID;
            Style bst = FindResource("CustomerSearchButton") as Style;
            resultButton.Style = bst;
            

            TextBlock tbServiceID = new TextBlock();
            TextBlock tbdates = new TextBlock();
            TextBlock tbNotes = new TextBlock();
            TextBlock tbTotalBill = new TextBlock();

            Style st = FindResource("CustomerSearchField") as Style;

            tbServiceID.Style = st;
            tbServiceID.Width = 75;
            tbServiceID.Text = ServiceID;
            tbServiceID.Padding = new Thickness(15, 2, 15, 2);

            tbdates.Style = st;
            tbdates.Width = 220;
            tbdates.Text = dates;
            tbdates.HorizontalAlignment = HorizontalAlignment.Left;
            tbdates.Padding = new Thickness(15, 2, 15, 2);

            tbNotes.Style = st;
            tbNotes.Width = 430;
            tbNotes.Text = notes;
            tbNotes.Padding = new Thickness(15, 2, 15, 2);

            tbTotalBill.Style = st;
            tbTotalBill.Width = 150;
            tbTotalBill.Text = totalBill;
            tbTotalBill.Padding = new Thickness(15, 2, 15, 2);

            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            stack.Children.Add(tbServiceID);
            stack.Children.Add(tbdates);
            stack.Children.Add(tbNotes);
            stack.Children.Add(tbTotalBill);
            resultButton.Content = stack;
          //  resultButton.Click += resultButton_Click;

            spHeaderRow.Visibility = Visibility.Visible;
            spResultsPanel.Children.Add(resultButton);
        }

    }
}
