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
 *  Document    : BookingsEdit
 *                This form is used to edit booking information.
 *                This is a page which will be displayed on the left side of the
 *                Common Tasks Main Window. 
 */

namespace StephenGlasspell_CarRental
{
  
    /// Interaction logic for BookingsEdit.xaml

    public partial class BookingsEdit : Page
    {
        // Store the BookingID and VehicleVIN at Page scope, for use throughout the page.
        int bookingID;
        int CustomerID;
        string vehicleVIN;
        decimal decTotalBill = 0;
        decimal decTotalPaid = 0;
        List<string> vehiclesAvailable = new List<string>();



        // Dates used in form stored at Page scope.
        DateTime scheduledCollectionDateTime;
        DateTime scheduledReturnDateTime;
        DateTime actualCollectionDateTime;
        DateTime actualReturnDateTime;

        // This is a list of all the form fields on the page. It is used to easily iterate through the list
        // and validate the inputs and send to the database. 
        private List<FormField> fields = new List<FormField>();

        // Colours for use in validation. 
        private Color SUCCESS_COLOR = Color.FromArgb(150, 0, 255, 0);
        private Color FAIL_COLOR = Color.FromArgb(50, 255, 0, 0);

        public BookingsEdit()
        {
            InitializeComponent();
            
            // Generate the list of form fields, as declared above. 
            populateFields();

        }

        public BookingsEdit(int n)
        {
            InitializeComponent();

            bookingID = n;

            // Generate the list of form fields, as declared above. 
            populateFields();

            // Get the data from the database and display in the form.
            showBookingData();
            
        }

