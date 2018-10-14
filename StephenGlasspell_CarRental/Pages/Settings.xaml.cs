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
 */

namespace StephenGlasspell_CarRental
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            populateSettings();
        }

       void populateSettings()
        {
            if(DataDelegate.debugMode == true)
            {
                cmbDebugMode.SelectedIndex = 0;
            }
            else
            {
                cmbDebugMode.SelectedIndex = 1;
            }

            cmbDatabase.SelectedIndex = DataValidator.getComboBoxIndexOf(cmbDatabase, Database.dbSource);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CommonTasks.getInstance().Show();
        }

        private void cmbDebugMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbDebugMode.SelectedIndex == 0)
            {
                DataDelegate.debugMode = true;
            }
            else
            {
                DataDelegate.debugMode = false;
            }
        }

        private void cmbDatabase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            cmbDatabase.SelectedItem = DataValidator.getComboBoxIndexOf(cmbDatabase, Database.dbSource);


        }
    }
}
