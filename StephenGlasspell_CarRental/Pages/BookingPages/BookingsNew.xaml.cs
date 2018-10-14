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
 *  Document    : This page is for creating new Bookings.
 *              When this page is opened, the customerID should already be known and 
 *              passed to this page as a parameter, and then used in the constructor.
 *              A constructor without the customerId parameter pass is also provided, just in case,
 *              but it should never be used.
 */

namespace StephenGlasspell_CarRental
{
    /// <summary>
    /// Interaction logic for BookingsNew.xaml
    /// </summary>
    public partial class BookingsNew : Page
    {
        // Page scope variables accessible to all methods on the page.
        private int CustomerID;
        String VehicleVIN;
        bool formHasLoaded = false;

        DateTime collectionDateTime = DateTime.Now;
        DateTime returnDateTime = DateTime.Now;

        // List of control forms.
        private List<FormField> fields = new List<FormField>();

        // List of Vehicles available between selected dates.
        List<String> vehiclesAvailable = new List<String>();


        public BookingsNew()
        {
            InitializeComponent();

            // Generate a list of control fields. Used for validation.
            populateFields();
        }

        // Constructor with parameter.
        public BookingsNew(int custID)
        {
            InitializeComponent();
            CustomerID = custID;

            // Generate a list of control fields. Used for validation.
            populateFields();

        }
        // Constructor with both customerID and the customer Name. Should be the main constructor.
        public BookingsNew(int custID, String custName)
        {
            InitializeComponent();
            CustomerID = custID;
            txtCustomerName.Text = custName;
            
        }

        // Method to generate the list of Form Fields used on this page.
        // The list is used to easily link form fields like TextBoxes and DatePickers with their
        // associated "Success" TextBoxes to show if the user input is valid.
        // Also, the Fields list makes it easy to validate the data because the dataType has been selected from an enum
        // and each field is assigned to a Table in the database, making it easy to iterate through this list and
        // send the data to the correct table in the database.
        // This method is copied to similar pages throughout the application and used in the same manner,
        // althought obviously the form controls change from page to page.
        void populateFields()
        {

            fields.Add(new FormField(Name: "ScheduledHireBeginDateTime",        // Used to identify the field.
                                     FormControl: txtScheduledHireBeginDateTime,  // The form control that the user has enter information into
                                     UserEntryText: "",                              // The information that the user has entered.
                                     DatabaseTable: "Booking",                          // The table that this data is held in, in the database.
                                     DatabaseFieldName: "txtScheduledHireBeginDateTime",  // The Field in the table in the database
                                     SuccessTextBox: txtScheduledHireBeginDateTimeSuccess,  // The "Success" TextBox to display a tick or cross to show whether the user input is valid.
                                     DataFieldType: DataValidator.dataFieldType.scheduledHireBeginDateTime)); // An enum of what kind of data this is, used for validation.

            fields.Add(new FormField(Name: "ScheduledHireReturnDateTime",
                                     FormControl: txtScheduledHireReturnDateTime,
                                     UserEntryText: "",
                                     DatabaseTable: "Booking",
                                     DatabaseFieldName: "txtScheduledHireReturnDateTime",
                                     SuccessTextBox: txtScheduledHireReturnDateTimeSuccess,
                                     DataFieldType: DataValidator.dataFieldType.scheduledHireReturnDateTime));
      

            fields.Add(new FormField(Name: "txtTotalCharge",
                                     FormControl: txtTotalCharge,
                                     UserEntryText: "",
                                     DatabaseTable: "Booking",
                                     DatabaseFieldName: "TotalCharge",
                                     SuccessTextBox: txtTotalChargeSuccess,
                                     DataFieldType: DataValidator.dataFieldType.totalCharge));

        }

        // Method to check all the inputs are valid and send them to the database if valid.
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
           
            bool allFieldsAreVaild = true;

            foreach (FormField field in fields)
            {
                if (field.successTextBox.Text != DataDelegate.SUCCESS)
                {
                    // Only display this error message if in debug mode.
                    if (DataDelegate.debugMode)
                    {
                        MessageBox.Show(field.name.ToString(), "Field not valid");
                    }
                    allFieldsAreVaild = false;
                    break;
                }
            }