        // Method to get the data from the database and display in the form.
        void showBookingData()
        {
            // Send the SQL statement to the Database.
            DataSet d = Database.getInstance().select("", new string[] { " Booking as T1, Customer as T2, Vehicle as T3 " }, new String[] { " T1.BookingID,	T1.VehicleVIN,	T1.CustomerID,	T1.EmployeeID,	T1.BookingCreationDate,	T1.ScheduledHireBeginDateTime,	T1.ScheduledHireReturnDateTime,	T1.ActualHireBeginDateTime,	T1.ActualHireReturnDateTime,	T1.OdometerReadingPreHire,	T1.OdometerReadingPostHire,	T1.TotalBill, T2.Title, T2.FirstName, T2.LastName, T3.Manufacturer, T3.Model, T3.DailyRate " }, "WHERE T1.VehicleVIN = T3.VehicleVIN and T1.CustomerID = T2.CustomerID and T1.BookingID ", "=", bookingID.ToString(), new string[] { "" });

            // Strings to store the results from the database.
            // Initialised to prevent errors.
            String
                       strVehicleVIN = "",
                       strCustomerID = "",
                       strEmployeeID = "",
                       strBookingCreationDate = "",

                       strScheduledHireBeginDateTime = "",
                       strScheduledHireBeginDate = "",
                       strScheduledHireBeginHour = "",
                       strScheduledHireBeginMinute = "",

                       strScheduledHireReturnDateTime = "",
                       strScheduledHireReturnDate = "",
                       strScheduledHireReturnHour = "",
                       strScheduledHireReturnMinute = "",

                       strActualHireBeginDateTime = "",
                       strActualHireReturnDateTime = "",
                       strOdometerReadingPreHire = "",
                       strOdometerReadingPostHire = "",
                       strTotalBill = "",
                   

                       strTitle = "",
                       strFirstName = "",
                       strLastName = "",
                       strManufacturer = "",
                       strModel = "",
                       strDailyRate = "";

            

            // Iterate through the results returned from the database.
            foreach (DataTable table in d.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                  
                    foreach (DataColumn column in table.Columns)
                    {

                        // Handle all of the results and place the results in the correct strings as declared above.
                        if (column.ColumnName == "Title")
                        {
                            strTitle = row[column].ToString();

                        }
                        if (column.ColumnName == "FirstName")
                        {
                            strFirstName = row[column].ToString();

                        }
                        if (column.ColumnName == "LastName")
                        {
                            strLastName = row[column].ToString();

                        }

                        if (column.ColumnName == "VehicleVIN")
                        {
                            strVehicleVIN = row[column].ToString();

                            // Store the VehicleVIN at Page scope as well.
                            vehicleVIN = strVehicleVIN;
                         
                                 
                        }
                        if (column.ColumnName == "CustomerID")
                        {
                            strCustomerID = row[column].ToString();
                            CustomerID = int.Parse(strCustomerID);
                   
                           
                        }
                        if (column.ColumnName == "EmployeeID")
                        {
                            strEmployeeID = row[column].ToString();
                        }
                        if (column.ColumnName == "BookingCreationDate")
                        {
                            strBookingCreationDate = row[column].ToString();
                        }
                        if (column.ColumnName == "ScheduledHireBeginDateTime")
                        {
                            strScheduledHireBeginDateTime = row[column].ToString();

                            // This is a hidden TextBox used to hold the full date as text in one place.
                            txtScheduledHireBeginDateTime.Text = strScheduledHireBeginDateTime;

                            // Parse the date from a string to a DateTime object.
                            DateTime.TryParse(strScheduledHireBeginDateTime, out scheduledCollectionDateTime);

                            // Format the string in a way that displays well for the user.
                            strScheduledHireBeginDate = scheduledCollectionDateTime.ToString("dd-MM-yyyy");

                            // Put the date in the DatePicker Form Control
                            dpCollection.SelectedDate = scheduledCollectionDateTime;

                            // Get the HOUR
                            strScheduledHireBeginHour = scheduledCollectionDateTime.ToString("HH");

                            // Put the HOUR in the hour ComboBox using a horrible method which works, so leave it unless you have a better solution.
                            cmbCollectionHour.SelectedIndex = DataValidator.getComboBoxIndexOf(cmbCollectionHour, strScheduledHireBeginHour);

                            // Get the MINUTE
                            strScheduledHireBeginMinute = scheduledCollectionDateTime.ToString("mm");

                            // Put the MiNUTE in the minute ComboBox using the same method above.
                            cmbCollectionMinute.SelectedIndex = DataValidator.getComboBoxIndexOf(cmbCollectionMinute, strScheduledHireBeginMinute);

                        }
                        if (column.ColumnName == "ScheduledHireReturnDateTime")
                        {  
                            // This uses the same procedure as the one above.
                            // See above for details on use.
                            strScheduledHireReturnDateTime = row[column].ToString();
                            txtScheduledHireReturnDateTime.Text = strScheduledHireReturnDateTime;
                           
                            DateTime.TryParse(strScheduledHireReturnDateTime, out scheduledReturnDateTime);
                            strScheduledHireReturnDate = scheduledReturnDateTime.ToString("dd-MM-yyyy");
                            dpReturn.SelectedDate = scheduledReturnDateTime;
                            strScheduledHireReturnHour = scheduledReturnDateTime.ToString("HH");
                            cmbReturnHour.SelectedIndex = DataValidator.getComboBoxIndexOf(cmbReturnHour, strScheduledHireReturnHour);
                            strScheduledHireReturnMinute = scheduledReturnDateTime.ToString("mm");
                            cmbReturnMinute.SelectedIndex = DataValidator.getComboBoxIndexOf(cmbReturnMinute, strScheduledHireReturnMinute);

                        }
                        if (column.ColumnName == "ActualHireBeginDateTime")
                        {
                            strActualHireBeginDateTime = row[column].ToString();

                            // Hidden TextBox to hold the full date in one place.
                            txtActualHireBeginDateTime.Text = strActualHireBeginDateTime;

                            // Parse from a string to a dateTime object, held at Page scope.
                            DateTime.TryParse(strActualHireBeginDateTime, out actualCollectionDateTime);
                        }
                        if (column.ColumnName == "ActualHireReturnDateTime")
                        {
                            // This uses the same procedure as the one above.
                            // See above for details on use.
                            strActualHireReturnDateTime = row[column].ToString();
                            txtActualHireReturnDateTime.Text = strActualHireReturnDateTime;
                            DateTime.TryParse(strActualHireReturnDateTime, out actualReturnDateTime);
                        
                        }
                        if (column.ColumnName == "OdometerReadingPreHire")
                        {
                            strOdometerReadingPreHire = row[column].ToString();
                            txtOdometerReadingPreHire.Text = strOdometerReadingPreHire;
                        }
                        if (column.ColumnName == "OdometerReadingPostHire")
                        {
                            strOdometerReadingPostHire = row[column].ToString();
                            txtOdometerReadingPostHire.Text = strOdometerReadingPostHire;
                        }
                        if (column.ColumnName == "TotalBill")
                        {
                            // Take the string and parse it as a double, to format it as currency.
                            strTotalBill = row[column].ToString();
                            decimal totalBill = 0;
                            decimal.TryParse(strTotalBill, out totalBill);
                            txtTotalCharge.Text = totalBill.ToString("C");
                            decTotalBill = Math.Round(totalBill,2);
                        }
                    
                        if (column.ColumnName == "Manufacturer")
                        {
                            strManufacturer = row[column].ToString();

                        }
                        if (column.ColumnName == "Model")
                        {
                            strModel = row[column].ToString();

                        }
                        if (column.ColumnName == "DailyRate")
                        {
                            // Take the string and parse it as a double, to format it as currency.
                            strDailyRate = row[column].ToString();
                            double dailyRate = 0;
                            double.TryParse( strDailyRate, out dailyRate);
                            txtDailyRate.Text = dailyRate.ToString("C");

                        }
                    }

                }
                
            }
            // Display the Customer Name
            txtCustomerName.Text = strTitle + " " + strFirstName + " " + strLastName + ".";
            // Clear the list of Vehicles.
            lstVehiclesAvailable.Items.Clear();
            // Add the Vehicle to the listBox on the form.
            lstVehiclesAvailable.Items.Add(strManufacturer + " " + strModel);

            
            string strPaymentsMade = Database.getInstance().datasetToString(Database.getInstance().customSQL("SELECT SUM(Amount) FROM Payment WHERE BookingID = '" +bookingID+"'"));
            decimal decPaymentsMade = 0;
            Decimal.TryParse(strPaymentsMade, out decPaymentsMade);

