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
 *  Document    : This is a splash page for when the user logs in.
 *              It can be used to display important notifications, helpful tips, or provide security when the 
 *              screen is not in use, but not logged off.
 */

namespace StephenGlasspell_CarRental
{
    /// <summary>
    /// Interaction logic for CoomonTasksSplashPage.xaml
    /// </summary>
    public partial class CommonTasksSplashPage : Page
    {
        public CommonTasksSplashPage()
        {


            InitializeComponent();

            splashMessage();
        }

        void splashMessage()
        {
            txtWelcomeName.Text = DataDelegate.currentUserName;
           
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            splashMessage();
        }
    }
}
