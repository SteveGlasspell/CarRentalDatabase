﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
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
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Current User (Employee)

        private App()
        {
            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            findAvailableDatabase();
        }

       void findAvailableDatabase()
        {
            try
            {
                // Sets up the dummy bookings in the database to simulate real use.

                Database.getInstance().setDBSource("OLYMPUS-MONS-FI");
                
                Database.getInstance().customSQL("UPDATE Booking set ActualHireBeginDateTime = ScheduledHireBeginDateTime WHERE ScheduledHireBeginDateTime < CURRENT_TIMESTAMP;");
                Database.getInstance().customSQL("UPDATE Booking set ActualHireReturnDateTime = ScheduledHireReturnDateTime WHERE ScheduledHireReturnDateTime < CURRENT_TIMESTAMP;");
                Database.getInstance().customSQL("UPDATE Booking set ActualHireBeginDateTime = null, ActualHireReturnDateTime = null WHERE ScheduledHireBeginDateTime > CURRENT_TIMESTAMP");
                Database.getInstance().customSQL("UPDATE Booking set ActualHireReturnDateTime = null WHERE ScheduledHireReturnDateTime > CURRENT_TIMESTAMP");
            }
            catch (Exception e)
            {
                Database.getInstance().setDBSource("MSSQLSERVER021");
                Database.getInstance().customSQL("UPDATE Booking set ActualHireBeginDateTime = ScheduledHireBeginDateTime WHERE ScheduledHireBeginDateTime < CURRENT_TIMESTAMP;");
                Database.getInstance().customSQL("UPDATE Booking set ActualHireReturnDateTime = ScheduledHireReturnDateTime WHERE ScheduledHireReturnDateTime < CURRENT_TIMESTAMP;");
                Database.getInstance().customSQL("UPDATE Booking set ActualHireBeginDateTime = null, ActualHireReturnDateTime = null WHERE ScheduledHireBeginDateTime > CURRENT_TIMESTAMP");
                Database.getInstance().customSQL("UPDATE Booking set ActualHireReturnDateTime = null WHERE ScheduledHireReturnDateTime > CURRENT_TIMESTAMP");

            }
        }
    }
}