            txtPaymentStatus.Text = decPaymentsMade.ToString("C");
            decTotalPaid = Math.Round(decPaymentsMade,2);
            showHideControls();
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
            fields.Add(new FormField(Name: "ScheduledHireBeginDateTime",            // Used to identify the field.
                                     FormControl: txtScheduledHireBeginDateTime,    // The form control that the user has enter information into
                                     UserEntryText: "",                             // The information that the user has entered.
                                     DatabaseTable: "Booking",                      // The table that this data is held in, in the database.
                                     DatabaseFieldName: "ScheduledHireBeginDateTime", // The Field in the table in the database
                                     SuccessTextBox: txtScheduledHireBeginDateTimeSuccess, // The "Success" TextBox to display a tick or cross to show whether the user input is valid.
                                     DataFieldType: DataValidator.dataFieldType.scheduledHireBeginDateTime)); // An enum of what kind of data this is, used for validation.

            fields.Add(new FormField(Name: "ScheduledHireReturnDateTime",
                                     FormControl: txtScheduledHireReturnDateTime,
                                     UserEntryText: "",
                                     DatabaseTable: "Booking",
                                     DatabaseFieldName: "ScheduledHireReturnDateTime",
                                     SuccessTextBox: txtScheduledHireReturnDateTimeSuccess,
                                     DataFieldType: DataValidator.dataFieldType.scheduledHireReturnDateTime));

            fields.Add(new FormField(Name: "txtOdometerReadingPreHire",
                                     FormControl: txtOdometerReadingPreHire,
                                     UserEntryText: "",
                                     DatabaseTable: "Booking",
                                     DatabaseFieldName: "OdometerReadingPreHire",
                                     SuccessTextBox: txtOdometerReadingPreHireSuccess,
                                     DataFieldType: DataValidator.dataFieldType.odometerReadingPreHire));

            fields.Add(new FormField(Name: "txtOdometerReadingPostHire",
                                     FormControl: txtOdometerReadingPostHire,
                                     UserEntryText: "",
                                     DatabaseTable: "Booking",
                                     DatabaseFieldName: "OdometerReadingPostHire",
                                     SuccessTextBox: txtOdometerReadingPostHireSuccess,
                                     DataFieldType: DataValidator.dataFieldType.odometerReadingPostHire));

