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
using System.Threading;

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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow instance = null;

        public MainWindow()
        {
            InitializeComponent();
            
            if(!isUserLoggedIn())
            {
                showLoginScreen();
                Hide();
            }   
        }

        public static MainWindow getInstance()
        {
            if (instance == null)
            {
                instance = new MainWindow();

            }
            return instance;
        }

        public void enableWindow()
        {
            dpMainPageDockPanel.IsEnabled = true;
           
           
        }

        private bool isUserLoggedIn()
        {
            if((DataDelegate.currentUserName != null) && (DataDelegate.currentUserName != ""))
            {
                return true;
            }

            return false;
        }

        private void showLoginScreen()
        {
            dpMainPageDockPanel.IsEnabled = false;

            LoginPage login = new LoginPage();
            login.Show();

            
        }
     

       

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void frmMainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void miViewCommonTasks_Click(object sender, RoutedEventArgs e)
        {
            CommonTasks.getInstance().Hide();
            CommonTasks.getInstance().Show();
            this.Topmost = false;
          
            if(CommonTasks.getInstance().WindowState != WindowState.Normal)
            {
                CommonTasks.getInstance().WindowState = WindowState.Normal;
            }

        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Topmost = true;
            about.Show();
        }

        private void miScan_Click(object sender, RoutedEventArgs e)
        {
            Scanner scanner = new Scanner();
           if(scanner.initialiseScanner())
            {
                scanner.scanImage();
            }
        }

        private void frmMainWindow_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CommonTasks.getInstance().Topmost == false)
            {
                CommonTasks.getInstance().Topmost = true;
            }
        }

        private void miPrint_Click(object sender, RoutedEventArgs e)
        {
            Printer printer = new Printer();
            printer.print();
        }

        private void miCamera_Click(object sender, RoutedEventArgs e)
        {
           
            Camera camera = new Camera();
            camera.takePicture();
            
            

        }

        private void miSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            CommonTasks.getInstance().Hide();
            settings.Show();
        }
    }
}
