using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
    class FormField
    {
        public String name              { get; set; }
        public Object formControl       { get; set; }
        public String userEntryText     { get; set; }
        public String databaseTable     { get; set; }
        public String databaseFieldName { get; set; }
        public TextBlock successTextBox { get; set; }
        public DataValidator.dataFieldType dataFieldType { get; set; }

        public FormField() { }

        public FormField(String n)
        {
            this.name = n;
        }

        public FormField(String Name, 
                        Object FormControl, 
                        String UserEntryText, 
                        String DatabaseTable, 
                        String DatabaseFieldName, 
                        TextBlock SuccessTextBox, 
                        DataValidator.dataFieldType DataFieldType)
        {
            this.name =                 Name;
            this.formControl =          FormControl;
            this.userEntryText =        UserEntryText;
            this.databaseTable =        DatabaseTable;
            this.databaseFieldName =    DatabaseFieldName;
            this.successTextBox =       SuccessTextBox;
            this.dataFieldType =        DataFieldType;
        }

        public void clear()
        {
            this.name = "";
            this.formControl = null;
            this.userEntryText = "";
            this.databaseTable = "";
            this.databaseTable = "";
            this.successTextBox = null;
            this.dataFieldType = DataValidator.dataFieldType.NOTSET;
        }

    }
}
