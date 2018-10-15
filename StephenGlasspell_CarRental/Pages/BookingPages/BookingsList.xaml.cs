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
 *  Document    : This page is to show a list of current bookings.
 *                The page displays bookings by date.
 *                When the page loads, it shows bookings for the current date.
 *                Two buttons left and right of a textbox which displays the selected date allow the
 *                user to navigate forward or backward by days.
 *                Bookings are displayed in order of their collection time, to show the business users the
 *                sequence in which the cars are expected to be collected.
 */

namespace StephenGlasspell_CarRental
{
    /// <summary>
    /// Interaction logic for BookingsList.xaml
    /// </summary>
    public partial class BookingsList : Page
    {
        // Set today's date and set it to midnight, for easy searching. Page scope variable accessible across all methods.
        DateTime selectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

        public BookingsList()
        {
            InitializeComponent();
            // Show the selected date on the form.
            lblDate.Text = selectedDate.Date.ToString("dd MMMM yyyy");

            // Display all the bookings for today.
            showAllBookingsForDate(selectedDate);
           
        }

        // Method to set the Mouse Cursor back to normal.
        // It may have been set to busy if there are long wait times for the database.
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            
        }

        // Method to display all bookings for a selected date on the page.
        private void showAllBookingsForDate(DateTime selectedDate)
        {
            // Make strings for the selected date and the day after that.
            String strSelectedDate = selectedDate.ToString("yyyy-MM-dd");
            String strSelectedDate2 = selectedDate.AddDays(1).ToString("yyyy-MM-dd");

            // Send query to database.
            DataSet d = Database.getInstance().customSQL("SELECT T1.BookingID, " +
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
                                            "FROM Booking as T1, " +
                                            "Customer as T2, " +
                                            "CustomerContact as T3, " +
                                            "Vehicle as T4, " +
                                            "Employee as T5 " +
                                            "WHERE T1.ScheduledHireReturnDateTime > '" + strSelectedDate + "' " +
                                            "and T1.ScheduledHireBeginDateTime <  '" + strSelectedDate2 + "' " +
                                            "and T1.EmployeeID = T5.EmployeeID " +
                                            "and T1.CustomerID = t2.CustomerID " +
                                            "and T1.CustomerID = T3.CustomerID " +
                                            "and T1.VehicleVIN = T4.VehicleVIN");

            // If no results have been found, display a message on the page.
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
            else  // If results have been found.
            {
                // Add the results to the page.
                addResults(d);
            }
        }

        // Method to display all bookings between two dates.
        // Similar to previous method.
        private void showAllBookingsForDates(DateTime dateFrom, DateTime dateTo)
        {
            String strSelectedDate = dateFrom.ToString("yyyy-MM-dd");
            String strSelectedDate2 = dateTo.ToString("yyyy-MM-dd");

            DataSet d = Database.getInstance().customSQL("SELECT T1.BookingID, " +
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
                                            "FROM Booking as T1, " +
                                            "Customer as T2, " +
                                            "CustomerContact as T3, " +
                                            "Vehicle as T4, " +
                                            "Employee as T5 " +
                                            "WHERE T1.ScheduledHireReturnDateTime >  '" + strSelectedDate + "' " +
                                            "and T1.ScheduledHireBeginDateTime < '" + strSelectedDate2 + "' " +
                                            "and T1.EmployeeID = T5.EmployeeID " +
                                            "and T1.CustomerID = t2.CustomerID " +
                                            "and T1.CustomerID = T3.CustomerID " +
                                            "and T1.VehicleVIN = T4.VehicleVIN " +
                                            "ORDER BY T1.ScheduledHireBeginDateTime");

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

        // Method to handle keypresses in the Search textBox. Searches the database for matches on every keystroke.
        private void SearchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            String inputString = SearchInput.Text;

            // if there's nothing to seach for, stop here.
            if (String.IsNullOrEmpty(inputString))
            {
                return;
            }

            // User shortcut to show all bookings as a list on the page.
            if (inputString == "*")
            {
                showAllBookingsForDates(DateTime.Now.AddYears(-10), DateTime.Now.AddYears(10));

                return;
            }

            // Only handle search terms with 3 or more characters.
            if (inputString.Length < 3)
            {
                spResultsPanel.Children.Clear();
                spHeaderRow.Visibility = Visibility.Collapsed;
                return;
            }

            spResultsPanel.Children.Clear();

            // Find possible matches.
            searchDatabaseForMatches(inputString);
        }

