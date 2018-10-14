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

/*
 *  Programmer  : Stephen Glasspell J381116@tafe.wa.edu.au
 *  Date        : Sept/ Oct 2018
 *  Purpose     : Car Rental Database Application.
 *                This is an application for use in the offices of a Vehicle Hire company
 *                to be used to handle the administration of the business,
 *                including bookings, customer data, vehicle data and employee data.
 *                Secondary tools such as financial reporting and calendar views
 *                can be added to make all business tasks easier for the business.
 *                
 *  Version     : Draft.           
 *  
 *  Document    : Displays all bookings for a selected customer.
 *              Allows user to select a bookings and edit details.
 */

namespace StephenGlasspell_CarRental
{
    /// <summary>
    /// Interaction logic for CustomerBookings.xaml
    /// </summary>
    public partial class CustomerBookings : Page
    {
        int CustomerID;
        String CustomerName;

        public CustomerBookings(int n)
        {
            CustomerID = n;
            InitializeComponent();
            showAllBookingsForCustomer(CustomerID);

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = DataDelegate.previousCursor;

        }

        // Method to get all the bookings in the database for the selected customer.
        private void showAllBookingsForCustomer(int custID)
        {
            

            DataSet d = Database.getInstance().customSQL("SELECT T1.BookingID, " +
                                            "T1.TotalBill, "+
                                            "T2.Title, " +
                                            "T2.FirstName, " +
                                            "T2.LastName, " +
                                            "T3.HouseNumber, " +
                                            "T3.StreetName, " +
                                            "T3.Suburb, " +
                                            "T3.City, " +
                                            "T3.State, " +
                                            "T3.Postcode, " +
                                            "T3.MobilePhone, " +
                                            "T3.Email, " +
                                            "T4.Manufacturer, " +
                                            "T4.Model, " +
                                            "T4.VehicleVIN, " +
                                            "T1.ScheduledHireBeginDateTime, " +
                                            "T1.ScheduledHireReturnDateTime, " +
                                            "T1.ActualHireBeginDateTime, " +
                                            "T1.ActualHireReturnDateTime, " +
                                            "T1.EmployeeID, " +
                                            "T5.FirstName, " +
                                            "T5.LastName, " +
                                            "T5.JobTitle " +
                                        //    "(SELECT Sum(Amount) FROM Payment Where PaymentID = BookingID) as 'PAID' " +
                                            "FROM Booking as T1, " +
                                            "Customer as T2, " +
                                            "CustomerContact as T3, " +
                                            "Vehicle as T4, " +
                                            "Employee as T5 " +
                                          //  "Payment as T6 "+
                                            "WHERE " +
                                            "T1.EmployeeID = T5.EmployeeID " +
                                            "and T1.CustomerID = t2.CustomerID " +
                                            "and T1.CustomerID = T3.CustomerID " +
                                            "and T1.VehicleVIN = T4.VehicleVIN " +
                                            "and T1.CustomerID = '"+custID+"' " +                                           
                                           "ORDER BY T1.ScheduledHireBeginDateTime ASC");
            if (d.Tables[0].Rows.Count == 0)
            {
                TextBlock txtResultsNotFound = new TextBlock();
                txtResultsNotFound.Text = "No records found for these search criteria.";
                txtResultsNotFound.FontSize = 20;
                txtResultsNotFound.HorizontalAlignment = HorizontalAlignment.Center;
                txtResultsNotFound.Margin = new Thickness(30);
                txtResultsNotFound.FontWeight = FontWeights.Bold;
                spResultsPanel.Children.Add(txtResultsNotFound);
            }
            else
            {
                addResults(d);
            }




        }


