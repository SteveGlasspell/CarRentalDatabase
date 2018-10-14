using System;
using System.Collections.Generic;
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
 *  Document    : This page is a main navigation page to all of the possible tasks a user would want to do 
 *                 regarding Bookings.
 *                 EDIT : This page has been bypassed for the sake of a faster user interface.
 *                 It is easier for a user to just jump to a list of bookings and be able to 
 *                 search for a specific booking, and then perform the task. 
 *                 This page is pretty but not completely necessary.
 *                 The page is to be kept, in case of future need, but is currently not used.
 */

namespace StephenGlasspell_CarRental
{
    /// <summary>
    /// Interaction logic for BookingsMain.xaml
    /// </summary>
    public partial class BookingsMain : Page
    {
        // Page scope variable accessible by all methods.
        int CustomerID;


        public BookingsMain()
        {
            InitializeComponent();
        }
        public BookingsMain(int custID)
        {
            InitializeComponent();
            CustomerID = custID;
        }

        
        // Method to navigate to BookingsNew page.
        // If a customerID is selected, use the customerID as a parameter in the call to the page.
        private void btnBookingNew_Click(object sender, RoutedEventArgs e)
        {
            if((CustomerID != null)&&(CustomerID > 0))
            {
                CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new BookingsNew(CustomerID));
            }
            else
            {
                CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new BookingsNew());

            }

        }

        // Navigate to the BookingsList.
        private void btnBookingList_Click(object sender, RoutedEventArgs e)
        {
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new BookingsList());
        }
    }
}