            fields.Add(new FormField(Name: "txtTotalCharge",
                                     FormControl: txtTotalCharge,
                                     UserEntryText: "",
                                     DatabaseTable: "Booking",
                                     DatabaseFieldName: "TotalCharge",
                                     SuccessTextBox: txtTotalChargeSuccess,
                                     DataFieldType: DataValidator.dataFieldType.totalCharge));

        }

        // Method to show or hide certain buttons on the page.
        // Some actions may or may not be allowed depending on booking dates -
        // For example you can't change the booking dates if they are in the past.
        void showHideControls()
        {
            string acCol = actualReturnDateTime.ToString();
            string acRet = actualReturnDateTime.ToString();
            string paid = decTotalPaid.ToString();
            string bill = decTotalBill.ToString();

            

            if (DataDelegate.debugMode)
            {
                MessageBox.Show(acCol + "\n" + acRet + "\n" + paid + "\n" + bill, "Details");
            }



            if(decTotalPaid < decTotalBill)
            {
             
                btnMakePayment.Visibility = Visibility.Visible;
            }
            else
            {
                btnMakePayment.Visibility = Visibility.Collapsed;
            }

            // If the start of the booking is less than 30 minutes from now...
           if((scheduledCollectionDateTime.AddMinutes(-30).CompareTo(DateTime.Now) < 0) )
            {

                
                // If the vehicle has not yet been collected...
                if((actualCollectionDateTime == null)||(actualCollectionDateTime.CompareTo(new DateTime()) == 0))
                {
                    
                    // If there is an outstanding balance to be paid.
                    if (decTotalPaid < decTotalBill)
                    {
                        // Show the "Make Payment" button and hide the "Sign out Vehicle" Button
                        btnMakePayment.Visibility = Visibility.Visible;
                        btnSignOut.Visibility = Visibility.Collapsed;
                    }
                    else  // if the bill has been paid
                    {
                        // Hide the "Make Payment" button and hide the "Sign out Vehicle" Button
                        btnMakePayment.Visibility = Visibility.Collapsed;
                        btnSignOut.Visibility = Visibility.Visible;
                    }
                }
                else // If the vehicle has been collected...
                {
                    cmbCollectionHour.IsEnabled = false;
                    cmbCollectionMinute.IsEnabled = false;
                    dpCollection.IsEnabled = false;
                    btnSignOut.Visibility = Visibility.Collapsed;

                    // If the vehicle has not been returned...
                    if((actualReturnDateTime.CompareTo(new DateTime()) == 0)||(actualCollectionDateTime == null)  )
                    {
                     
                        // Show "Vehicle Sign In" button
                        btnSignIn.Visibility = Visibility.Visible;
                    }
                    else  // If the Vehicle has been returned
                    {
                        cmbReturnHour.IsEnabled = false;
                        cmbReturnMinute.IsEnabled = false;
                        dpReturn.IsEnabled = false;


                        // Hide "Vehicle Sign In" button
                        btnSignIn.Visibility = Visibility.Collapsed;
                    }
                }
            }
               
        }
           



            


        