        // Method to easily query the database with a search term.
        void searchDatabaseForMatches(String s)
        {
             spResultsPanel.Children.Clear();

            // Split the search term string into an array of words to check
            String inputString = s;
            String[] inputWords = s.Split(' ');

            // This is an array of fields in the database we will look for a match in.
            String[] fieldsToCheck = new String[] { "T2.FirstName", "T2.LastName",  "T4.Manufacturer", "T4.Model", "T4.VehicleVIN" };

            // Iterate through each word entered into the search bar.
            foreach (String word in inputWords)
            {
                word.Trim();
                if (!String.IsNullOrEmpty(word))
                {
                    // Do multiple calls to the database, checking each word in the searh term against the list of fields to check.
                    foreach (String field in fieldsToCheck)
                    {

                        DataSet d = Database.getInstance().customSQL("SELECT * " +                                 
                                          "FROM Booking as T1, " +
                                          "Customer as T2, " +                                 
                                          "Vehicle as T4 " +                              
                                          "WHERE " +                         
                                          " T1.CustomerID = t2.CustomerID " +
                                          "and T1.VehicleVIN = T4.VehicleVIN  " +
                                          " and " + field + " = '" + word + "';");

                        // Add the results to the page after every call to the database.
                        addResults(d);
                    }
                }
            }
            return;
        }