            if (!allFieldsAreVaild)
            {
                MessageBox.Show("Please recheck your entries.", "Your form contains invalid data.");
                return;
            }
            else
            {
                // New list of tables. These relate to the tables in the database.
                List<String> tables = new List<String>();

                // Iterate through every field in the fields list.
                foreach (FormField field in fields)
                {
                    //If the table for the selected datafield is not in the list, add it to the list.
                    if (!tables.Contains(field.databaseTable))
                    {
                        tables.Add(field.databaseTable);
                    }

                }

                // Find out what the next customerId in the database should be.
                // TODO - shouldn't this be auto incremented? FIX THIS.
                int CustomerID = Database.getInstance().getNextCustID();

                // For every table in the list, make lists of fields and values (Properties and values)
                foreach (String table in tables)
                {
                    List<String> DBfields = new List<String>();
                    List<String> DBvalues = new List<String>();

                    DBfields.Add("CustomerID");
                    DBvalues.Add(CustomerID.ToString());

                    // Count any errors.
                    int errors = 0;

                    // This code is to build lists of Database fields and their associated values, per table in the databse.
                    for (int index = 0; index < fields.Count; index++)
                    {
                        if (fields[index].databaseTable == table)
                        {
                            DBfields.Add(fields[index].databaseFieldName);
                            DBvalues.Add(fields[index].userEntryText);
                        }

                        // When you get to the end of the list of fields, 
                        if (index == fields.Count - 1)
                        {
                            // Create string arrays from the lists...
                            String[] databaseFields = DBfields.ToArray();
                            String[] databaseValues = DBvalues.ToArray();

                            // .. and include them in the SQL statement to the database.
                            //                  Array of tables
                            //                   \/      Array of fields
                            //                            \/            Array of values
                            if (Database.getInstance().insert(table, databaseFields, databaseValues) == 0)
                            {

                                errors++;
                            }
                            DBfields.Clear();
                            DBvalues.Clear();
                        }
                    }
                    if (errors > 0) // There were errors.
                    {
                        MessageBox.Show("There were some errors inserting data into the database.", "Database Error");
                        return;
                    }
                    else  // No errors found
                    {
                       // Navigate to the Customer Update Success page.
                        CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new CustomerUpdateSuccess(CustomerID));
                    }

                }
                clearForm();
            }

        }

        // Method to clear all the form controls and associated variables and "Success" textboxes.
        // Iterates through the Fields list and clears values.
        private void clearForm()
        {
            foreach (FormField field in fields)
            {
                if (field.formControl is TextBox)
                {
                    TextBox t = (TextBox)field.formControl;
                    t.Text = "";
                }
                if (field.formControl is ComboBox)
                {
                    ComboBox c = (ComboBox)field.formControl;
                    c.SelectedIndex = 0;
                }
                if (field.formControl is DatePicker)
                {
                    DatePicker dp = (DatePicker)field.formControl;
                    dp.SelectedDate = DateTime.Now;
                }
            }
        }

        // Method to validate user input on the fly. Called whenever a form control (textbox, datepicker etc)
        // is changed by the user.
        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Some errors are experienced if the page hasn't loaded fully. This has been fixed on other pages.
            // TODO Fix this.
            if (!formHasLoaded)
            {
                return;
            }

            // Find the type of form control that was just changed.
            // Extract the 

            if(sender is ComboBox)
            {
                ComboBox c = (ComboBox)sender;

                if(c.Name == "cmbCollectionHour")
                {
                    ComboBoxItem cmbHour = c.SelectedItem as ComboBoxItem;
                    int hour;
                    int.TryParse(cmbHour.Content.ToString(), out hour);
                    collectionDateTime = new DateTime(collectionDateTime.Year, collectionDateTime.Month, collectionDateTime.Day, hour, collectionDateTime.Minute, collectionDateTime.Second, collectionDateTime.Millisecond);
                }
                if (c.Name == "cmbCollectionMinute")
                {
                    ComboBoxItem cmbMinute = c.SelectedItem as ComboBoxItem;
                    int minute;
                    int.TryParse(cmbMinute.Content.ToString(), out minute);
                    collectionDateTime = new DateTime(collectionDateTime.Year, collectionDateTime.Month, collectionDateTime.Day, collectionDateTime.Hour, minute, collectionDateTime.Second, collectionDateTime.Millisecond);
                }



                if (c.Name == "cmbReturnHour")
                {
                    ComboBoxItem cmbHour = c.SelectedItem as ComboBoxItem;
                    int hour;
                    int.TryParse(cmbHour.Content.ToString(),out hour);
                    returnDateTime = new DateTime(returnDateTime.Year, returnDateTime.Month, returnDateTime.Day, hour, returnDateTime.Minute, returnDateTime.Second);
                }

                if (c.Name == "cmbReturnMinute")
                {
                    ComboBoxItem cmbMinute = c.SelectedItem as ComboBoxItem;
                    int minute;
                    int.TryParse(cmbMinute.Content.ToString(), out minute); 
                    returnDateTime = new DateTime(returnDateTime.Year, returnDateTime.Month, returnDateTime.Day, returnDateTime.Hour, minute, returnDateTime.Second);
                }
            }

            if(sender is DatePicker)
            {
                DatePicker d = (DatePicker)sender;

                if(d.Name == "dpCollection")
                {
                    int day = dpCollection.SelectedDate.Value.Day;
                    int month = dpCollection.SelectedDate.Value.Month;
                    int year = dpCollection.SelectedDate.Value.Year;

                    collectionDateTime = new DateTime(year, month, day, collectionDateTime.Hour, collectionDateTime.Minute, collectionDateTime.Second);
                }

                if(d.Name == "dpReturn")
                {
                    int day = dpReturn.SelectedDate.Value.Day;
                    int month = dpReturn.SelectedDate.Value.Month;
                    int year = dpReturn.SelectedDate.Value.Year;

                    returnDateTime = new DateTime(year, month, day, returnDateTime.Hour, returnDateTime.Minute, returnDateTime.Second); ;
                }
            }

            if(sender is TextBox)
            {
                TextBox t = (TextBox)sender;
            }
            // Validate collection time and display tick or cross on screen in "Success" textbox.
            txtCollectionDateSuccess.Text = collectionTimeIsInFuture() ? DataDelegate.SUCCESS : "";
            txtCollectionTimeSuccess.Text = collectionTimeIsInFuture() ? DataDelegate.SUCCESS : "";

            txtReturnDateSuccess.Text = returnTimeIsInFuture() ? DataDelegate.SUCCESS : "";
            txtReturnTimeSuccess.Text = returnTimeIsInFuture() ? DataDelegate.SUCCESS : "";

            if(collectionTimeIsInFuture() && returnTimeIsInFuture())
            {
                displayAvailableVehicles();
            }
            else // collectiontime is in the past
            {
                // Display message in the listbox
                lstVehiclesAvailable.Items.Clear();
                lstVehiclesAvailable.Items.Add("Select your dates and times to see the available vehicles.");
            }

        }

        // Method that calls the database and displays results in the listBox onscreen.
        private void displayAvailableVehicles()
        {
          
            expVehicleSelection.IsExpanded = true;
            
            String CollectionDateTime = collectionDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            String ReturnDateTime = returnDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            // SQL command to databse using a custom SQL method. Not very secure. 
            // TODO fix this - change to a regular select method call.
            DataSet results = Database.getInstance().customSQL("SELECT T1.DailyRate, T1.Manufacturer, T1.Model, T1.VehicleVIN " +
                                                    "FROM Vehicle as T1 " +
                                                    "WHERE T1.SuitableForHire = 'TRUE' and T1.Manufacturer NOT IN(" +
                                                   "SELECT Distinct T1.Manufacturer "+
                                                   "FROM Vehicle as T1, Booking as T2 "+
                                                   "WHERE t1.VehicleVIN = t2.VehicleVIN and not "+
                                                   "(T2.ScheduledHireReturnDateTime < '"+collectionDateTime+ "' "+
                                                   "or T2.ScheduledHireBeginDateTime > '"+returnDateTime+"'))");
            vehiclesAvailable.Clear();
            lstVehiclesAvailable.Items.Clear();
            txtDailyRate.Clear();
            txtTotalCharge.Clear();

            // Iterate through results and add to listbox.
            foreach(DataTable table in results.Tables)
            {
                foreach(DataRow row in table.Rows)
                {
                    String resultsText = "";

                    foreach(DataColumn column in table.Columns)
                    {
                        if (column.ColumnName == "DailyRate")
                        {

                            resultsText += double.Parse(row[column].ToString()).ToString("C") + " /day. \t";
                        }
                        if (column.ColumnName == "Manufacturer")
                        {
                            resultsText += row[column].ToString() + " ";
                        }
                        if (column.ColumnName == "Model")
                        {
                            resultsText += row[column].ToString() + " \t";
                        }
                        if (column.ColumnName == "VehicleVIN")
                        {
                            vehiclesAvailable.Add(row[column].ToString());
                        }
                    }
                    lstVehiclesAvailable.Items.Add(resultsText);
                }
                
            }
        }

        // Method checks collectiondateTime, asscociated datepicker and comboboxes,
        // Returns TRUE if date later than today and fields are valid.
        private bool collectionTimeIsInFuture()
        {
            if(collectionDateTime.CompareTo(DateTime.Now) >= 0)
            {
                if((dpCollection.SelectedDate.Value.Date.CompareTo(DateTime.Now.Date) >= 0) 
                    && (cmbCollectionHour.SelectedIndex > 0) 
                    && (cmbCollectionMinute.SelectedIndex > 0))
                {
                    return true;
                }

            }

            return false;
        }

        // Method to check returndatetime and assocated datepicker and comboboxes
        // Returns TRUE if date is later than today and return datetime is later than collection datetime
        private bool returnTimeIsInFuture()
        {
            if((returnDateTime.CompareTo(DateTime.Now) > 0) && (returnDateTime.CompareTo(collectionDateTime) > 0)){
                
                if((dpReturn.SelectedDate.Value.Date.CompareTo(DateTime.Now.Date) > 0) 
                    && (cmbReturnHour.SelectedIndex > 0)
                        && (cmbReturnMinute.SelectedIndex > 0))
                {
                    return true;
                }
            }

            return false;
        }

        // Short method to check both collection and return dates.
        private bool bookingTimesAreValid()
        {
            if(collectionTimeIsInFuture() && returnTimeIsInFuture())
            {
                return true;
            }

            return false;
        }

        // Method to check the form has loaded. Used to fix a bug with SelectionChanged method.
        // TODO fix the bug and get rid of this.
        private void dpCollection_Loaded(object sender, RoutedEventArgs e)
        {
            formHasLoaded = true;
        }


        // Method to handle events when the user selects a car from the list of available vehicle.
        private void lstVehiclesAvailable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check the user has really selected something.
            if((vehiclesAvailable.Count > 0)&&(lstVehiclesAvailable.SelectedIndex >= 0))
            {
                // Get the VehicleVIN
              VehicleVIN =   vehiclesAvailable.ElementAt(lstVehiclesAvailable.SelectedIndex);
                double dbldailyRate = 0;
                // Call to database to find out the DailyRate.
                DataSet results = Database.getInstance().select("", new string[] { "Vehicle as T1" }, new string[] { "T1.DailyRate" }, "WHERE T1.VehicleVIN ", "=", VehicleVIN, new String[] { });

                foreach(DataTable table in results.Tables)
                {
                    foreach(DataRow row in table.Rows)
                    {
                        foreach(DataColumn column in table.Columns)
                        {
                            if(column.ColumnName == "DailyRate")
                            {
                                double.TryParse( row[column].ToString(), out dbldailyRate);
                            }
                        }
                    }
                }

                // Put the dailyRate in a textBox, formatted as currency.
                txtDailyRate.Text = dbldailyRate.ToString("C");

                // Calculate the Total Price of the booking
                // Based on the hire dates and the dailyRate.
                TimeSpan duration = new TimeSpan();
                duration = (returnDateTime - collectionDateTime);
                txtTotalCharge.Text = ((duration.Days +1)* dbldailyRate).ToString("C");
                expPaymentDetails.IsExpanded = true;
                

            }
            else // The user didn't select anything (or the listbox has been cleared)
            {
                txtDailyRate.Clear();
                txtTotalCharge.Clear();
            }

        }

        // Method to insert the booking data into the database
        private void btnConfirmBooking_Click(object sender, RoutedEventArgs e)
        {
            string strSHBDT = collectionDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            string strSHRDT = returnDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            
            // Databse insert method call. Returns the number of changed records.
            int success = Database.getInstance().insert("Booking ", new string[] { "VehicleVIN", "CustomerID", "EmployeeID", "ScheduledHireBeginDateTime", "ScheduledHireReturnDateTime", "TotalBill" }, new string[] { VehicleVIN, CustomerID.ToString(), DataDelegate.EmployeeID.ToString(), strSHBDT, strSHRDT, txtTotalCharge.Text });

            if(success > 0) // If the numbers of inserted records is more than zero.
            {
                // Navigate to BookingCreatedSuccess page.
                clearForm();
                CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new BookingCreatedSuccess());
            }
            else // No records were inserted.
            {
                // Show the error message.
                MessageBox.Show("There were problems entering the data into the database.", "Error");
            }
        }
    }
}
