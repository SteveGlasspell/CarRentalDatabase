﻿using System;
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
 */

namespace StephenGlasspell_CarRental
{
    /// <summary>
    /// Interaction logic for FleetMain.xaml
    /// </summary>
    public partial class FleetMain : Page
    {
        public FleetMain()
        {
            InitializeComponent();
        }

        void showVehicles()
        {
            


        }

        private void btnVehicleEdit_Click(object sender, RoutedEventArgs e)
        {
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new FleetEdit());
        }

        private void btnVehicleList_Click(object sender, RoutedEventArgs e)
        {
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new FleetList());
        }

        private void btnNewService_Click(object sender, RoutedEventArgs e)
        {
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new ServiceNew());
        }

        private void btnNewVehicle_Click(object sender, RoutedEventArgs e)
        {
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new FleetNew());
        }
    }
}
