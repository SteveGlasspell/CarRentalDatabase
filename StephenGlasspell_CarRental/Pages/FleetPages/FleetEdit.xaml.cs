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
 */

namespace StephenGlasspell_CarRental
{
    /// <summary>
    /// Interaction logic for FleetEdit.xaml
    /// </summary>
    public partial class FleetEdit : Page
    {

         String VehicleVIN;



        private Color SUCCESS_COLOR = Color.FromArgb(150, 0, 255, 0);
        private Color FAIL_COLOR = Color.FromArgb(50, 255, 0, 0);

        private List<FormField> fields = new List<FormField>();

        public FleetEdit()
        {
            InitializeComponent();
            populateFields();
        }

        public FleetEdit(String strVIN)
        {
            InitializeComponent();
            VehicleVIN = strVIN;
            populateFields();
            showVehicleData();
        }

        void showVehicleData()
        {
            DataSet d = Database.getInstance().select("", new string[] { " Vehicle as T1 " }, new String[] { " T1.VehicleVIN, T1.Manufacturer, T1.Model, T1.VehicleDetails, T1.DailyRate, T1.ChassisNumber,  T1.SuitableForHire, T1.DateOfFirstRegistration, T1.ImagePath " }, "WHERE T1.VehicleVIN ", "=", VehicleVIN, new string[] { " ORDER BY T1.DateOfFirstRegistration DESC;" });

            String
                        strVehicleVIN = "",
                       strManufacturer = "",
                       strModel = "",
                       strDateOfFirstRegistration = "",
                       strVehicleDetails = " ",
                       strDailyRate = "",
                       strSuitableForHire = "",
                       strImagePath="";

            foreach (DataTable table in d.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                   

                    foreach (DataColumn column in table.Columns)
                    {
                        if (column.ColumnName == "VehicleVIN")
                        {
                            strVehicleVIN = row[column].ToString();
                            txtVehicleVIN.Text = strVehicleVIN;       
                        }
                        if (column.ColumnName == "Manufacturer")
                        {
                            txtManufacturer.Text = row[column].ToString();
                        }
                        if (column.ColumnName == "Model")
                        {
                            txtModel.Text = row[column].ToString();
                        }
                        if (column.ColumnName == "DateOfFirstRegistration")
                        {
                            dpDateOfFirstRegistration.SelectedDate = DataValidator.stringToDate(row[column].ToString());
                        }
                        if (column.ColumnName == "VehicleDetails")
                        {
                            txtVehicleDetails.Text = row[column].ToString();
                        }
                        if(column.ColumnName == "DailyRate")
                        {
                            txtDailyRate.Text = string.Format("{0:C}", Convert.ToDecimal(row[column].ToString()));
                            
                        }

                  
                        if(column.ColumnName == "ChassisNumber")
                        {
                            txtChassisNumber.Text = row[column].ToString();
                        }

                        if (column.ColumnName == "SuitableForHire")
                        {
                            cmbSuitableForHire.SelectedIndex = DataValidator.getComboBoxIndexOf(cmbSuitableForHire, row[column].ToString());
                        }
                        if(column.ColumnName == "ImagePath")
                        {
                            strImagePath = row[column].ToString();
                            imgVehicleImage.Source = new BitmapImage(new Uri(strImagePath, UriKind.Relative));
                        }

                    }

                }
            }
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

            fields.Add(new FormField(Name: "txtDailyRate",
                                    FormControl: txtDailyRate,
                                    UserEntryText: "",
                                    DatabaseTable: "Vehicle",
                                    DatabaseFieldName: "DailyRate",
                                    SuccessTextBox: txtDailyRateSuccess,
                                    DataFieldType: DataValidator.dataFieldType.dailyRate));

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
                                    DataFieldType: DataValidator.dataFieldType.chassisNumber));

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
            int errors = 0;

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
                return;
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

                foreach (String table in tables)
                {
                    List<String> DBfields = new List<String>();
                    List<String> DBvalues = new List<String>();

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

                            if (Database.getInstance().update(table, databaseFields, databaseValues, " WHERE VehicleVIN", " = ","'" +VehicleVIN+"'") == 0)
                            {

                                errors++;
                            }
                            DBfields.Clear();
                            DBvalues.Clear();
                        }
                    }
                }
            }
            if (errors > 0)
            {
                MessageBox.Show("There were some errors updating the database.", "Database Error");
            }
            else
            {
                CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new VehicleUpdateSuccess(VehicleVIN));

               
            }
        }

        private void btnUpdateVehicleImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBookService_Click(object sender, RoutedEventArgs e)
        {
           
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new CalendarMonth(VehicleVIN));
        }

        private void btnServiceHistory_Click(object sender, RoutedEventArgs e)
        {
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new ServiceHistory(VehicleVIN));
        }
    }






}