        // Method to validate the user input on the fly.
        // Is called whenever a user changes data in a form control.
        void formInputChanged(object sender, RoutedEventArgs e)
        {
            // Variables to store the Property and Value.
            string name = "";
            string input = "";
            
            // dataFieldType is an enum to choose the type of date you want to validate.
            // Different dataFieldTypes have different validation rules.
            // Set to "NOTSET" as default.
            DataValidator.dataFieldType type = DataValidator.dataFieldType.NOTSET;

            // The associated "Success" TextBlock, used to display a tick or cross depending on whether user input is valid.
            TextBlock successTextBlock = null;

            // Has the validation been a success? Set to false as default.
            bool validationSuccess = false;

            // Find out which kind of form control has called this method.
            // And extra the Property (name) and Value (input).
            if (sender is TextBox)
            {
                TextBox t = (TextBox)sender;
                name = t.Name;
                input = t.Text;
            }
            else if (sender is ComboBox)
            {
                ComboBox c = (ComboBox)sender;
                name = c.Name;
                input = ((ComboBoxItem)(c.SelectedItem)).Content.ToString();

            }
            else if (sender is DatePicker)
            {
                DatePicker d = (DatePicker)sender;
                name = d.Name;
                input = d.SelectedDate.Value.Date.Date.ToShortDateString();
            }
            else
            {
                // If it's not one of the above types of form control, just stop here.
                return;
            }

            // Escape this method if it hasn't found the Control Type by now.
            if (String.IsNullOrEmpty(name))
            {
                return;
            }

            // Loop through the Fields List to assign the correct dataType, "Success" textBlock and the user input.
            for (int i = 0; i < fields.Count; i++)
            {
                if (fields[i].name.Equals(name))
                {
                    type = fields[i].dataFieldType;
                    successTextBlock = fields[i].successTextBox;
                    fields[i].userEntryText = input;
                }
            }

            // Find out the type of data the user has just entered ( ie firstname, dateOfBith, CreditCardNumber etc)
            if (type != DataValidator.dataFieldType.NOTSET)
            {
                // Check if the user input is valid, using the rules in the DataValidator.validateField method.
                validationSuccess = DataValidator.validateField(type, input);

                // If the data has been successfully validated, display the tick or a cross beside it.
                successTextBlock.Text = validationSuccess ? DataDelegate.SUCCESS : DataDelegate.FAIL;

                if (sender is TextBox)
                {
                    TextBox tb = (TextBox)sender;
                    tb.Background = validationSuccess ? new SolidColorBrush(SUCCESS_COLOR) : new SolidColorBrush(FAIL_COLOR);
                }

                if (sender is DatePicker)
                {
                    DatePicker dp = (DatePicker)sender;
                    dp.Background = validationSuccess ? new SolidColorBrush(SUCCESS_COLOR) : new SolidColorBrush(FAIL_COLOR);
                }

            }
        }

        // Method to clear all the Form Controls on the page.
        private void clearForm()
        {
            // Iterate through every field in the Fields List and set it to blank.
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

        // Method to check all the inputs are valid and send them to the database if valid.
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!bookingTimesAreValid())
            {
                MessageBox.Show("Please check your dates and try again.","Selected dates are invalid.");
                return;
            }

           int recordsUpdated = Database.getInstance().update("Booking", new string[] { "VehicleVIN","TotalBill", "ScheduledHireBeginDateTime", "ScheduledHireReturnDateTime" }, new string[] {vehicleVIN, txtTotalCharge.Text, scheduledCollectionDateTime.ToString("yyyy-MM-dd HH:mm:ss"), scheduledReturnDateTime.ToString("yyyy-MM-dd HH:mm:ss") },"WHERE BookingID","=", bookingID.ToString());