        // Method to add results from database query to screen. 
        // Takes a Dataset as an argument.
        private void addResults(DataSet d)
        {
            String
                       strBookingID = "",
                       strTitle = "",
                   strFirstName = "",
                   strLastName = "",
                   strHouseNumber = "",
                   strStreetName = "",
                   strSuburb = "",
                   strCity = "",
                   strState = "",
                   strPostcode = "",
                   strMobilePhone = "",
                   strEmail = "",
                   strManufacturer = "",
                   strModel = "",
                   strVehicleVIN = "",
                   strScheduledHireBeginDateTime = "",
                   strScheduledHireReturnDateTime = "",
                   strActualHireBeginDateTime = "",
                   strActualHireReturnDateTime = "",
                   strEmployeeID = "",
                   strJobTitle = "",
                   strPaid = "",
                     strTotalBill = "";

            foreach (DataTable table in d.Tables)
            {
                foreach (DataRow row in table.Rows)
                {

                    foreach (DataColumn column in table.Columns)
                    {
                        if (column.ColumnName == "BookingID")
                        {
                            strBookingID = row[column].ToString();
                        }
                        if (column.ColumnName == "Title")
                        {
                            strTitle = row[column].ToString();
                        }
                        if (column.ColumnName == "FirstName")
                        {
                            strFirstName = row[column].ToString();
                        }
                        if (column.ColumnName == "LastName")
                        {
                            strLastName = row[column].ToString();
                        }
                        if (column.ColumnName == "HouseNumber")
                        {
                            strHouseNumber = row[column].ToString();
                        }
                        if (column.ColumnName == "StreetName")
                        {
                            strStreetName = row[column].ToString();
                        }
                        if (column.ColumnName == "Suburb")
                        {
                            strSuburb = row[column].ToString();
                        }
                        if (column.ColumnName == "City")
                        {
                            strCity = row[column].ToString();
                        }
                        if (column.ColumnName == "State")
                        {
                            strState = row[column].ToString();
                        }
                        if (column.ColumnName == "Postcode")
                        {
                            strPostcode = row[column].ToString();
                        }
                        if (column.ColumnName == "MobilePhone")
                        {
                            strMobilePhone = row[column].ToString();
                        }
                        if (column.ColumnName == "Email")
                        {
                            strEmail = row[column].ToString();
                        }
                        if (column.ColumnName == "Manufacturer")
                        {
                            strManufacturer = row[column].ToString();
                        }
                        if (column.ColumnName == "Model")
                        {
                            strModel = row[column].ToString();
                        }
                        if (column.ColumnName == "VehicleVIN")
                        {
                            strVehicleVIN = row[column].ToString();
                        }
                        if (column.ColumnName == "ScheduledHireBeginDateTime")
                        {
                            String date = row[column].ToString();
                            if (!String.IsNullOrEmpty(date))
                            {
                                DateTime dateTime;
                                DateTime.TryParse(row[column].ToString(), out dateTime);
                                strScheduledHireBeginDateTime = dateTime.ToString("dd/MM/yyyy HH:mm");
                            }
                        }
                        if (column.ColumnName == "ScheduledHireReturnDateTime")
                        {
                            String date = row[column].ToString();
                            if (!String.IsNullOrEmpty(date))
                            {
                                DateTime dateTime;
                                DateTime.TryParse(row[column].ToString(), out dateTime);
                                strScheduledHireReturnDateTime = dateTime.ToString("dd/MM/yyyy HH:mm");
                            }
                        }
                        if (column.ColumnName == "ActualHireBeginDateTime")
                        {
                            String date = row[column].ToString();
                            if (!String.IsNullOrEmpty(date))
                            {
                                DateTime dateTime;
                                DateTime.TryParse(row[column].ToString(), out dateTime);
                                strActualHireBeginDateTime = dateTime.ToString("dd/MM/yyyy HH:mm");
                            }

                        }
                        if (column.ColumnName == "ActualHireReturnDateTime")
                        {
                            String date = row[column].ToString();
                            if (!String.IsNullOrEmpty(date))
                            {
                                DateTime dateTime;
                                DateTime.TryParse(row[column].ToString(), out dateTime);
                                strActualHireReturnDateTime = dateTime.ToString("dd/MM/yyyy HH:mm");
                            }


                        }
                        if (column.ColumnName == "EmployeeID")
                        {
                            strEmployeeID = row[column].ToString();
                        }
                        if (column.ColumnName == "JobTitle")
                        {
                            strJobTitle = row[column].ToString();
                        }
                        if(column.ColumnName == "PAID")
                        {
                            double paid = 0;
                            Double.TryParse( row[column].ToString(),out paid );
                            strPaid = paid.ToString("C");
                            
                        }
                        if (column.ColumnName == "TotalBill")
                        {
                            double bill = 0;
                            Double.TryParse(row[column].ToString(), out bill);
                            strTotalBill = bill.ToString("C");
                        }
                    }

                    String fullName = strTitle + " " + strFirstName + " " + strLastName;
                    CustomerName = fullName;
                    String vehicle = strManufacturer + " " + strModel + "\n" + strVehicleVIN;
                    String dates = strScheduledHireBeginDateTime + " -\n" + strScheduledHireReturnDateTime;
                    String collected = strActualHireBeginDateTime;
                    String payment = "Billing Amount : " + strTotalBill + "\nPAID :" + strPaid; ;
                    addResult(strBookingID, payment, vehicle, dates, collected);

                }
            }


        }

