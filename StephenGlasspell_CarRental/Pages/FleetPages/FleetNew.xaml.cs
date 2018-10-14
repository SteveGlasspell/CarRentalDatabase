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
 */

namespace StephenGlasspell_CarRental
{
    /// <summary>
    /// Interaction logic for FleetNew.xaml
    /// </summary>
    public partial class FleetNew : Page
    {
        private const string SUCCESS = "✅";
        private const string FAIL = "❎";

        private Color SUCCESS_COLOR = Color.FromArgb(150, 0, 255, 0);
        private Color FAIL_COLOR = Color.FromArgb(50, 255, 0, 0);

        private List<FormField> fields = new List<FormField>();


        public FleetNew()
        {
            InitializeComponent();
            populateFields();
        }

        void populateFields()
        {
            fields.Add(new FormField(Name: "txtVehicleVIN",
                                     FormControl: txtVehicleVIN,
                                     UserEntryText: "",
                                     DatabaseTable: "Vehicle",
                                     DatabaseFieldName: "VehicleVIN",
                                     SuccessTextBox: txtVehicleVINSuccess,
                                     DataFieldType: DataValidator.dataFieldType.vehicleVIN));

            fields.Add(new FormField(Name: "txtManufacturer",
                                    FormControl: txtManufacturer,
                                    UserEntryText: "",
                                    DatabaseTable: "Vehicle",
                                    DatabaseFieldName: "Manufacturer",
                                    SuccessTextBox: txtManufacturerSuccess,
                                    DataFieldType: DataValidator.dataFieldType.manufacturer));

            fields.Add(new FormField(Name: "txtModel",
                                    FormControl: txtModel,
                                    UserEntryText: "",
                                    DatabaseTable: "Vehicle",
                                    DatabaseFieldName: "Model",
                                    SuccessTextBox: txtModelSuccess,
                                    DataFieldType: DataValidator.dataFieldType.model));

            fields.Add(new FormField(Name: "dpDateOfFirstRegistration",
                                    FormControl: dpDateOfFirstRegistration,
                                    UserEntryText: "",
                                    DatabaseTable: "Vehicle",
                                    DatabaseFieldName: "DateOfFirstRegistration",
                                    SuccessTextBox: txtDateOfFirstRegistrationSuccess,
                                    DataFieldType: DataValidator.dataFieldType.dateOfFirstRegistration));

            fields.Add(new FormField(Name: "txtVehicleDetails",
                                    FormControl: txtVehicleDetails,
                                    UserEntryText: "",
                                    DatabaseTable: "Vehicle",
                                    DatabaseFieldName: "VehicleDetails",
                                    SuccessTextBox: txtVehicleDetailsSuccess,
                                    DataFieldType: DataValidator.dataFieldType.vehicleDetails));

            fields.Add(new FormField(Name: "txtChassisNumber",
                                    FormControl: txtChassisNumber,
                                    UserEntryText: "",
                                    DatabaseTable: "Vehicle",
                                    DatabaseFieldName: "ChassisNumber",
                                    SuccessTextBox: txtChassisNumberSuccess,
                                    DataFieldType: DataValidator.dataFieldType.vehicleDetails));

            fields.Add(new FormField(Name: "cmbSuitableForHire",
                                    FormControl: cmbSuitableForHire,
                                    UserEntryText: "",
                                    DatabaseTable: "Vehicle",
                                    DatabaseFieldName: "SuitableForHire",
                                    SuccessTextBox: txtSuitableForHireSuccess,
                                    DataFieldType: DataValidator.dataFieldType.suitableForHire));


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

                successTextBlock.Text = validationSuccess ? SUCCESS : FAIL;

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
            bool allFieldsAreVaild = true;

            foreach (FormField field in fields)
            {
                if (field.successTextBox.Text != SUCCESS)
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

                                errors++;
                            }
                            DBfields.Clear();
                            DBvalues.Clear();
                        }
                    }
                    if (errors > 0)
                    {
                        MessageBox.Show("There were some errors inserting data into the database.", "Database Error");
                    }
                    else
                    {
                        //  MessageBox.Show("Record created successfully.", "Database Updated.");
                        CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new CustomerUpdateSuccess(CustomerID));
                    }

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



       

       
    }
}

