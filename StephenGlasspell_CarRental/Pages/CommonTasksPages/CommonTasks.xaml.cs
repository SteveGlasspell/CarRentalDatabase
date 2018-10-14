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
 *  Document    : This Window is the window where the user will do most of their interactions.
 *              It hosts two main Frames which navigate to different pages, and four main
 *              buttons along the bottom.
 *              The Frame on the left is the main Frame where the user will interact with the software
 *              and the Frame on the right displays context sensitive messages and buttons,
 *              or a pretty picture of a Mercedes, depending on the user's preference.
 *              The context sensitive left screen can be toggled between pretty and functional with a button click.
 *              
 *              This CommonTasks Window implements the Singleton pattern so that the user always returns to the screen
 *              as they left it. 
 */

namespace StephenGlasspell_CarRental
{
    /// <summary>
    /// Interaction logic for Common.xaml
    /// </summary>
    public partial class CommonTasks : Window
    {
        public static CommonTasks instance = null;

        

        public CommonTasks()
        {
            InitializeComponent();
            // Display pages in the frames.
            frmCommonTasksSideFrame.Navigate(CommonTasksSidePanel.getInstance());
            frmCommonTasksMainFrame.Navigate(new CommonTasksSplashPage());   
        }

        // Singleton pattern - return a static instance of the window, only.
        public static CommonTasks getInstance()
        {
            if(instance == null)
            {
                instance = new CommonTasks();
            }

            return instance;
        }

        public void refresh()
        {
            frmCommonTasksMainFrame.Refresh();
       
        }



       

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            // Calls to the CustomerList page can take a few seconds. 
            // When navigation to that page, set the Mouse Cursor in Waiting mode,
            // and it will be returned to normal when the CustomerList page loads.
            Mouse.OverrideCursor = Cursors.Wait;
            frmCommonTasksMainFrame.Navigate(new CustomerList());
        }

        private void btnBookings_Click(object sender, RoutedEventArgs e)
        {
            frmCommonTasksMainFrame.Navigate(new BookingsList());
        }

        private void btnFleet_Click(object sender, RoutedEventArgs e)
        {
            frmCommonTasksMainFrame.Navigate(new FleetList());
           
        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            frmCommonTasksMainFrame.Navigate(new EmployeesMain());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Override the closing process and just hide the window instead.
            // Otherwise the window can't be reopened.
            e.Cancel = true;
            Hide();
        }

      
    }
}
