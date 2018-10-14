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
    /// Interaction logic for FleetList.xaml
    /// </summary>
    public partial class FleetList : Page
    {
        public FleetList()
        {
            InitializeComponent();
            showAll();
        }

        public void SearchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            String inputString = SearchInput.Text;

            if (String.IsNullOrEmpty(inputString))
            {
                return;
            }

            if (inputString == "*")
            {
                showAll();

                return;
            }

            if (inputString.Length < 3)
            {
                spResultsPanel.Children.Clear();
               
                return;
            }

            spResultsPanel.Children.Clear();

            // Find possible matches.
            searchDatabaseForMatches(inputString);


        }

        private void showAll()
        {
            DataSet d = Database.getInstance().selectStarFrom("VEHICLE as T1 ORDER BY 'DateOfFirstRegistration' DESC");

            addResults(d);


        }

        void searchDatabaseForMatches(String s)
        {
            String inputString = s;
            String[] inputWords = s.Split(' ');

            String[] fieldsToCheck = new String[] { "T1.VehicleVIN", "T1.Manufacturer", "T1.Model" };

            foreach (String word in inputWords)
            {
                word.Trim();
                if (!String.IsNullOrEmpty(word))
                {
                    foreach (String field in fieldsToCheck)
                    {
                        DataSet d = Database.getInstance().select("", new string[] { "Vehicle as T1" }, new String[] { "T1.VehicleVIN, T1.Manufacturer, T1.Model, T1.VehicleDetails, T1.SuitableForHire" }, " WHERE " + field, "=", word, new string[] { "ORDER BY T1.ChassisNumber ASC" });
                        addResults(d);
                    }
                }
            }
            return;
        }

        private void addResults(DataSet d)
        {

            foreach (DataTable table in d.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    String
                         strVehicleVIN = "",
                        strManufacturer = "",
                        strModel = "",
                        strDateOfFirstRegistration = "",
                        strChassisNumber = "",
                        strVehicleDetails = "",
                        strSuitableForHire = "",
                        strImagePath ="";
                        

                    foreach (DataColumn column in table.Columns)
                    {
                        if (column.ColumnName == "VehicleVIN")
                        {
                            strVehicleVIN = row[column].ToString();
                        }
                        if (column.ColumnName == "Manufacturer")
                        {
                            strManufacturer = row[column].ToString();
                        }
                        if (column.ColumnName == "Model")
                        {
                            strModel = row[column].ToString();
                        }
                        if (column.ColumnName == "DateOfFirstRegistration")
                        {
                            strDateOfFirstRegistration = row[column].ToString();
                        }
                        if (column.ColumnName == "ChassisNumber")
                        {
                            strChassisNumber = row[column].ToString();
                        }
                        if (column.ColumnName == "VehicleDetails")
                        {
                            strVehicleDetails = row[column].ToString();
                        }              
                        if (column.ColumnName == "SuitableForHire")
                        {
                            strSuitableForHire = row[column].ToString();
                        }
                        if(column.ColumnName == "ImagePath")
                        {
                            strImagePath = row[column].ToString();
                        }


                   
                    }

                    String vehicleName = strManufacturer + " "+ strModel; 
                    String details = strVehicleDetails;

                    addResult(strVehicleVIN, vehicleName, strImagePath,  strSuitableForHire);

                }
            }


        }

        private void addResults(String[] s)
        {
            if (s.Length == 0)
            {
                return;
            }

            String[] t = new String[5];

            for (int i = 0; i < t.Length; i++)
            {
                if (s.Length > i)
                {
                    t[i] = s[i];
                }
                else
                {
                    t[i] = "";
                }
            }


            addResult(t[0], t[1], t[2], t[3]);


        }

        private void addResult(String vehicleVIN, String vehicleName, String image,  String suitableForHire)
        {
            Button resultButton = new Button();
            resultButton.Name = "ID_" + vehicleVIN;
            resultButton.Width = 200;
            resultButton.Background = Brushes.Gold;
            resultButton.Margin = new Thickness(20);
         //   Style bst = FindResource("CustomerSearchButton") as Style;
         //   resultButton.Style = bst;
            resultButton.ToolTip = "Click to Edit Vehicle Details.";

            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;

            TextBlock txtTop = new TextBlock();
            txtTop.Text = vehicleName;
            txtTop.Foreground = Brushes.DarkRed;
            txtTop.FontSize = 18;
            txtTop.HorizontalAlignment = HorizontalAlignment.Center;
            txtTop.Margin = new Thickness(11,0,15,1);

            TextBlock txtBottom = new TextBlock();
            txtBottom.Text = "VIN: "+vehicleVIN;
            txtBottom.Foreground = Brushes.DarkRed;
            txtBottom.FontSize = 18;
            txtBottom.HorizontalAlignment = HorizontalAlignment.Center;
            txtBottom.Margin = new Thickness(11, 0, 15, 1);

            Image vehicleImage = new Image();


            if(suitableForHire == "TRUE")
            {
                sp.Background = Brushes.LightGreen;
            
            }
            else
            {
                sp.Background = Brushes.IndianRed;
                txtBottom.Text = "NOT FOR HIRE";
                txtTop.Foreground = Brushes.White;
                txtBottom.Foreground = Brushes.White;
            }

         //   Style st = FindResource("CustomerSearchField") as Style;

            vehicleImage.Source = new BitmapImage(new Uri(image, UriKind.Relative));
            vehicleImage.Height = 120;
            vehicleImage.Stretch = Stretch.Fill;

            sp.Children.Add(txtTop);
            sp.Children.Add(vehicleImage);
            sp.Children.Add(txtBottom);

            resultButton.Content = sp;
            resultButton.Click += resultButton_Click;

            
            spResultsPanel.Children.Add(resultButton);
        }

        void resultButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int length = button.Name.Length - 3;
            String strVIN = button.Name.Substring(3, length);
            
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new FleetEdit(strVIN));
        }

        private void btnCreateNewVehicle_Click(object sender, RoutedEventArgs e)
        {
            CommonTasks.getInstance().frmCommonTasksMainFrame.Navigate(new FleetNew());
        }
    }
}

