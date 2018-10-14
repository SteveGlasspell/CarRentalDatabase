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
using System.Data;
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
 *  
 *  Document    : This page provides a searchable list of customers. By default, the page displays the full list 
 *                 of customers. A search bar at the top of the page can be used to search on a variety of fields.
 *                 Just start typing and when a match is found in any of the main fields, the database
 *                 will return results.
 */

namespace StephenGlasspell_CarRental
{
    /// <summary>
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomerList : Page
    {
        
        
        public CustomerList()
        {
            InitializeComponent();
            showAll();
          

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = DataDelegate.previousCursor;
        }


        // Shows all customer records.
        private void showAll()
        {
            DataSet d = Database.getInstance().selectStarFrom("Customer as T1, " +
                                                 "CustomerContact as T2, " +
                                                 "DrivingLicense as T3, " +
                                                 " CreditCard as T4 " +
                                                 "WHERE T1.CustomerID = T2.CustomerID " +
                                                 "and T1.CustomerID = T3.CustomerID " +
                                                 "and T1.CustomerID = T4.CustomerID " +
                                                 "ORDER BY T1.LastName ASC");

            addResults(d);

            
        }
        
        // Method to handle the text input in the search bar.
        private void SearchInput_TextChanged(object sender, TextChangedEventArgs e)
        {           
            String inputString = SearchInput.Text;

            if (String.IsNullOrEmpty(inputString))
            {
                return;
            }

            // Shortcut to display all records.
            if (inputString == "*")
            {
                showAll();

                return;
            }

            if (inputString.Length < 3)
            {
                spResultsPanel.Children.Clear();
                spHeaderRow.Visibility = Visibility.Collapsed;
                return;
            }
           
            spResultsPanel.Children.Clear();

            // Find possible matches.
            searchDatabaseForMatches(inputString);
            
            
            
        }

        // Method that takes the input text from the search bar and queries the 
        //database for matches across a variety of fields.
        void searchDatabaseForMatches(String s)
        {
            String inputString = s;
            String[] inputWords = s.Split(' ');

            String[] fieldsToCheck = new String[] {"T1.FirstName",
                                                    "T1.LastName",
                                                    "T3.MobilePhone",
                                                    "T3.Email",
                                                    "T2.DrivingLicenseNumber" };

            foreach (String word in inputWords)
            {
                word.Trim();
                if(!String.IsNullOrEmpty(word))
                {
                    foreach (String field in fieldsToCheck)
                    {
                        DataSet d = Database.getInstance().select("", new string[] { "Customer as T1, " +
                                                                        "DrivingLicense as T2, " +
                                                                        "CustomerContact as T3" }, 
                                                                        new String[] { "T1.CustomerID, " +
                                                                        "T1.Title, " +
                                                                        "T1.FirstName, " +
                                                                        "T1.LastName, " +
                                                                        "T3.HouseNumber, " +
                                                                        "T3.StreetName, " +
                                                                        "T3.Suburb, " +
                                                                        "T3.City, " +
                                                                        "T3.State, " +
                                                                        "T3.Postcode, " +
                                                                        "T3.MobilePhone, " +
                                                                        "T2.DrivingLicenseNumber" }, 
                                                                        "WHERE T1.CustomerID = T2.CustomerID " +
                                                                        "and T3.CustomerID = T1.CustomerID " +
                                                                        "and " + field, "=", word , 
                                                                        new string[] { "ORDER BY T1.LastName ASC" });
                        addResults(d);
                    }
                }
            }
            return;
        }

        private void addResults(DataSet d)
        {
          
            foreach(DataTable table in d.Tables)
            {
                    foreach(DataRow row in table.Rows)
                    {
                        String 
                             strCustomerID = "",
                            strTitle = "",
                            strFirstName = "",
                            strLastName = "",
                            strDateOfBirth = "",
                            strHouseNo = "",
                            strStreetName = "",
                            strSuburb = "",
                            strCity = "",
                            strState = "",
                            strPostcode = "",
                            strPhone = "",
                            strEmail = "",
                            strDrivingLicenseNumber = "";

                    foreach (DataColumn column in table.Columns)
                    {
                        if (column.ColumnName == "CustomerID")
                        {
                            strCustomerID = row[column].ToString();
                        }
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
                        if (column.ColumnName == "DateOfBirth")
                        {
                            strDateOfBirth = row[column].ToString();
                        }
                        if (column.ColumnName == "HouseNumber")
                        {
                            strHouseNo = row[column].ToString();
                        }
                        if (column.ColumnName == "StreetName")
                        {
                            strStreetName = row[column].ToString();
                        }
                        if (column.ColumnName == "Suburb")
                        {
                            strSuburb = row[column].ToString();
                        }
                        if (column.ColumnName == "City")
                        {
                            strCity = row[column].ToString();
                        }
                        if (column.ColumnName == "State")
                        {
                            strState = row[column].ToString();
                        }
                        if (column.ColumnName == "Postcode")
                        {
                            strPostcode = row[column].ToString();
                        }
                        if (column.ColumnName == "MobilePhone")
                        {
                            strPhone = row[column].ToString();
                        }
                        if (column.ColumnName == "Email")
                        {
                            strEmail = row[column].ToString();
                        }
                        if (column.ColumnName == "DrivingLicenseNumber")
                        {
                            strDrivingLicenseNumber = row[column].ToString();
                        }
                    }

                    String fullName = strTitle + " " + strFirstName + " " + strLastName;
                    String address = strHouseNo + " " + strStreetName + " " + strSuburb + " " + strCity + " " + strState + " " + strPostcode;

                    addResult(strCustomerID, fullName, address, strPhone, strDrivingLicenseNumber);
                    
                }
            }

          
        }

        private void addResults(String[] s)
        {
            if(s.Length == 0)
            {
                return;
            }

            String[] t = new String[5];

            for(int i = 0; i < t.Length; i++)
            {
                if(s.Length > i)
                {
                    t[i] = s[i];
                }
                else
                {
                    t[i] = "";
                }
            }
            

            addResult(t[0], t[1], t[2], t[3], t[4]);


        }

        private void addResult(String customerID, String name, String address, String phone, String drivingLicense)
        {
            Button resultButton = new Button();
            resultButton.Name = "ID_" + customerID;
            Style bst = FindResource("CustomerSearchButton") as Style;
            resultButton.Style = bst;
            resultButton.ToolTip = "Click to Edit Customer Details.";

            TextBlock tbName = new TextBlock();
            TextBlock tbAddress = new TextBlock();
            TextBlock tbPhone = new TextBlock();
            TextBlock tbDrivingLicense = new TextBlock();

            Style st = FindResource("CustomerSearchField") as Style;

            tbName.Style = st;
            tbName.Width = 250;
            tbName.Text = name;
            tbName.Padding = new Thickness(15,2,15,2);

            tbAddress.Style = st;
            tbAddress.Width = 250;
            tbAddress.Text = address;
            tbAddress.HorizontalAlignment = HorizontalAlignment.Left;
            tbAddress.Padding = new Thickness(15, 2, 15, 2);

            tbPhone.Style = st;
            tbPhone.Width = 150;
            tbPhone.Text = phone;
            tbPhone.Padding = new Thickness(15, 2, 15, 2);

            tbDrivingLicense.Style = st;
            tbDrivingLicense.Width = 215;
            tbDrivingLicense.Text = drivingLicense;
            tbDrivingLicense.Padding = new Thickness(15, 2, 15, 2);

            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            stack.Children.Add(tbName);
            stack.Children.Add(tbAddress);
            stack.Children.Add(tbPhone);
            stack.Children.Add(tbDrivingLicense);
            resultButton.Content = stack;
            resultButton.Click += resultButton_Click;

            spHeaderRow.Visibility = Visibility.Visible;
            spResultsPanel.Children.Add(resultButton);
        }

        void resultButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int length = button.Name.Length - 3;
            String name = button.Name.Substring(3, length);
            int CustomerID = int.Parse(name);
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new CustomersEdit(CustomerID));
        }

        private void btnCreateNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new CustomersNew()); 
        }

       
    }
}
