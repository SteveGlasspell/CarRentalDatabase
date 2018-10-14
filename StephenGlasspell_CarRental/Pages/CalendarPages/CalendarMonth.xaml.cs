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
 *  Document    : This page is to display the current bookings for the selected vehicle
 *                 so that the user can choose a free date and book a vehicle service in that date.
 *                 The page displays a grid, in the form of a calendar.
 *                 When the form loads, it populates the calendar dates using the current date, 
 *                 to show a complete month.
 *                 To better see which dates are in the current month, those dates have a green background, 
 *                 and any dates belonging to previous or next month will be in yellow.
 *                 The calendar then poplulates the calendar grid with buttons containing details of bookings.
 *                 The buttons can take the user to the selected booking.
 *                 In between these booked dates, a button for Book Service is placed.
 *                 The Book Service Button takes the user to a form where they can book a service for the selected date.
 */

namespace StephenGlasspell_CarRental
{
    /// <summary>
    /// Interaction logic for CalendarMonth.xaml
    /// </summary>
    public partial class CalendarMonth : Page
    {
        // Page scope variables accessible to all methods on the page.
        DateTime selectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
        String VehicleVIN = "";
        List<StackPanel> Dates = new List<StackPanel>();

        // A struct to hold the main booking details for each booking found within the selected dates.
        struct booking
        {
            public int bookingID;
            public string VehicleVIN;
            public DateTime scheduledHireBegin;
            public DateTime scheduledHireReturn;
        }

        // A list to hold the booking structs, as above.
        List<booking> Bookings = new List<booking>();

       
        // Constructor with no parameter.
        // Will be used to show all bookings for all cars.
        public CalendarMonth()
        {
            InitializeComponent();

            // Generate a list of dates in the selected month.
            listDates();
            // Populate the calendar.
            generateCalendarDates();
        }

        // Constructor with selected VehicleVIN
        // Used to show bookings for selected vehicle.
        public CalendarMonth(String VIN)
        {
            VehicleVIN = VIN;
            InitializeComponent();

            // Generate a list of dates in the selected month.
            listDates();
            // Populate the calendar.
            generateCalendarDates();
        }

        // This method puts all the stackpanels created in the calendar into a list, so that
        // the list can be easily iterated over. 
        void listDates()
        {
            Dates.Add(C0R0);
            Dates.Add(C1R0);
            Dates.Add(C2R0);
            Dates.Add(C3R0);
            Dates.Add(C4R0);
            Dates.Add(C5R0);
            Dates.Add(C6R0);

            Dates.Add(C0R1);
            Dates.Add(C1R1);
            Dates.Add(C2R1);
            Dates.Add(C3R1);
            Dates.Add(C4R1);
            Dates.Add(C5R1);
            Dates.Add(C6R1);

            Dates.Add(C0R2);
            Dates.Add(C1R2);
            Dates.Add(C2R2);
            Dates.Add(C3R2);
            Dates.Add(C4R2);
            Dates.Add(C5R2);
            Dates.Add(C6R2);

            Dates.Add(C0R3);
            Dates.Add(C1R3);
            Dates.Add(C2R3);
            Dates.Add(C3R3);
            Dates.Add(C4R3);
            Dates.Add(C5R3);
            Dates.Add(C6R3);

            Dates.Add(C0R4);
            Dates.Add(C1R4);
            Dates.Add(C2R4);
            Dates.Add(C3R4);
            Dates.Add(C4R4);
            Dates.Add(C5R4);
            Dates.Add(C6R4);

            foreach(StackPanel date in Dates)
            {
                date.Orientation = Orientation.Vertical;
            }
        }

        
        void clearCalendar()
        {
            foreach(StackPanel date in Dates)
            {
                date.Children.Clear();
            }
        }