            if (recordsUpdated == 0) // If there are any errors, show a message.
            {
                MessageBox.Show("There were some errors updating the database.", "Database Error");
                return;
            }
            else  // If there are no errors, display the "BookingUpdateSuccess" page.
            {
                CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new BookingUpdateSuccess(bookingID));

                
            }
        }
        
        // Method to validate user input on the fly.
        // Similar to formInputChanged. Added this method instead of amending formInputChanged to avoid making over-long methods where possible.
        // TODO look into combining these methods in as few lines of code as possble, as having two similar methods is confusing.
        // THIS METHOD WAS INTRODUCED BECAUSE THE DATES ARE HANDLED BY THREE DIFFERENT CONTROLS (1 DatePicker and 2 ComboBoxes for hour and minute) AND THE COMBINED DATE IS HANDLED AND SEND TO DATABASE.
        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Find out what kind of control has been changed or had information entered.
            // Parse the data into variabes and hidden text fields.
            if (sender is ComboBox)
            {
                ComboBox c = (ComboBox)sender;

                if (c.Name == "cmbCollectionHour")
                {
                    ComboBoxItem cmbHour = c.SelectedItem as ComboBoxItem;
                    int hour = 0;
                    int.TryParse(cmbHour.Content.ToString(), out hour);
                    scheduledCollectionDateTime = new DateTime(scheduledCollectionDateTime.Year, scheduledCollectionDateTime.Month, scheduledCollectionDateTime.Day, hour, scheduledCollectionDateTime.Minute, scheduledCollectionDateTime.Second, scheduledCollectionDateTime.Millisecond);
                    txtScheduledHireBeginDateTime.Text = scheduledCollectionDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (c.Name == "cmbCollectionMinute")
                {
                    ComboBoxItem cmbMinute = c.SelectedItem as ComboBoxItem;
                    int minute = 0;
                    int.TryParse(cmbMinute.Content.ToString(), out minute);
                    scheduledCollectionDateTime = new DateTime(scheduledCollectionDateTime.Year, scheduledCollectionDateTime.Month, scheduledCollectionDateTime.Day, scheduledCollectionDateTime.Hour, minute, scheduledCollectionDateTime.Second, scheduledCollectionDateTime.Millisecond);
                    txtScheduledHireBeginDateTime.Text = scheduledCollectionDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }



                if (c.Name == "cmbReturnHour")
                {
                    ComboBoxItem cmbHour = c.SelectedItem as ComboBoxItem;
                    int hour = 0;
                    int.TryParse(cmbHour.Content.ToString(), out hour);
                    scheduledReturnDateTime = new DateTime(scheduledReturnDateTime.Year, scheduledReturnDateTime.Month, scheduledReturnDateTime.Day, hour, scheduledReturnDateTime.Minute, scheduledReturnDateTime.Second);
                    txtScheduledHireReturnDateTime.Text = scheduledReturnDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (c.Name == "cmbReturnMinute")
                {
                    ComboBoxItem cmbMinute = c.SelectedItem as ComboBoxItem;
                    int minute = 0;
                    int.TryParse(cmbMinute.Content.ToString(), out minute);
                    scheduledReturnDateTime = new DateTime(scheduledReturnDateTime.Year, scheduledReturnDateTime.Month, scheduledReturnDateTime.Day, scheduledReturnDateTime.Hour, minute, scheduledReturnDateTime.Second);
                    txtScheduledHireReturnDateTime.Text = scheduledReturnDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }



            }

            if (sender is DatePicker)
            {
                DatePicker d = (DatePicker)sender;

                if (d.Name == "dpCollection")
                {
                    int day = dpCollection.SelectedDate.Value.Day;
                    int month = dpCollection.SelectedDate.Value.Month;
                    int year = dpCollection.SelectedDate.Value.Year;

                    scheduledCollectionDateTime = new DateTime(year, month, day, scheduledCollectionDateTime.Hour, scheduledCollectionDateTime.Minute, scheduledCollectionDateTime.Second);
                    txtScheduledHireBeginDateTime.Text = scheduledCollectionDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (d.Name == "dpReturn")
                {
                    int day = dpReturn.SelectedDate.Value.Day;
                    int month = dpReturn.SelectedDate.Value.Month;
                    int year = dpReturn.SelectedDate.Value.Year;

                    scheduledReturnDateTime = new DateTime(year, month, day, scheduledReturnDateTime.Hour, scheduledReturnDateTime.Minute, scheduledReturnDateTime.Second); ;
                }


            }

            if (sender is TextBox)
            {
                TextBox t = (TextBox)sender;

                if(t.Name == "ScheduledHireBeginDateTime")
                {
                    txtScheduledHireBeginDateTimeSuccess.Text = collectionTimeIsInFuture() ? DataDelegate.SUCCESS : "";
                }
                if (t.Name == "CollectionDate")
                {
                    txtCollectionDateSuccess.Text = collectionTimeIsInFuture() ? DataDelegate.SUCCESS : "";
                }
                if (t.Name == "CollectionTime")
                {
                    txtCollectionTimeSuccess.Text = collectionTimeIsInFuture() ? DataDelegate.SUCCESS : "";
                }
                if (t.Name == "ScheduledHireReturnDateTime")
                {
                    txtScheduledHireReturnDateTimeSuccess.Text = returnTimeIsInFuture() ? DataDelegate.SUCCESS : "";
                }
                if (t.Name == "ReturnDate")
                {
                    txtReturnDateSuccess.Text = returnTimeIsInFuture() ? DataDelegate.SUCCESS : "";
                }
                if (t.Name == "ReturnTime")
                {
                    txtReturnTimeSuccess.Text = returnTimeIsInFuture() ? DataDelegate.SUCCESS : "";
                }
            }
            
            // Only display available vehicles if the booking is in the future. 
            if (collectionTimeIsInFuture() && returnTimeIsInFuture())
            {
                displayAvailableVehicles();
            }
            else  // If the booking is in the past
            {
                // Clear the VehicleVIN to make sure an incorrect Vehicle is booked.
                vehicleVIN = "";
                /*
                lstVehiclesAvailable.Items.Clear();
                lstVehiclesAvailable.Items.Add("Select your dates and times to see the available vehicles.");
                */
            }

        }
        
        // Method to check which vehicles are available on selected dates and display them in a listBox on the page.
        private void displayAvailableVehicles()
        {
            // Expand the Vehicle Selection ListBox.
            expVehicleSelection.IsExpanded = true;

            // Format the dates to strings, so that they can be sent to the database.
            String CollectionDateTime = scheduledCollectionDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            String ReturnDateTime = scheduledReturnDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            // Send the Query to the Database.
            DataSet results = Database.getInstance().customSQL("SELECT T1.DailyRate, T1.Manufacturer, T1.Model, T1.VehicleVIN " +
                                                    "FROM Vehicle as T1 " +
                                                    "WHERE T1.SuitableForHire = 'TRUE' and T1.Manufacturer NOT IN(" +
                                                   "SELECT Distinct T1.Manufacturer " +
                                                   "FROM Vehicle as T1, Booking as T2 " +
                                                   "WHERE t1.VehicleVIN = t2.VehicleVIN " +
                                                   "and T2.BookingID != '"+bookingID +
                                                   "' and not " +
                                                   "(T2.ScheduledHireReturnDateTime < '" + scheduledCollectionDateTime + "' " +
                                                   "or T2.ScheduledHireBeginDateTime > '" + scheduledReturnDateTime + "'))");

            lstVehiclesAvailable.Items.Clear();
            vehiclesAvailable.Clear();
            txtDailyRate.Clear();
            txtTotalCharge.Clear();

            // Iterate through the results and add the vehicles to the listbox
            foreach (DataTable table in results.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    String resultsText = "";

                    foreach (DataColumn column in table.Columns)
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
                            vehicleVIN = row[column].ToString();
                            
                        }
                    }
                    lstVehiclesAvailable.Items.Add(resultsText);
                    vehiclesAvailable.Add(vehicleVIN);
                  
                }

            }

            lstVehiclesAvailable.SelectedIndex = vehiclesAvailable.IndexOf(vehicleVIN) ;
        }

        // Method to check all the control forms for the Collection date and time.
        // Returns TRUE if the collection date is later than today's current date and time.
        private bool collectionTimeIsInFuture()
        {
            if (scheduledCollectionDateTime.CompareTo(DateTime.Now) >= 0)
            {
                // Recheck that the ComboBoxes and DatePicker selections are valid.
                if ((dpCollection.SelectedDate.Value.Date.CompareTo(DateTime.Now.Date) >= 0)
                    && (cmbCollectionHour.SelectedIndex > 0)
                    && (cmbCollectionMinute.SelectedIndex > 0))
                {
                    if(btnCancelBooking != null)
                    {
                        // Also, show the Cancel button if the booking is in the future.
                        btnCancelBooking.Visibility = Visibility.Visible;
                    }
                    
                    return true;
                }

            }
            else // if the date is in the past
            {
                if (btnCancelBooking != null)
                {
                    // Hide the Cancel button. It's too late.
                    btnCancelBooking.Visibility = Visibility.Collapsed;
                }
            }
            
            return false;
        }

        // Method to check if the Return Date Time is in the future.
        // Returns TRUE if the return date is later than the current date time.
        private bool returnTimeIsInFuture()
        {
            // Recheck the return date in the future and the return date is later than the collection date (Duh!)
            if ((scheduledReturnDateTime.CompareTo(DateTime.Now) > 0) && (scheduledReturnDateTime.CompareTo(scheduledCollectionDateTime) > 0))
            {
                // Recheck the datepicker and comboboxes show valid data.
                if ((dpReturn.SelectedDate.Value.Date.CompareTo(DateTime.Now.Date) > 0)
                    && (cmbReturnHour.SelectedIndex > 0)
                        && (cmbReturnMinute.SelectedIndex > 0))
                {
                    return true;
                }
            }

            return false;
        }

        // Method to validate the dates entered.
        private bool bookingTimesAreValid()
        {
            if (collectionTimeIsInFuture() && returnTimeIsInFuture())
            {
                return true;
            }

            return true;
        }

        // Method to delete the selected booking.
        // Warns the user with a MessageBox and sends a delete statement to the database.
        private void btnCancelBooking_Click(object sender, RoutedEventArgs e)
        {
            // Warning.
            MessageBoxResult result = MessageBox.Show( "Are you sure you want to cancel the booking?\nThe record will be permanently deleted.", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if(result == MessageBoxResult.Yes)
            {
                //Send SQL statement to database.
                Database.getInstance().customSQL("DELETE FROM BOOKING WHERE BookingID = " + bookingID.ToString());

                // Show confirmation.
                MessageBox.Show("Booking Deleted", "Success");

                // Navigate back to the List of Bookings page.
                CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new BookingsList());
            }
        }

        private void btnShowAvailableVehicles_Click(object sender, RoutedEventArgs e)
        {
            displayAvailableVehicles();
        }

        private void lstVehiclesAvailable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstVehiclesAvailable.SelectedIndex >= 0)
            {
                vehicleVIN = vehiclesAvailable.ElementAt(lstVehiclesAvailable.SelectedIndex);
     
            }
            
        
            DataSet d = Database.getInstance().customSQL("SELECT DailyRate FROM Vehicle WHERE VehicleVIN = '" +vehicleVIN+"'");

            foreach(DataTable table in d.Tables)
            {
                foreach(DataRow row in table.Rows)
                {
                    foreach(DataColumn column in table.Columns)
                    {
                        if(column.ColumnName == "DailyRate")
                        {
                            double dblDailyRate = 0;
                            double.TryParse(row[column].ToString(), out dblDailyRate);
                            txtDailyRate.Text = dblDailyRate.ToString("C");

                            double dblTotalBill = 0;                       
                            TimeSpan duration = (scheduledReturnDateTime - scheduledCollectionDateTime);
                            dblTotalBill = ((duration.Days + 1) * dblDailyRate);
                            txtTotalCharge.Text = dblTotalBill.ToString("C");
                        }
                    }
                }
            }
            
        }

        private void btnGoToCustomerEdit_Click(object sender, RoutedEventArgs e)
        {
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new CustomersEdit(CustomerID));


       

        }

        private void btnMakePayment_Click(object sender, RoutedEventArgs e)
        {
            
            decimal amountToPay = decTotalBill - decTotalPaid;

            if(amountToPay > 0)
            {
              int recordsInserted =  Database.getInstance().insert("Payment", new string[] { "BookingID", "Amount", "DateOfPayment" }, new string[] { bookingID.ToString(), amountToPay.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });

                if(recordsInserted > 0)
                {
                    
                    showBookingData();
                    showHideControls();
                    MessageBox.Show("The payment has been processed successfully.", "Payment Successful");
                }
                else
                {
                    MessageBox.Show("There was a problem creating the payment record.", "Payment failed.");
                }
            }
            else
            {
                if (DataDelegate.debugMode)
                {
                    MessageBox.Show("Total Bill : " + decTotalBill + "\nTotal Paid : " + decTotalPaid, "Payment Details.");
                }
                MessageBox.Show("The bill has already been settled.", "Nothing to pay.");
            }
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            int odo;
            if(!int.TryParse(txtOdometerReadingPreHire.Text, out odo))
            {
                MessageBox.Show("You must enter the Odometer PreHire Reading before signing out the vehicle.","Enter Odometer Reading.");
                return;
            }


          int recordsUpdated =  Database.getInstance().update("Booking", new string[] { "actualHireBeginDateTime" },new string[] { DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }, "WHERE BookingID" , "=", bookingID.ToString());

            if(recordsUpdated > 0)
            {
                MessageBox.Show("Please give the keys to customer.", "Vehicle Booked Out For Hire");
                showBookingData();
                showHideControls();
            }
            else
            {
                MessageBox.Show("There was a problem updating the database.", "Booking Out Error.");
            }
           

        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            int recordsUpdated = Database.getInstance().update("Booking", new string[] { "actualHireReturnDateTime" }, new string[] { DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }, "WHERE BookingID", "=", bookingID.ToString());

            if (recordsUpdated > 0)
            {
                MessageBox.Show("Please accept the keys from customer.", "Vehicle Booked Back In");
                showBookingData();
                showHideControls();
            }
            else
            {
                MessageBox.Show("There was a problem updating the database.", "Booking Out Error.");
            }
        }
    }
}