        // Method accepts a Dataset object and displays the values on the page.
        private void addResults(DataSet d)
        {
            // String variables to hold the results.
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
           strJobTitle = "";

            DateTime scheduledBegin = new DateTime();
            DateTime scheduledReturn = new DateTime();
            DateTime actualBegin = new DateTime();
            DateTime actualReturn = new DateTime();



            foreach (DataTable table in d.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    scheduledBegin = new DateTime();
                    scheduledReturn = new DateTime();
                    actualBegin = new DateTime();
                    actualReturn = new DateTime();

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
                                strScheduledHireBeginDateTime = dateTime.ToString("dd MMM yyyy HH:mm");
                                scheduledBegin = dateTime;
                            }

                        }
                        if (column.ColumnName == "ScheduledHireReturnDateTime")
                        {
                            String date = row[column].ToString();
                            if (!String.IsNullOrEmpty(date))
                            {
                                DateTime dateTime;
                                DateTime.TryParse(row[column].ToString(), out dateTime);
                                strScheduledHireReturnDateTime = dateTime.ToString("dd MMM yyyy HH:mm");
                                scheduledReturn = dateTime;
                            }


                        }
                        if (column.ColumnName == "ActualHireBeginDateTime")
                        {
                            String date = row[column].ToString();
                            if (!String.IsNullOrEmpty(date))
                            {
                                DateTime dateTime;
                                DateTime.TryParse(row[column].ToString(), out dateTime);
                                strActualHireBeginDateTime = dateTime.ToString("dd MMM yyyy HH:mm");
                                actualBegin = dateTime;
                            }

                        }
                        if (column.ColumnName == "ActualHireReturnDateTime")
                        {
                            String date = row[column].ToString();
                            if (!String.IsNullOrEmpty(date))
                            {
                                DateTime dateTime;
                                DateTime.TryParse(row[column].ToString(), out dateTime);
                                strActualHireReturnDateTime = dateTime.ToString("dd MMM yyyy HH:mm");
                                actualReturn = dateTime;
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
                    }
                    // Format the strings to build a display.
                    String fullName = strTitle + " " + strFirstName + " " + strLastName;
                    String address = strHouseNumber + " " + strStreetName + " " + strSuburb + " " + strCity + " " + strState + " " + strPostcode;
                    String vehicle = strManufacturer + " " + strModel + "\n" + strVehicleVIN;
                    String dates = strScheduledHireBeginDateTime + " -\n" + strScheduledHireReturnDateTime;

                    String collected = "";

                    if (!string.IsNullOrEmpty(strActualHireBeginDateTime))
                    {
                        collected = strActualHireBeginDateTime + "-\n" + strActualHireReturnDateTime; 
                    }
                    addResult(strBookingID, fullName, vehicle, dates, collected);
                }
                
            }
        }

        // Method similar to above, but for use with string arrays.
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

        // Method to add a single result to the page.
        // Builds a button with several stackpanels inside, containing text to display on screen.
        private void addResult(String bookingID, String customer, String vehicle, String dates, String collected)
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

            //Get the style from the main app.xaml resource.
            Style st = FindResource("CustomerSearchField") as Style;

            tbCustomer.Style = st;
            tbCustomer.Width = 250;
            tbCustomer.Text = customer;
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

        

        // Method called when the user clicks one of the results onscreen, displayed as a button.
        void resultButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a button object from the sender.
            Button button = (Button)sender;

            // Get the BookingID from the Button Name. 
            // Button names can't start with numbers, so 3 characters have been added by me to get around this.
            // Create a substring from the button name, of just the characters we want, not the characters I added.
            String name = button.Name.Substring(3);

            //  and parse that substring to get the ineteger BookingID.
            int BookingID = int.Parse(name);

            // Then navigate to the BookingsEdit page, using the BookingID as a parameter.
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new BookingsEdit(BookingID));
        }

        // Method that navigates to the Customer New page.
        private void btnCreateNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new CustomersNew());
        }

      

     

        private void btnPreviousDate_Click(object sender, RoutedEventArgs e)
        {
            spResultsPanel.Children.Clear();
            selectedDate = selectedDate.AddDays(-1);
            lblDate.Text = selectedDate.Date.ToString("dd MMMM yyyy");
            showAllBookingsForDate(selectedDate);
            updateHeader();
        }

        private void btnNextDate_Click(object sender, RoutedEventArgs e)
        {
            spResultsPanel.Children.Clear();
            selectedDate = selectedDate.AddDays(+1);
            lblDate.Text = selectedDate.Date.ToString("dd MMMM yyyy");
            showAllBookingsForDate(selectedDate);
            updateHeader();
        }

        private void btnPreviousWeek_Click(object sender, RoutedEventArgs e)
        {
            spResultsPanel.Children.Clear();
            selectedDate = selectedDate.AddDays(-7);
            lblDate.Text = selectedDate.Date.ToString("dd MMMM yyyy");
            showAllBookingsForDate(selectedDate);
            updateHeader();
        }

        private void btnNextWeek_Click(object sender, RoutedEventArgs e)
        {
            spResultsPanel.Children.Clear();
            selectedDate = selectedDate.AddDays(+7);
            lblDate.Text = selectedDate.Date.ToString("dd MMMM yyyy");
            showAllBookingsForDate(selectedDate);
            updateHeader();
        }

        private void btnPreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            spResultsPanel.Children.Clear();
            selectedDate = selectedDate.AddMonths(-1);
            lblDate.Text = selectedDate.Date.ToString("dd MMMM yyyy");
            showAllBookingsForDate(selectedDate);
            updateHeader();
        }

        private void btnNextMonth_Click(object sender, RoutedEventArgs e)
        {
            spResultsPanel.Children.Clear();
            selectedDate = selectedDate.AddMonths(+1);
            lblDate.Text = selectedDate.Date.ToString("dd MMMM yyyy");
            showAllBookingsForDate(selectedDate);
            updateHeader();
        }



        private void btnGoToDay_Click(object sender, RoutedEventArgs e)
        {
            spResultsPanel.Children.Clear();
            selectedDate = DateTime.Now.Date;
            lblDate.Text = selectedDate.Date.ToString("dd MMMM yyyy");
            showAllBookingsForDate(selectedDate);
            updateHeader();
        }

        private void btnNewBooking_Click(object sender, RoutedEventArgs e)
        {
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new CustomerList());
        }

        void updateHeader()
        {
            if(selectedDate.CompareTo(DateTime.Now.Date) < 0)
            {
                hd2Title.Text = "Past Bookings";
            }
            if (selectedDate.CompareTo(DateTime.Now.Date) == 0)
            {
                hd2Title.Text = "Current Bookings";
            }
            if (selectedDate.CompareTo(DateTime.Now.Date) > 0)
            {
                hd2Title.Text = "Future Bookings";
            }
        }
    }
}