        // Method to work out where the date numbers go in the calendar.
        // And also add the booking details to the calendar.
        void generateCalendarDates()
        {
            clearCalendar();
            Bookings.Clear();
            
            // Display the selected month and year at the top the screen
            txtMonthYear.Text = selectedDate.ToString("MMMM yyyy");

            // This integer is to count the number of days in the week there are
            // at the beginning of the month
            // until we get to the first date of the month.
            // (It shows the end of last month in the calendar)
            int preDays = 0;

            // I'm pretty sure there is a shorter way to do this.
            switch (selectedDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    preDays = 0;
                    break;
                case DayOfWeek.Tuesday:
                    preDays = -1;
                    break;
                case DayOfWeek.Wednesday:
                    preDays = -2;
                    break;
                case DayOfWeek.Thursday:
                    preDays = -3;
                    break;
                case DayOfWeek.Friday:
                    preDays = -4;
                    break;
                case DayOfWeek.Saturday:
                    preDays = -5;
                    break;
                case DayOfWeek.Sunday:
                    preDays = -6;
                    break;
                default:
                    break;
            }

            // Change the selected date to the first Monday on this calendar, 
            // even if it's in the previous month.
            DateTime startDate = selectedDate.AddDays(preDays);

            // SQL call to database. Another insecure CustomSQL Method call.
            // TODO change this to a standard Database.select method call.
            DataSet d = Database.getInstance().customSQL("SELECT T1.BookingID, T1.ScheduledHireBeginDateTime, T1.ScheduledHireReturnDateTime, T1.VehicleVIN FROM Booking as T1 WHERE T1.VehicleVIN = '"+VehicleVIN+"' and T1.ScheduledHireReturnDateTime > '" + startDate.ToString("yyyy-MM-dd") + "' and T1.ScheduledHireBeginDateTime < '" + startDate.AddDays(35).ToString("yyyy-MM-dd") +"'");

            // Iterate throught the results, add the booking details to a booking struct, add that struct to a list.
            foreach( DataTable table in d.Tables)
            {
                foreach(DataRow row in table.Rows)
                {
                    booking book = new booking();

                    foreach(DataColumn column in table.Columns)
                    {
                        if(column.ColumnName == "BookingID")
                        {
                            int.TryParse(row[column].ToString(), out book.bookingID);
                        }
                        if (column.ColumnName == "ScheduledHireBeginDateTime")
                        {
                            DateTime.TryParse(row[column].ToString(), out book.scheduledHireBegin);
                        }
                        if (column.ColumnName == "ScheduledHireReturnDateTime")
                        {
                            DateTime.TryParse(row[column].ToString(), out book.scheduledHireReturn);
                        }
                        if(column.ColumnName == "VehicleVIN")
                        {
                            book.VehicleVIN = row[column].ToString();
                        }
                    }
                    Bookings.Add(book);
                }
            }

          // Iterate through each date on the calendar, starting from the first Monday.
          // (Even if it's for the previous month.)
          // 
            foreach(StackPanel date in Dates)
            {          
                // Show the date in the corner.
                TextBlock t = new TextBlock();
                t.Text = startDate.Date.Day.ToString();
                t.FontSize = 16;
                date.Children.Add(t);

               
                
                // Select the green or yellow background as appropriate.
                if (startDate.Month == selectedDate.Month)
                {
                    date.Background = Brushes.LightGreen;
                }
                else
                {
                    date.Background = Brushes.LightYellow;
                }

                bool bookingadded = false;

                // Check if there is a booking for the selected date.
                foreach (booking book in Bookings)
                {
                    if((startDate.CompareTo(book.scheduledHireReturn) < 0) && (startDate.AddDays(1).CompareTo(book.scheduledHireBegin) > 0))
                    {
           

                        Button b = new Button();
                        b.Content = "BOOKED";
                        b.Foreground = Brushes.White;
                        b.Background = Brushes.IndianRed;
                        b.FontSize = 16;
                        b.Name = "ID"+book.bookingID.ToString();
                        b.Click += booking_click;
                        date.Children.Add(b);
                        bookingadded = true;
                    }
                   
               
                }

                if ((startDate.CompareTo(DateTime.Now) > 0) && (bookingadded == false))
                {
                    Button b = new Button();
                    b.Content = "Book\nService";
                    b.Foreground = Brushes.White;
                    b.Background = Brushes.Green;
                    b.FontSize = 16;

                    b.Name = "ID" + startDate.ToString("yyyyMMdd");
                    b.Click += bookService_click;
                    date.Children.Add(b);
                }



                // Increment the date by one day and move on to the next square on the calendar.
                startDate = startDate.AddDays(1);
            }
        }

        // Method to display the previous month on the calendar.
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            selectedDate = selectedDate.AddMonths(-1);
            generateCalendarDates();
        }

        // Method to display the next month in the calendar.
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            selectedDate = selectedDate.AddMonths(1);
            generateCalendarDates();
        }

        // Method called when a Book Service button is pressed. 
        // Navigates the user to a ServiceDate page
        private void bookService_click(object sender, RoutedEventArgs e)
        {
            String date = "";
            String year = "";
            String month = "";
            String day = "";
            int intYear = 0;
            int intMonth = 0;
            int intDay = 0;
            DateTime serviceDate = new DateTime();
            if (sender is Button)
            {
                Button button = (Button)sender;
                date = button.Name.Substring(2);  // Date is stored in the button name.
                year = date.Substring(0, 4);
                month = date.Substring(4, 2);
                day = date.Substring(6, 2);
                int.TryParse(year, out intYear);
                int.TryParse(month, out intMonth);
                int.TryParse(day, out intDay);
                serviceDate = new DateTime(intYear, intMonth, intDay);
                
            }

            if (serviceDate.CompareTo(DateTime.Now) > 0)
            {
                CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new ServiceNew(serviceDate, VehicleVIN));

            }
            else
            {
                MessageBox.Show("There was a problem parsing the service date.\n" + serviceDate.ToString("yyyy-MM-dd HH:mm:ss")+"\n"+date,"Service Booking Error");
            }
        }

        // Method takes the user to a BookingsEdit to see details of the selected booking.
        private void booking_click(object sender, RoutedEventArgs e)
        {
            int bookingID = 0;
            if (sender is Button)
            {
                Button button = (Button)sender;
                string strBookingID = button.Name.Substring(2);
                int.TryParse(strBookingID, out bookingID);
            }

            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new BookingsEdit(bookingID));
        }
    }
}
