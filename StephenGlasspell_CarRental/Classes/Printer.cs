using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Drawing.Printing;
using System.Windows.Media.Animation;

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
    class Printer
    {
        

        

        public Printer()
        {
            
        }


        public void print()
        {
            PrintDialog print = new PrintDialog();
            if (CommonTasks.getInstance().Topmost)
            {
                CommonTasks.getInstance().Topmost = false;
                
                print.ShowDialog();
                CommonTasks.getInstance().Topmost = true;
            }
            else
            {
                
                print.ShowDialog();
            }

            


            
        }

    }
}
