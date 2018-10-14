using System;
using System.Text.RegularExpressions;
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
using WebEye.Controls.Wpf;
using System.IO;

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
    /// Interaction logic for CustomersNew.xaml
    /// </summary>
    public partial class CustomersNew : Page
    {

        

        private Color SUCCESS_COLOR = Color.FromArgb(150, 0, 255, 0);
        private Color FAIL_COLOR = Color.FromArgb(50, 255, 0, 0);

        private List<FormField> fields = new List<FormField>();

        public CustomersNew()
        {
            InitializeComponent();
            populateFields();
        }

        void populateFields()
        {
            fields.Add(new FormField(Name: "cmbTitle",
                                     FormControl: cmbTitle,
                                     UserEntryText: "",
                                     DatabaseTable: "Customer",
                                     DatabaseFieldName: "Title",
                                     SuccessTextBox: txtTitleSuccess,
                                     DataFieldType: DataValidator.dataFieldType.title));

            fields.Add(new FormField(Name: "txtFirstName",
                                    FormControl: txtFirstName,
                                    UserEntryText: "",
                                    DatabaseTable: "Customer",
                                    DatabaseFieldName: "FirstName",
                                    SuccessTextBox: txtFirstNameSuccess,
                                    DataFieldType: DataValidator.dataFieldType.firstname));

            fields.Add(new FormField(Name: "txtMiddleName",
                                    FormControl: txtMiddleName,
                                    UserEntryText: "",
                                    DatabaseTable: "Customer",
                                    DatabaseFieldName: "MiddleName",
                                    SuccessTextBox: txtMiddleNameSuccess,
                                    DataFieldType: DataValidator.dataFieldType.middlename));

            fields.Add(new FormField(Name: "txtLastName",
                                    FormControl: txtLastName,
                                    UserEntryText: "",
                                    DatabaseTable: "Customer",
                                    DatabaseFieldName: "LastName",
                                    SuccessTextBox: txtLastNameSuccess,
                                    DataFieldType: DataValidator.dataFieldType.lastname));

            fields.Add(new FormField(Name: "dpDateOfBirth",
                                    FormControl: dpDateOfBirth,
                                    UserEntryText: "",
                                    DatabaseTable: "Customer",
                                    DatabaseFieldName: "DateOfBirth",
                                    SuccessTextBox: txtDateOfBirthSuccess,
                                    DataFieldType: DataValidator.dataFieldType.dateOfBirth));

            fields.Add(new FormField(Name: "txtHouseNumber",
                                    FormControl: txtHouseNumber,
                                    UserEntryText: "",
                                    DatabaseTable: "CustomerContact",
                                    DatabaseFieldName: "HouseNumber",
                                    SuccessTextBox: txtHouseNoSuccess,
                                    DataFieldType: DataValidator.dataFieldType.houseNo));

            fields.Add(new FormField(Name: "txtStreet",
                                    FormControl: txtStreet,
                                    UserEntryText: "",
                                    DatabaseTable: "CustomerContact",
                                    DatabaseFieldName: "StreetName",
                                    SuccessTextBox: txtStreetNameSuccess,
                                    DataFieldType: DataValidator.dataFieldType.streetname));

            fields.Add(new FormField(Name: "txtSuburb",
                                    FormControl: txtSuburb,
                                    UserEntryText: "",
                                    DatabaseTable: "CustomerContact",
                                    DatabaseFieldName: "Suburb",
                                    SuccessTextBox: txtSuburbSuccess,
                                    DataFieldType: DataValidator.dataFieldType.suburb));

            fields.Add(new FormField(Name: "txtCity",
                                    FormControl: txtCity,
                                    UserEntryText: "",
                                    DatabaseTable: "CustomerContact",
                                    DatabaseFieldName: "City",
                                    SuccessTextBox: txtCitySuccess,
                                    DataFieldType: DataValidator.dataFieldType.city));

            fields.Add(new FormField(Name: "txtPostcode",
                                    FormControl: txtPostcode,
                                    UserEntryText: "",
                                    DatabaseTable: "CustomerContact",
                                    DatabaseFieldName: "Postcode",
                                    SuccessTextBox: txtPostCodeSuccess,
                                    DataFieldType: DataValidator.dataFieldType.postcode));

            fields.Add(new FormField(Name: "cmbState",
                                    FormControl: cmbState,
                                    UserEntryText: "",
                                    DatabaseTable: "CustomerContact",
                                    DatabaseFieldName: "State",
                                    SuccessTextBox: txtStateSuccess,
                                    DataFieldType: DataValidator.dataFieldType.state));

            fields.Add(new FormField(Name: "txtPhone",
                                    FormControl: txtPhone,
                                    UserEntryText: "",
                                    DatabaseTable: "CustomerContact",
                                    DatabaseFieldName: "MobilePhone",
                                    SuccessTextBox: txtPhoneSuccess,
                                    DataFieldType: DataValidator.dataFieldType.phone));

            fields.Add(new FormField(Name: "txtEmail",
                                    FormControl: txtEmail,
                                    UserEntryText: "",
                                    DatabaseTable: "CustomerContact",
                                    DatabaseFieldName: "Email",
                                    SuccessTextBox: txtEmailSuccess,
                                    DataFieldType: DataValidator.dataFieldType.email));

            fields.Add(new FormField(Name: "cmbDrivingLicenseCountry",
                                    FormControl: cmbDrivingLicenseCountry,
                                    UserEntryText: "",
                                    DatabaseTable: "DrivingLicense",
                                    DatabaseFieldName: "Country",
                                    SuccessTextBox: txtLicenseCountrySuccess,
                                    DataFieldType: DataValidator.dataFieldType.drivingLicenseIssuingCountry));

            fields.Add(new FormField(Name: "txtDrivingLicenseNumber",
                                    FormControl: txtDrivingLicenseNumber,
                                    UserEntryText: "",
                                    DatabaseTable: "DrivingLicense",
                                    DatabaseFieldName: "DrivingLicenseNumber",
                                    SuccessTextBox: txtLicenseNumberSuccess,
                                    DataFieldType: DataValidator.dataFieldType.drivingLicenseNumber));


            fields.Add(new FormField(Name: "dpDrivingLicenseIssueDate",
                                    FormControl: dpDrivingLicenseIssueDate,
                                    UserEntryText: "",
                                    DatabaseTable: "DrivingLicense",
                                    DatabaseFieldName: "IssueDate",
                                    SuccessTextBox: txtLicenseIssueDateSuccess,
                                    DataFieldType: DataValidator.dataFieldType.drivingLicenseIssueDate));

            fields.Add(new FormField(Name: "dpDrivingLicenseExpiry",
                                    FormControl: dpDrivingLicenseExpiry,
                                    UserEntryText: "",
                                    DatabaseTable: "DrivingLicense",
                                    DatabaseFieldName: "ExpiryDate",
                                    SuccessTextBox: txtLicenseExpirySuccess,
                                    DataFieldType: DataValidator.dataFieldType.drivingLicenseExpiryDate));

            fields.Add(new FormField(Name: "cmbDrivingConvictions",
                                    FormControl: cmbDrivingConvictions,
                                    UserEntryText: "",
                                    DatabaseTable: "DrivingLicense",
                                    DatabaseFieldName: "Convictions",
                                    SuccessTextBox: txtLicenseConvictionSuccess,
                                    DataFieldType: DataValidator.dataFieldType.drivingLicenseConvictions));

            fields.Add(new FormField(Name: "cmbCreditCardType",
                                    FormControl: cmbCreditCardType,
                                    UserEntryText: "",
                                    DatabaseTable: "CreditCard",
                                    DatabaseFieldName: "CredCardType",
                                    SuccessTextBox: txtCreditCardTypeSuccess,
                                    DataFieldType: DataValidator.dataFieldType.creditCardType));

            fields.Add(new FormField(Name: "txtCreditCardCardNumber",
                                    FormControl: txtCreditCardCardNumber,
                                    UserEntryText: "",
                                    DatabaseTable: "CreditCard",
                                    DatabaseFieldName: "CredCardNumber",
                                    SuccessTextBox: txtCreditCardNumberSuccess,
                                    DataFieldType: DataValidator.dataFieldType.creditCardNumber));

            fields.Add(new FormField(Name: "txtCreditCardCardName",
                                    FormControl: txtCreditCardCardName,
                                    UserEntryText: "",
                                    DatabaseTable: "CreditCard",
                                    DatabaseFieldName: "NameOnCredCard",
                                    SuccessTextBox: txtCreditCardNameSuccess,
                                    DataFieldType: DataValidator.dataFieldType.creditCardNameOnCard));

            fields.Add(new FormField(Name: "dpCreditCardExpiry",
                                    FormControl: dpCreditCardExpiry,
                                    UserEntryText: "",
                                    DatabaseTable: "CreditCard",
                                    DatabaseFieldName: "CredCardExpiry",
                                    SuccessTextBox: txtCreditCardExpirySuccess,
                                    DataFieldType: DataValidator.dataFieldType.creditCardExpiryDate));

            fields.Add(new FormField(Name: "txtCreditCardSecurityCode",
                                    FormControl: txtCreditCardSecurityCode,
                                    UserEntryText: "",
                                    DatabaseTable: "CreditCard",
                                    DatabaseFieldName: "CredCardSecurityCode",
                                    SuccessTextBox: txtCreditCardSecurityCodeSuccess,
                                    DataFieldType: DataValidator.dataFieldType.creditCardSecurityCode));
        }


        void formInputChanged(object sender, RoutedEventArgs e)
        {

            string name = "";
            string input = "";
            DataValidator.dataFieldType type = DataValidator.dataFieldType.NOTSET;
            TextBlock successTextBlock = null;
            bool validationSuccess = false;

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
                return;
            }

            // Escape this method if it hasn't found the Control Type by now.
            if (String.IsNullOrEmpty(name))
            {
                return;
            }

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
                validationSuccess = DataValidator.validateField(type, input);

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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (isDrivingLicenseNumberAlreadyInDatabase())
            {
                MessageBox.Show("The Driving License you entered is already in the database. No duplicates allowed.", "Driving License Duplicate");
                txtDrivingLicenseNumber.Focus();
                txtDrivingLicenseNumber.SelectAll();
                return;
            }

            bool allFieldsAreVaild = true;

            foreach (FormField field in fields)
            {
                if (field.successTextBox.Text != DataDelegate.SUCCESS)
                {
                    allFieldsAreVaild = false;
                    break;
                }
            }

            if (!allFieldsAreVaild)
            {
                MessageBox.Show("Please recheck your entries.", "Your form contains invalid data.");
            }
            else
            {
                List<String> tables = new List<String>();

                foreach (FormField field in fields)
                {
                    if (!tables.Contains(field.databaseTable))
                    {
                        tables.Add(field.databaseTable);
                    }

                }

                int CustomerID = Database.getInstance().getNextCustID();

                foreach (String table in tables)
                {
                    List<String> DBfields = new List<String>();
                    List<String> DBvalues = new List<String>();

                    DBfields.Add("CustomerID");
                    DBvalues.Add(CustomerID.ToString());

                    int errors = 0;

                    for (int index = 0; index < fields.Count; index++)
                    {
                        if (fields[index].databaseTable == table)
                        {
                            DBfields.Add(fields[index].databaseFieldName);
                            DBvalues.Add(fields[index].userEntryText);
                        }

                        if (index == fields.Count - 1)
                        {
                            String[] databaseFields = DBfields.ToArray();
                            String[] databaseValues = DBvalues.ToArray();

                            if (Database.getInstance().insert(table, databaseFields, databaseValues) == 0)
                            {
                                MessageBox.Show("There were some errors inserting data into the database.", "Database Error");
                                return;
                            
                            }
                            DBfields.Clear();
                            DBvalues.Clear();
                        }
                    }
                    
                    
                //  MessageBox.Show("Record created successfully.", "Database Updated.");
                CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new CustomerUpdateSuccess(CustomerID));
                    

                }
                clearForm();
            }

        }

        private void btnUpdateCustomerImage_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Please look into the webcam and smile!", "Prepare to take photograph.", MessageBoxButton.OKCancel); ;
            if (result == MessageBoxResult.OK)
            {
                Camera camera = new Camera();
                camera.takePicture();
                imgCustomerImage.Source = DataDelegate.customerImage;
            }

        }

        private void btnScanDrivingLicense_Click(object sender, RoutedEventArgs e)
        {
            Scanner scanner = new Scanner();
            scanner.initialiseScanner();
            scanner.scanImage();
        }

        private bool isDrivingLicenseNumberAlreadyInDatabase()
        {
            string drivingLicenseNumber = txtDrivingLicenseNumber.Text;
            int numberOfMatches = Database.getInstance().countMatches("DrivingLicense", "DrivingLicenseNumber", drivingLicenseNumber);
            if(numberOfMatches > 0)
            {
                return true;
            }
            return false;
        }
    }
}