        // Method to display results from a database query as a string array.
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


            addResult(t[0], t[1], t[2], t[3], t[4]);


        }

        //Method to display a single result from a database call.
        private void addResult(String bookingID, String payment, String vehicle, String dates, String collected)
        {

            Button resultButton = new Button();
            resultButton.Name = "ID_" + bookingID;

            Style bst = FindResource("CustomerSearchButton") as Style;
            resultButton.Style = bst;
            resultButton.ToolTip = "Click to Edit Booking Details.";

            TextBlock tbCustomer = new TextBlock();
            TextBlock tbVehicle = new TextBlock();
            TextBlock tbDates = new TextBlock();
            TextBlock tbCollected = new TextBlock();

            if (!String.IsNullOrEmpty(collected))
            {
                tbCustomer.Background = Brushes.LightGreen;
                tbVehicle.Background = Brushes.LightGreen;
                tbDates.Background = Brushes.LightGreen;
                tbCollected.Background = Brushes.LightGreen;
            }

            Style st = FindResource("CustomerSearchField") as Style;

            tbCustomer.Style = st;
            tbCustomer.Width = 250;
            tbCustomer.Text = payment;
            tbCustomer.Padding = new Thickness(15, 2, 15, 2);

            tbVehicle.Style = st;
            tbVehicle.Width = 250;
            tbVehicle.Text = vehicle;
            tbVehicle.Padding = new Thickness(15, 2, 15, 2);

            tbDates.Style = st;
            tbDates.Width = 175;
            tbDates.Text = dates;
            tbDates.Padding = new Thickness(15, 2, 15, 2);

            tbCollected.Style = st;
            tbCollected.Width = 175;
            tbCollected.Text = collected;
            tbCollected.Padding = new Thickness(15, 2, 15, 2);

            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            stack.Children.Add(tbCustomer);
            stack.Children.Add(tbVehicle);
            stack.Children.Add(tbDates);
            stack.Children.Add(tbCollected);
            resultButton.Content = stack;
            resultButton.Click += resultButton_Click;

            spHeaderRow.Visibility = Visibility.Visible;
            spResultsPanel.Children.Add(resultButton);
        }

        void resultButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int length = button.Name.Length - 3;
            String name = button.Name.Substring(3, length);
            int BookingID = int.Parse(name);
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new BookingsEdit(BookingID));
        }

        private void btnCreateNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new CustomersNew());
        }

        private void btnNewBooking_Click(object sender, RoutedEventArgs e)
        {
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new BookingsNew(CustomerID, CustomerName));
        }
    }
}
