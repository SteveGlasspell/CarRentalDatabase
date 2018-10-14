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
 */

namespace StephenGlasspell_CarRental
{
    /// <summary>
    /// Interaction logic for ServiceNew.xaml
    /// </summary>
    public partial class ServiceNew : Page
    {
        private DateTime serviceDate;
        private string VehicleVIN;

        public ServiceNew()
        {
            InitializeComponent();
        }

        public ServiceNew(DateTime SD, string VIN)
        {
            InitializeComponent();
            serviceDate = SD;
            VehicleVIN = VIN;
            updateDate();
        }

        void updateDate()
        {
            dpServiceDate.SelectedDate = serviceDate;
            txtVehicleVIN.Text = VehicleVIN;
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Find out what kind of control has been changed or had information entered.
            // Parse the data into variabes and hidden text fields.
            if (sender is ComboBox)
            {
                ComboBox c = (ComboBox)sender;

                if (c.Name == "cmbServiceHour")
                {
                    ComboBoxItem cmbHour = c.SelectedItem as ComboBoxItem;
                    int hour = 0;
                    int.TryParse(cmbHour.Content.ToString(), out hour);
                    serviceDate = new DateTime(serviceDate.Year, serviceDate.Month, serviceDate.Day, hour, serviceDate.Minute, serviceDate.Second, serviceDate.Millisecond);
                    
                }
                if (c.Name == "cmbServiceMinute")
                {
                    ComboBoxItem cmbMinute = c.SelectedItem as ComboBoxItem;
                    int minute = 0;
                    int.TryParse(cmbMinute.Content.ToString(), out minute);
                    serviceDate = new DateTime(serviceDate.Year, serviceDate.Month, serviceDate.Day, serviceDate.Hour, minute, serviceDate.Second, serviceDate.Millisecond);
                                    }
            }

            if (sender is DatePicker)
            {
                DatePicker d = (DatePicker)sender;

                if (d.Name == "dpService")
                {
                    int day = dpServiceDate.SelectedDate.Value.Day;
                    int month = dpServiceDate.SelectedDate.Value.Month;
                    int year = dpServiceDate.SelectedDate.Value.Year;

                    serviceDate = new DateTime(year, month, day, serviceDate.Hour, serviceDate.Minute, serviceDate.Second);
            
                }
            }
        }

        // Method to check all the control forms for the Collection date and time.
        // Returns TRUE if the collection date is later than today's current date and time.
        private bool ServiceTimeIsInFuture()
        {
            if (serviceDate.CompareTo(DateTime.Now) >= 0)
            {
                // Recheck that the ComboBoxes and DatePicker selections are valid.
                if ((dpServiceDate.SelectedDate.Value.Date.CompareTo(DateTime.Now.Date) >= 0)
                    && (cmbServiceHour.SelectedIndex > 0)
                    && (cmbServiceMinute.SelectedIndex > 0))
                {

                    return true;
                }

            }

            return false;
        }

        private void btnBookService_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceTimeIsInFuture())
            {
                DateTime returnDate = new DateTime(serviceDate.Year, serviceDate.Month, serviceDate.Day, 17, 0, 0);
                if(serviceDate.Hour > 15)
                {
                    returnDate = returnDate.AddDays(1);
                }

                if (checkDatesForClashes(serviceDate, returnDate, VehicleVIN))
                {
                    MessageBox.Show("Your selected dates clash with other bookings. Please reschedule.","Date Clash Detected");
                    return;

                }

                MessageBoxResult result = MessageBox.Show("Are you happy with these date?\nService Time : " + serviceDate.ToString("yyyy-MM-dd HH:mm") + "\nPickup Time : "+returnDate.ToString("yyyy-MM-dd HH:mm"),"Booking Confirmation",MessageBoxButton.OKCancel,MessageBoxImage.Question);

                if(result == MessageBoxResult.OK)
                {
                 int recordsInserted =  Database.getInstance().insert("Service", new string[] { "VehicleVIN", "ServiceDate", "ReturnDate" }, new string[] { VehicleVIN, serviceDate.ToString("yyyy-MM-dd HH:mm:ss"), returnDate.ToString("yyyy-MM-dd HH:mm:ss") });

                    if(recordsInserted > 0)
                    {
                        CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new ServiceBookingSuccess());
                    }
                }

            }
        }

        private bool checkDatesForClashes(DateTime serviceDate, DateTime returnDate, string VIN)
        {
           DataSet d = Database.getInstance().customSQL("SELECT T1.BookingID FROM Booking as T1 WHERE T1.ScheduledHireBeginDateTime < '"+ returnDate.ToString("yyyy-MM-dd HH:mm:ss")+"' and T1.ScheduledHireReturnDateTime > '"+serviceDate.ToString("yyyy-MM-dd HH:mm:ss")+"' and T1.VehicleVIN = '"+VIN+"'");
            int recordsReturned = d.Tables[0].Rows.Count;

            if(recordsReturned > 0)
            {
                return true;
            }
            return false;

           
        }
    }
}
