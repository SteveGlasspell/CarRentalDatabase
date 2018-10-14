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
 *  Document    : This is a context sensitive display, just to the right of the main panel.
 *              Most of the time, it will display a picture of a Mercedes,
 *              but it can also show useful information to the user about the current form
 *              or regarding notifications and messages about upcoming collections or overdue items.
 *              
 *              This is currently the last thing I am working on, so the implementation is very basic.
 *              Can be produced to be more functional.
 */

namespace StephenGlasspell_CarRental
{
    /// <summary>
    /// Interaction logic for CommonTasksSidePanel.xaml
    /// </summary>
    public partial class CommonTasksSidePanel : Page
    {
        private static CommonTasksSidePanel instance = null;

        // Is the page showing details or a picture of a car? Details: true. Car: false.
        private bool isPageShowingTodayDetails = false;

        // Simple struct for messages.
        struct message
        {
            public string messageType;
            public string title;
            public string content;
            public DateTime created;
        }

        // A list to hold messages.
        List<message> messages = new List<message>();
        // A list for specific types of messages.
        List<message> collectionsMessages = new List<message>();

        // Constructor
        private CommonTasksSidePanel()
        {
            InitializeComponent();
            
            
        }

        // Return a static instance of this page, using the singleton pattern.
        public static CommonTasksSidePanel getInstance()
        {
            if(instance == null)
            {
                instance = new CommonTasksSidePanel();
            }

            return instance;
        }

        // Placeholder method. Can be used for classes to store error messages and display them on screen.
        // TODO build this.
        public void updateErrorMessages(String message)
        {
            txtErrorMessages.Text = message;
  
          
        }

        // Method to get a list of collection times for today. Alerts user to prepare for customer arrival.
        private void getCollectionTimes()
        {
            collectionsMessages.Clear();

            DataSet d = Database.getInstance().customSQL("SELECT " +
                                    "T1.VehicleVIN, " +
                                    "T1.ScheduledHireBeginDateTime "+
                                    "FROM Booking as T1 " +
                                    "WHERE T1.ScheduledHireBeginDateTime > '" + DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss")+"' " +
                                    "and T1.ScheduledHireBeginDateTime < '" + new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23,59,59).ToString("yyyy-MM-dd HH:mm:ss") +"' " +
                                    " ORDER BY T1.ScheduledHireBeginDateTime ASC");

            foreach (DataTable table in d.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    message newMessage = new message();
                    DateTime collection = new DateTime();
                    string strVehicleVIN = "";
                    string strScheduledHireBeginDateTime = "";

                    foreach (DataColumn column in table.Columns)
                    {
                        if (column.ColumnName == "VehicleVIN")
                        {
                            strVehicleVIN = row[column].ToString();
                        }
                        if (column.ColumnName == "ScheduledHireBeginDateTime")
                        {
                            strScheduledHireBeginDateTime = row[column].ToString();
                            DateTime.TryParse(strScheduledHireBeginDateTime, out collection);
                        }
                    }
                    // Put the upcoming collection details in a message.
                    newMessage.messageType = "Upcoming Collection";
                    newMessage.title = "Collection at " + collection.ToString("HH:mm");
                    newMessage.content = strVehicleVIN + " due for collection at " + collection.ToString("HH:mm");
                    collectionsMessages.Add(newMessage);
                }
            }
            // Display the notification in a button.
            Button btnCollectionsDue = new Button();
            btnCollectionsDue.Content = collectionsMessages.Count + " collections due today";
            btnCollectionsDue.FontSize = 18;
            btnCollectionsDue.Click += btnCollections_Click;
            
            spCommonTasksSidePanelMainPanel.Children.Add(btnCollectionsDue);
        }

        private void getReturnTimes()
        {

        }
        private void getServiceTimes()
        {
             
        }

        private void getLateCollections()
        {

        }

        private void getLateReturns()
        {

        }
        private void getOutstandingPayments()
        {

        }


        private void Refresh_Click(object sender, RoutedEventArgs e)
        {

           
        }

        // Method to toggle the display on the context sensitive right panel.
        private void btnToday_Click(object sender, RoutedEventArgs e)
        {
            isPageShowingTodayDetails = !isPageShowingTodayDetails;

            if (isPageShowingTodayDetails)
            {
                imgBackground.Visibility = Visibility.Hidden;
                imgCommonImage.Visibility = Visibility.Collapsed;
                getCollectionTimes();
                
                btnToday.Content = "Hide Today";
                
            }
            else
            {
                imgBackground.Visibility = Visibility.Visible;
                imgCommonImage.Visibility = Visibility.Visible;
                spCommonTasksSidePanelMainPanel.Children.Clear();
                btnToday.Content = "Show Today";

                
            }

        }

        private void btnCollections_Click(object sender, RoutedEventArgs e)
        {
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new BookingsList());
        }
    }
    
}
